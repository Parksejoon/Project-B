using UnityEngine;
using UnityEngine.Tilemaps;

public class OriginalExplosion : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Tilemap[]	dangerTileMaps;			// 위험 블록 타일맵들
	private Tilemap[]	normalTileMaps;			// 일반 블록 타일맵들
	private Grid[]		grids;					// 그리드들

										
	// 초기화
	private void Awake()
	{
		GameObject[] targetDangerGrids = GameObject.FindGameObjectsWithTag("DangerBlock");
		GameObject[] targetNormalGrids = GameObject.FindGameObjectsWithTag("SoilBlock");

		dangerTileMaps = new Tilemap[targetDangerGrids.Length];
		normalTileMaps = new Tilemap[targetNormalGrids.Length];
		grids = new Grid[targetNormalGrids.Length];

		for (int i = 0; i < targetNormalGrids.Length; i++)
		{
			grids[i] = targetNormalGrids[i].transform.parent.GetComponent<Grid>();
			dangerTileMaps[i] = targetDangerGrids[i].GetComponent<Tilemap>();
			normalTileMaps[i] = targetNormalGrids[i].GetComponent<Tilemap>();
		}
	}

	// 시작
	private void Start()
	{
		ComitBomb();
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Ball"))
		{
			collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 3f, ForceMode2D.Impulse);
		}

		if (collision.CompareTag("BallGenerator"))
		{
			Destroy(collision.gameObject);
		}
	}

	// 폭발
	private void ComitBomb()
	{
		for (int t = 0; t < dangerTileMaps.Length; t++)
		{
			Vector3Int core = grids[t].WorldToCell(transform.position);

			for (int i = -4; i <= 4; i++)
			{
				for (int j = -4; j <= 4; j++)
				{
					dangerTileMaps[t].SetTile(new Vector3Int(core.x + i, core.y + j, 0), null);
				}
			}
		}

		for (int t = 0; t < normalTileMaps.Length; t++)
		{
			Vector3Int core = grids[t].WorldToCell(transform.position);

			for (int i = -4; i <= 4; i++)
			{
				for (int j = -4; j <= 4; j++)
				{
					normalTileMaps[t].SetTile(new Vector3Int(core.x + i, core.y + j, 0), null);
				}
			}
		}
	}
}
