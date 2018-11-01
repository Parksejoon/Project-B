using UnityEngine;

public class MouseCollider : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	mouseBombEffect;			// 마우스 폭발 이펙트
	[SerializeField]
	private MouseChase	mouseChase;					// 마우스 추적


	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("ASD");
		
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

	// 충돌체 탈출
	private void OnCollisionExit2D(Collision2D collision)
	{

	}
}
