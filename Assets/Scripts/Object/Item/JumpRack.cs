using UnityEngine;

public class JumpRack : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private		Vector2			jumpWay;            // 점프 방향
	private		float			jumpPower = 20;		// 점프 파워


	private void Awake()
	{
		Vector3 rotation = transform.parent.rotation.eulerAngles;
		float angle = rotation.z * Mathf.PI / 180;

		jumpWay = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
	}


	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();

			targetRigidbody.velocity = Vector2.zero;
			targetRigidbody.AddForce(jumpWay * jumpPower, ForceMode2D.Impulse);
		}
	}
}
