using UnityEngine;

public class UnstableBlock : MonoBehaviour
{
	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ball"))
		{
			Destroy(gameObject);
		}
	}
}
