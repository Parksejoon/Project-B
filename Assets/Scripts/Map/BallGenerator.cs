using System.Collections;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
	// 일반
	[SerializeField]
	private GameObject		ballPrefab;					// 라바 볼

	// 수치
	[SerializeField]
	private float			createDelay;				// 생성 주기
	[SerializeField]
	private Vector2			shotWay;					// 발사 방향
	[SerializeField]
	private float			shotPower;                  // 발사 힘
	[SerializeField]
	private float			zPosition = 0;				// z 포지션


	// 초기화
	private void Awake()
	{
		StartCoroutine(GenerateCoroutine());
	}

	// 라바 볼 생성
	private void GenerateLavaBall()
	{
		Vector3 position = transform.position;

		position.z = zPosition;
		Rigidbody2D target = Instantiate(ballPrefab, position, Quaternion.identity).GetComponent<Rigidbody2D>();

		target.AddForce(shotWay * shotPower);
	}

	// 생성 반복 코루틴
	private IEnumerator GenerateCoroutine()
	{
		yield return new WaitForSeconds(createDelay);

		GenerateLavaBall();

		StartCoroutine(GenerateCoroutine());
	}
}