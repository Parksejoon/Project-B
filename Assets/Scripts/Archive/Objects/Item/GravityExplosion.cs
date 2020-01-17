using UnityEngine;

public class GravityExplosion : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Ball") || collision.CompareTag("Monster"))
		{
			collision.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position) * 4f, ForceMode2D.Impulse);
		}

		if (collision.CompareTag("BallGenerator"))
		{
			Destroy(collision.gameObject);
		}
	}
}
