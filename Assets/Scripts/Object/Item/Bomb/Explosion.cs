using UnityEngine;

public class Explosion : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Ball"))
		{
			collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 5f, ForceMode2D.Impulse);
		}

		Debug.Log(collision.tag);

		if (collision.CompareTag("BallGenerator"))
		{
			Debug.Log("asdf");
			Destroy(collision.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.tag);
	}
}
