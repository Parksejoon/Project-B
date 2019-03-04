using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    public static BlockerManager Instance
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
    }

    // Update is called once per frame
    void Update ()
    {
		// 왜인지 모르겠지만 없어도 잘돌아감;
        //GetLightBlockerMesh();
    }

    // Create a mesh containing all the light blocker edges
	// 모든 light blocker들의 모서리를 이용해 하나의 매쉬로 만든다!
    public Mesh GetLightBlockerMesh()
    {
		// 자식 오브젝트의 Blocker 컴포넌트들을 가져옴
        Blocker[] blockers = GetComponentsInChildren<Blocker>();

		// 모서리 리스트를 만듬
        List<Vector2> edges = new List<Vector2>();
        foreach (Blocker b in blockers)
        {
            b.GetEdges(edges);
        }

		// 정점 리스트와 노말 리스트를 만듬
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> normals = new List<Vector2>();
		// i += 2 모서리는 edges[0] = edges[1]이기 때문에
        for (int i = 0; i < edges.Count; i += 2)
        {
			// 각 정점들을 넣음
            verts.Add(edges[i + 0]);
            verts.Add(edges[i + 1]);
			// 노말들을 넣음
            normals.Add(edges[i + 1]);
            normals.Add(edges[i + 0]);
        }

        // Simple 1:1 index buffer
		// 1:1로 대응하는 인덱스 버퍼를 만듬
        int[] incides = new int[edges.Count];
        for (int i = 0; i < edges.Count; i++)
        {
            incides[i] = i;
        }

		// 새로운 매쉬를 만듬
        Mesh mesh = new Mesh();
		// 매쉬의 정점들을 대입
        mesh.SetVertices(verts);
		// 매쉬의 uv값을 대입
        mesh.SetUVs(0, normals);
		// 인덱스 버퍼를 대입
        mesh.SetIndices(incides, MeshTopology.Lines, 0);

        return mesh;
    }
}
