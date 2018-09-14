using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArea : MonoBehaviour
{
	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("BallGenerator"))
		{
			collision.GetComponent<BallGenerator>().enabledGenerate = true;
		}
	}

	// 트리거 탈출
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("BallGenerator"))
		{
			collision.GetComponent<BallGenerator>().enabledGenerate = false;
		}
	}
}
