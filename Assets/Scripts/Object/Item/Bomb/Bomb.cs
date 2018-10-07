using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	explosion;			// 폭발

	// 인스펙터 비노출 변수
	// 일반
	private Tilemap[]	tileMaps;			// 타일맵들
	private Grid[]		grids;				// 그리드들


	// 초기화
	private void Awake()
	{
		GameObject[] targetGrids = GameObject.FindGameObjectsWithTag("SoilBlock");

		tileMaps = new Tilemap[targetGrids.Length];
		grids = new Grid[targetGrids.Length];

		for (int i = 0; i < targetGrids.Length; i++)
		{
			grids[i] = targetGrids[i].transform.parent.GetComponent<Grid>();
			tileMaps[i] = targetGrids[i].GetComponent<Tilemap>();
		}
	}

	// 시작
	private void Start()
	{
		StartCoroutine("BombCountDown");
	}

	// 폭발
	private void ComitBomb()
	{
		for (int t = 0; t < tileMaps.Length; t++)
		{
			Vector3Int core = grids[t].WorldToCell(transform.position);

			for (int i = -4; i <= 4; i++)
			{
				for (int j = -4; j <= 4; j++)
				{
					tileMaps[t].SetTile(new Vector3Int(core.x + i, core.y + j, 0), null);
				}
			}
		}

		// 폭발 충돌체 소환
		Instantiate(explosion, transform.position, Quaternion.identity);

		//Destroy(gameObject);
	}
	
	// 폭발 코루틴
	private IEnumerator BombCountDown()
	{
		yield return new WaitForSeconds(1f);

		ComitBomb();
	}
}
