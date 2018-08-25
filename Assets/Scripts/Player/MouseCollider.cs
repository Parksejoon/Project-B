using UnityEngine;

public class MouseCollider : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("DangerBlock"))
		{
			GameManager.instance.GameOver();
		}
	}
}
