using UnityEngine;

public class Teleport : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private		Transform	playerTransform;			// 플레이어 트랜스폼


	// 초기화
	private void Awake()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// 시작
	private void Start()
	{
		Vector3 newPosition = transform.position;

		newPosition.y += transform.localScale.y / 2;
		playerTransform.position = newPosition;
	}
}
