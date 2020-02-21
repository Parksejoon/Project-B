using UnityEngine;

// 페어리 움직임
// 마우스 체이싱, 페어리 충돌처리, 카메라 이탈처리
public class FairyMove : MonoBehaviour
{
	[SerializeField]
	private GameObject		mouseBombEffect;			// 마우스 폭발 이펙트
	[SerializeField]
	private float			zPosition = 0;				// z 포지션
	[SerializeField]
	private float			speed = 1;                  // 속도
	
	private const float		colliderRadius = 0.3f;		// 충돌체 반지름
	

	// 고정 프레임
	private void FixedUpdate()
	{
		// 마우스 체이싱
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
		CheckCollider(new Vector2(goalVector.x, transform.position.y));
		CheckCollider(new Vector2(transform.position.x, goalVector.y));

		// 카메라 처리
		PreventionCameraDeviation();
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//if (collision.gameObject.CompareTag("DangerBlock"))
		//{
		//	// 마우스 충돌 이펙트
		//	Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//	mousePos.z = -9;

		//	Instantiate(mouseBombEffect, mousePos, Quaternion.identity);


		//	// 게임 종료
		//	GameManager.instance.GameOver();
		//}
	}

	// 카메라 밖으로 못나가게 조정
	private void PreventionCameraDeviation()
	{
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

		if (pos.x < 0f) pos.x = 0f;
		if (pos.x > 1f) pos.x = 1f;
		if (pos.y < 0f) pos.y = 0f;
		if (pos.y > 1f) pos.y = 1f;

		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	// 충돌 처리
	private void CheckCollider(Vector3 pos)
	{
		Collider2D[] colliders;

		pos.z = zPosition;

		// 충돌처리
		colliders = Physics2D.OverlapCircleAll(pos, colliderRadius);

		foreach (Collider2D collider in colliders)
		{

		}

		transform.position = pos;
	}
}
