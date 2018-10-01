using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private		GameObject[]		mapPattern;         // 맵 패턴


	// 초기화
	private void Awake()
	{
		MapCreate();
	}

	// 맵 생성
	public void MapCreate()
	{
		for (int i = 0; i < 5; i++)
		{
			Instantiate(mapPattern[Random.Range(0, mapPattern.Length)], new Vector2(0, 70 * i), Quaternion.identity);
		}
	}
}
