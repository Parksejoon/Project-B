using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GameLight : ShadowCaster {

    public Color        mColour;
    private float       mAngle = 0;
    private float       mSpread = 360;
    public float        mFalloffExponent = 1.0f;
    public float        mAngleFalloffExponent = 1.0f;
	public float 		mFullBrightRadius = 0.0f;
    private float       mRadius = 0.0f;

    public float Angle
    {
        get
        {
            return mAngle;
        }
        set
        {
            if (mAngle != value)
            {
                mAngle = value;

                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, mAngle);
            }
        }
    }
    public float Spread
    {
        get
        {
            return mSpread;
        }
        set
        {
            if (mSpread != value)
            {
                mSpread = value;
                RebuildQuad();
            }
        }
    }

	public void Start() 
	{
		// 반지름을 잡음
        mRadius = Mathf.Max(transform.localScale.x, transform.localScale.y) * 0.5f;

        transform.localScale = Vector3.one;

        RebuildQuad();
    }

    /// <summary>
    /// Build the light's quad mesh. This aims to fit the light cone as best as possible.
    /// 빛의 쿼드 메쉬를 생성. 가능한 밝은 원뿔을 맞게 목표로 한다.
    /// </summary>
    public void RebuildQuad()
    {
		// 메쉬를 가져옴
        Mesh m = GetComponent<MeshFilter>().mesh;

		// 버텍스 리스트를 만듬
        List<Vector3> verts = new List<Vector3>();

		// 180도가 넘으면 전체를 하나의 메쉬로 잡음
        if (mSpread > 180.0f)
        {
            verts.Add(new Vector3(-mRadius, -mRadius));
            verts.Add(new Vector3(+mRadius, +mRadius));
            verts.Add(new Vector3(+mRadius, -mRadius));
            verts.Add(new Vector3(-mRadius, +mRadius));
        }
		// 180도 미만이면 반절만 하나의 메쉬로 잡음
        else
        {
            float radius = mRadius;

            float minAngle = -mSpread * 0.5f;
            float maxAngle = +mSpread * 0.5f;

            Bounds aabb = new Bounds(Vector3.zero, Vector3.zero);
            aabb.Encapsulate(new Vector3(radius, 0.0f));
            aabb.Encapsulate(new Vector3(Mathf.Cos(Mathf.Deg2Rad * minAngle), Mathf.Sin(Mathf.Deg2Rad * minAngle)) * radius);
            aabb.Encapsulate(new Vector3(Mathf.Cos(Mathf.Deg2Rad * maxAngle), Mathf.Sin(Mathf.Deg2Rad * maxAngle)) * radius);

            verts.Add(new Vector3(aabb.min.x, aabb.min.y));
            verts.Add(new Vector3(aabb.max.x, aabb.max.y));
            verts.Add(new Vector3(aabb.max.x, aabb.min.y));
            verts.Add(new Vector3(aabb.min.x, aabb.max.y));
        }

		// 메쉬의 정점을 설정
        m.SetVertices(verts);
    }

    /// <summary>
	/// 이 함수는 쉐도우 맵을 드로우할 때 필요한 파라미터와 이 오브젝트가 렌더링 될 때 사용되는 쉐도우 맵의 파라미터를 설정한다.
	/// 만약 쉐도우 렌더링을 건너뛰고싶다면 null을 반환해라
    /// </summary>
    /// <param name="shadowMapTexture"></param>
    /// <returns></returns>
    public override MaterialPropertyBlock BindShadowMap(Texture shadowMapTexture)
    {
        Vector4 shadowMapParams = GetShadowMapParams(mShadowMapSlot);

		// 쉐이더로 정보를 넘겨줌
        mMaterialPropertyBlock.SetVector("_LightPosition", new Vector4(transform.position.x, transform.position.y, mAngle * Mathf.Deg2Rad, mSpread * Mathf.Deg2Rad * 0.5f));
        mMaterialPropertyBlock.SetVector("_ShadowMapParams", shadowMapParams);

		// 이 오브젝트의 머티리얼을 가져옴
		MeshRenderer mr = GetComponent<MeshRenderer> ();

        Material mat = mr.materials[0];

        float radius = mRadius;

		// 머티리얼에 정보를 넘겨줌
        mat.SetVector("_Color", mColour);
		mat.SetVector("_LightPosition", new Vector4(transform.position.x, transform.position.y, mFalloffExponent, mAngleFalloffExponent));
		mat.SetVector("_Params2", new Vector4(mAngle * Mathf.Deg2Rad, mSpread * Mathf.Deg2Rad * 0.5f, 1.0f / ((1.0f - mFullBrightRadius) * radius), mFullBrightRadius * radius));
        mat.SetVector("_ShadowMapParams", shadowMapParams);
        mat.SetTexture("_ShadowTex", shadowMapTexture);
			
        return mMaterialPropertyBlock;
    }
}
