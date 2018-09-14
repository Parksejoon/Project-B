using UnityEngine;

public class BallPoolManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject			ballPrefab;             // 공 프리팹

	// 수치
	[SerializeField]
	private int					size;					// 크기


	// 초기화
	private void Awake()
	{
		ObjectPoolManager.Create("Ball", ballPrefab, size);
	}
}
