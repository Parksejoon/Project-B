using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocker : MonoBehaviour
{
	public TilemapRenderer abs;

	// Get the outline of the object for shadow map rendering
	// 모서리를 가져옴 ( 문제는 사각형만 된다는것. )
	public void GetEdges(List<Vector2> edges)
    {
		// 사각형 기준으로 4방향
        Vector3 v1 = new Vector3(-0.5f, -0.5f, 0.0f);
        Vector3 v2 = new Vector3(+0.5f, -0.5f, 0.0f);
        Vector3 v3 = new Vector3(+0.5f, +0.5f, 0.0f);
        Vector3 v4 = new Vector3(-0.5f, +0.5f, 0.0f);

		// 꼭지점 구함 ( 잘모르겠음 )
        v1 = transform.localToWorldMatrix.MultiplyPoint(v1);
        v2 = transform.localToWorldMatrix.MultiplyPoint(v2);
        v3 = transform.localToWorldMatrix.MultiplyPoint(v3);
        v4 = transform.localToWorldMatrix.MultiplyPoint(v4);

		// 모서리를 넣음
        edges.Add(new Vector2(v1.x, v1.y));
        edges.Add(new Vector2(v2.x, v2.y));

        edges.Add(new Vector2(v2.x, v2.y));
        edges.Add(new Vector2(v3.x, v3.y));

        edges.Add(new Vector2(v3.x, v3.y));
        edges.Add(new Vector2(v4.x, v4.y));

        edges.Add(new Vector2(v4.x, v4.y));
        edges.Add(new Vector2(v1.x, v1.y));
    }
}
