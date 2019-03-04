using UnityEngine;
using System.Collections;

public class Buble : MonoBehaviour
{
	[SerializeField]
	private Collider2D	targetCollider;         // 활성화 할 콜리전
	[SerializeField]
	private float		destoryTime = 6;		// 파괴 시간


	// 시작
	private void Start()
	{
		StartCoroutine(Destroyer());
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("DangerBlock")
			|| collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("SoilBlock") 
			|| collision.gameObject.CompareTag("CustomBlock"))
		{
			targetCollider.enabled = true;
		}
	}

	// 파괴 카운트다운
	private IEnumerator Destroyer()
	{
		yield return new WaitForSeconds(destoryTime);

		Destroy(gameObject);
	}
}
