using UnityEngine;

public class Item : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private		GameObject		targetObject;           // 아이템 오브젝트

	// 수치
	[SerializeField]
	private		int				itemCount;				// 아이템 개수

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 플레이어 = 아이템 획득
		if (collision.CompareTag("Player"))
		{
			InventoryManager.instance.AddItem(targetObject, itemCount);

			Destroy(gameObject);
		}
	}
}
