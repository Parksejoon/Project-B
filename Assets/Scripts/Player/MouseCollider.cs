using UnityEngine;

public class MouseCollider : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	mouseBombEffect;

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("DangerBlock"))
		{
			// 마우스 충돌 이펙트
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			mousePos.z = -9;

			Instantiate(mouseBombEffect, mousePos, Quaternion.identity);

			Debug.Log("ASD");

			// 게임 종료
			GameManager.instance.GameOver();
		}
	}
}
