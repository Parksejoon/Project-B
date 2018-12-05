using UnityEngine;

public class Buble : MonoBehaviour
{
	[SerializeField]
	private Collider2D	targetCollider;         // 활성화 할 콜리전


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
}
