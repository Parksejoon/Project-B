using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class ShadowCaster : MonoBehaviour
{
	// 쉐도우 캐스터 풀.
	// 아마도 쉐도우 캐스터를 오브젝트 풀처럼 사용할려는 듯 하다.
	public static List<ShadowCaster> ShadowCasterPool = new List<ShadowCaster>();
	
	// 이 쉐도우 맵이 몇번째 슬롯에 있는지.
	public int mShadowMapSlot = -1;

	// MaterialPropertyBlock = 적용할 머티리얼의 블록을 나타냄
	// 여러 오브젝트를 그리고 싶은 경우 사용함, 아마도 그림자를 그리는 용도가 아닐까 싶음
	// 일시적으로 material을 변경할때 사용된다고 한다.
    public MaterialPropertyBlock mMaterialPropertyBlock;


    public virtual void Awake()
    {
        mMaterialPropertyBlock = new MaterialPropertyBlock();
    }

    public virtual void OnEnable()
    {
        mShadowMapSlot = ShadowMapAlloc();
		
		// 쉐도우 캐스터 풀 관리
		ShadowCasterPool.Add(this);
    }

	public virtual void OnDisable()
    {
		// 쉐도우 캐스터 풀 관리
		ShadowCasterPool.Remove(this);

		//
        if (mShadowMapSlot >= 0)
        {
            ShadowMapFree(mShadowMapSlot);
            mShadowMapSlot = -1;
        }
    }

    public virtual MaterialPropertyBlock BindShadowMap(Texture shadowMapTexture)
    {
        return null;
    }

    /// <summary>
    /// Allocator row rows in the shadow map
	/// 쉐도우 맵 할당기 행
    /// </summary>
	
    static ulong ShadowMapAllocator = 0;

	// 이것은 쉐도우 맵 최대 텍스쳐와 일치해야 한다.
	// 한마디로 최대 light갯수가 이 변수값을 넘으면 안됨.
    public const int MAX_SHADOW_MAPS = 64;

    /// <summary>
    // 쉐도우 맵 행에 1d 쉐도우 맵을 할당해준다
    /// </summary>
    /// <returns></returns>
    public static int ShadowMapAlloc()
    {
		// 전체 쉐도우 맵중에 비어있는 쉐도우 맵을 탐색
        for (int i = 0; i < MAX_SHADOW_MAPS; i++)
        {
            ulong mask = (ulong)1 << i;

			// 만약 쉐도우 맵이 비어있으면 쉐도우 맵 얼로케이트에 있다는것을 표시하고
			// 비어있는 쉐도우 맵 번호를 반환
            if ((ShadowMapAllocator & mask) == 0)
            {
                ShadowMapAllocator |= mask;
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 쉐도우 맵 행을 비워준다
    /// </summary>
    /// <param name="slot"></param>
    public static void ShadowMapFree(int slot)
    {
		// 만약 슬롯에 할당되어있으면
        if (slot >= 0)
        {
            ulong mask = (ulong)1 << slot;

			// 해당 슬롯을 비워준다
			Debug.Assert((ShadowMapAllocator & mask) != 0);
            ShadowMapAllocator &= ~mask;
        }
    }

    /// <summary>
    /// 1d 쉐도우 맵을 읽고 쓰는데 사용하는 파라미터들을 계산한다
    /// x = 쉐도우 맵을 읽기 위한 파라미터 (uv space (0,1))
    /// y = 쉐도우 맵을 쓰기 위한 파라미터 (clip space (-1,+1))
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public static Vector4 GetShadowMapParams(int slot)
    {
        float u1 = ((float)slot + 0.5f) / MAX_SHADOW_MAPS;
        float u2 = (u1 - 0.5f) * 2.0f;

        if (   //(SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGL2) // OpenGL2 is no longer supported in Unity 5.5+
               (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore)
            || (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2)
            || (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3)
            )
        {
            return new Vector4(u1, u2, 0.0f, 0.0f);
        }
        else
        {
            return new Vector4(1.0f - u1, u2, 0.0f, 0.0f);
        }
    }
}
