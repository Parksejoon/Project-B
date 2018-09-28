using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Tilemap tileMap;
	private Grid	grid;


	// 초기화
	private void Awake()
	{
		GameObject target = GameObject.Find("Sub");

		grid = target.transform.parent.GetComponent<Grid>();
		tileMap = target.GetComponent<Tilemap>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine("BombCountDown");
	}

	// 폭발
	private void ComitBomb()
	{
		Vector3Int core = grid.WorldToCell(transform.position);

		for (int i = -2; i <= 2; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				tileMap.SetTile(new Vector3Int(core.x + i, core.y + j, 0), null);
			}
		}

		Destroy(gameObject);
	}

	// 폭발 코루틴
	private IEnumerator BombCountDown()
	{
		yield return new WaitForSeconds(1f);

		ComitBomb();
	}
}
