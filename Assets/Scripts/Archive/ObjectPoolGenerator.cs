using UnityEngine;

public class ObjectPoolGenerator : MonoBehaviour
{
	[SerializeField]
	private GameObject		ballPrefab;         // 공 프리팹
	[SerializeField]
	private int				ballSize;           // 공 크기


	// 초기화
	private void Awake()
	{
		//ObjectPoolManager.Create("Ball", ballPrefab, ballSize);
	}
}
