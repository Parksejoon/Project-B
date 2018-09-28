using UnityEngine;

public class ObjectPoolGenerator : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject			ballPrefab;             // 공 프리팹
	[SerializeField]
	private GameObject			bombPrefab;				// 폭탄 프리팹

	// 수치
	[SerializeField]
	private int					ballSize;               // 공 크기
	[SerializeField]
	private int					bombSize;				// 폭탄 크기


	// 초기화
	private void Awake()
	{
		ObjectPoolManager.Create("Ball", ballPrefab, ballSize);
		//ObjectPoolManager.Create("Bomb", ballPrefab, ballSize);
	}
}
