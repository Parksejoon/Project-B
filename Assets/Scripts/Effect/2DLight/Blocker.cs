using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
	public CompositeCollider2D	compositeCollider;      // 타일맵 콜라이더
	public GameObject			debugObject;			

	private bool				isCalc;					// 계산 했는지
	private List<Vector2>		calcEdges;              // 계산된 엣지들



	// 초기화
	private void Awake()
	{
		compositeCollider = GetComponent<CompositeCollider2D>();
	}

	// 모서리를 구함
	public void GetEdges(List<Vector2> edges)
    {
		//if (gameObject.isStatic && isCalc)
		//{
		//	edges = calcEdges;

		//	return;
		//}

		for (int i = 0; i < compositeCollider.pathCount; i++)
		{
			int pathPointCount = compositeCollider.GetPathPointCount(i);
			Vector2[] pathVerts = new Vector2[pathPointCount];

			compositeCollider.GetPath(i, pathVerts);
			edges.AddRange(pathVerts);

			for (int k = 0; k < pathPointCount - 1; k++)
			{
				edges.Add(pathVerts[k]);
				edges.Add(pathVerts[k + 1]);
			}

			edges.Add(pathVerts[pathPointCount - 1]);
			edges.Add(pathVerts[0]);

			Debug.Log(pathPointCount);
			for (int j = 0; j < pathPointCount; j++)
			{
				Debug.Log(pathVerts[j]);
				Instantiate(debugObject, new Vector2(pathVerts[j].x, pathVerts[j].y), Quaternion.identity, transform);
			}
		}

		//calcEdges = edges;

		//// 사각형 기준으로 4방향
		//Vector3 v1 = new Vector3(-0.5f, -0.5f, 0.0f);
		//      Vector3 v2 = new Vector3(+0.5f, -0.5f, 0.0f);
		//      Vector3 v3 = new Vector3(+0.5f, +0.5f, 0.0f);
		//      Vector3 v4 = new Vector3(-0.5f, +0.5f, 0.0f);

		//// 꼭지점 구함 ( 잘모르겠음 )
		//      v1 = transform.localToWorldMatrix.MultiplyPoint(v1);
		//      v2 = transform.localToWorldMatrix.MultiplyPoint(v2);
		//      v3 = transform.localToWorldMatrix.MultiplyPoint(v3);
		//      v4 = transform.localToWorldMatrix.MultiplyPoint(v4);

		//// 모서리를 넣음
		//      edges.Add(new Vector2(v1.x, v1.y));
		//      edges.Add(new Vector2(v2.x, v2.y));

		//      edges.Add(new Vector2(v2.x, v2.y));
		//      edges.Add(new Vector2(v3.x, v3.y));

		//      edges.Add(new Vector2(v3.x, v3.y));
		//      edges.Add(new Vector2(v4.x, v4.y));

		//      edges.Add(new Vector2(v4.x, v4.y));
		//      edges.Add(new Vector2(v1.x, v1.y));
	}
}
