using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Ball"))
		{
			collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * 10f, ForceMode2D.Impulse);
		}

		if (collision.CompareTag("BallGenerator"))
		{
			Destroy(collision.gameObject);
		}
	}
}
