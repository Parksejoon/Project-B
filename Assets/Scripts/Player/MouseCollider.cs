using UnityEngine;

public class MouseCollider : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameManager.instance.GameOver();
	}
}
