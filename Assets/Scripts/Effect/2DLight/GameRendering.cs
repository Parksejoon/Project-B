using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameRendering : MonoBehaviour
{
	private CommandBuffer mCommandBuffer1;			// 커맨드 버퍼
	private Mesh mShadowBlockerDynamicMesh = null;	// 쉐도우 블로커 동적 메쉬
	public RenderTexture mShadowMapInitialTexture;  // 이것은 540도의 가치를 가진다 (속이기 위해). 주어진 각도를 샘플링 하기 위해 두번의 참조가 필요하다.
    public RenderTexture mShadowMapFinalTexture;    // 이것은 360도 보다 위의 것을 360도로 줄인다. 각도를 샘플링 하려면 한번의 참조가 필요하다.
    public Material mShadowMapMaterial;				// 쉐도우 맵 머티리얼
    private Mesh mShadowMapOptimiseMesh = null;		// 쉐도우 맵 최적화 메쉬
    public Material mShadowMapOptimiseMaterial;		// 쉐도우 맵 최적화 머티리얼

    public static GameRendering Instance
    {
        get;
        private set;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        mShadowMapOptimiseMesh = MakeFullscreenRenderMesh();

        Debug.Assert(mShadowMapInitialTexture.height == ShadowCaster.MAX_SHADOW_MAPS);
    }

	// 렌더링 되기 전에
    public void OnPreRender()
    {
        if (mCommandBuffer1 == null)
        {
            mCommandBuffer1 = new CommandBuffer();

            Camera camera = GetComponent<Camera>();
            camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, mCommandBuffer1);
        }

        // 동적 블로커 메쉬 재생성
        mShadowBlockerDynamicMesh = demo.Instance.GetLightBlockerMesh(); 

        if (mShadowBlockerDynamicMesh != null)
        {
            mCommandBuffer1.Clear();

            mCommandBuffer1.SetRenderTarget(mShadowMapInitialTexture);

            mCommandBuffer1.ClearRenderTarget(true, true, new Color(1, 1, 1, 1), 1.0f);

            // 스스로의 그림자를 캐스트하는 모든 항목에 대해 쉐도우 맵을 렌더링
            // 쉐도우 블로커들은 그들의 극 공간에 있는 각각 광원에 대해 재 렌더링되며 
            // 매번 쉐도우 맵의 다른 행에 씀 ( 행들은 ShadowCaster.ShadowMapAlloc에 의해 할당된다 )
            {
				foreach (ShadowCaster caster in ShadowCaster.ShadowCasterPool)
                {
                    MaterialPropertyBlock properties = caster.BindShadowMap(mShadowMapFinalTexture);
                    if (properties != null)
                    {
                        mCommandBuffer1.DrawMesh(mShadowBlockerDynamicMesh, Matrix4x4.identity, mShadowMapMaterial, 0, -1, properties);
                    }
               }
            }
        }

        // 쉐도우맵을 단일 샘플을 얻을 수 있는 텍스처로 줄여 180도 더 떨어진 덮어씌여진 지역을 제거
        {
            mShadowMapOptimiseMaterial.SetTexture("_ShadowMap", mShadowMapInitialTexture);

            mCommandBuffer1.SetRenderTarget(mShadowMapFinalTexture);

            mCommandBuffer1.DrawMesh(mShadowMapOptimiseMesh, Matrix4x4.identity, mShadowMapOptimiseMaterial);
        }
    }

    // 전체 화면 쉐이더를 수행하는데 적합한 심플 메쉬를 만듬
    // pass, 예를 들어 화면을 (0,0) 부터 (1,1)까지 uvs를 사용해 채운다.
    public static Mesh MakeFullscreenRenderMesh()
    {
		// 전체 화면을 메쉬로 채운다고 생각하면 됨.
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs0 = new List<Vector2>();
        int[] indices = new int[6];

        verts.Add(new Vector3(-1.0f, +1.0f, 0.0f));
        verts.Add(new Vector3(+1.0f, +1.0f, 0.0f));
        verts.Add(new Vector3(+1.0f, -1.0f, 0.0f));
        verts.Add(new Vector3(-1.0f, -1.0f, 0.0f));

        uvs0.Add(new Vector2(0.0f, 0.0f));
        uvs0.Add(new Vector2(1.0f, 0.0f));
        uvs0.Add(new Vector2(1.0f, 1.0f));
        uvs0.Add(new Vector2(0.0f, 1.0f));

        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        indices[3] = 0;
        indices[4] = 2;
        indices[5] = 3;

        Mesh mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.SetUVs(0, uvs0);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        return mesh;
    }
}
