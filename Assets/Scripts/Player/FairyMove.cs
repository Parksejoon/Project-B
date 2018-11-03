using UnityEngine;

public class FairyMove : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	mouseBombEffect;				// 마우스 폭발 이펙트
	
	// 수치
	[SerializeField]
	private float zPosition = 0;						// z 포지션
	[SerializeField]
	private float speed = 1;                            // 속도

	// 인스펙터 비노출 변수
	// 수치
	private const float colliderRadius = 0.3f;			// 충돌체 반지름
	

	// 고정 프레임
	private void FixedUpdate()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 goalVector;

		// 거리 계산 및 goalVector계산
		if (Vector2.Distance(transform.position, position) < 1)
		{
			goalVector = Vector2.Lerp(transform.position, new Vector2(position.x, position.y), Time.deltaTime * speed);
		}
		else
		{
			goalVector = Vector2.MoveTowards(transform.position, new Vector2(position.x, position.y), Time.deltaTime * speed);
		}
		goalVector.z = zPosition;

		// 충돌 처리
		//CheckCollider(goalVector);
		CheckCollider(new Vector2(goalVector.x, transform.position.y));
		CheckCollider(new Vector2(transform.position.x, goalVector.y));
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("DangerBlock"))
		{
			// 마우스 충돌 이펙트
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			mousePos.z = -9;

			Instantiate(mouseBombEffect, mousePos, Quaternion.identity);


			// 게임 종료
			GameManager.instance.GameOver();
		}
	}

	// 충돌 처리
	private void CheckCollider(Vector3 pos)
	{
		Collider2D[] colliders;

		// 충돌처리
		colliders = Physics2D.OverlapCircleAll(pos, colliderRadius);

		foreach (Collider2D collider in colliders)
		{
			if (collider.CompareTag("Block") || collider.CompareTag("DangerBlock") || collider.CompareTag("SoilBlock"))
			{
				return;
			}
		}

		transform.position = pos;
	}
}
