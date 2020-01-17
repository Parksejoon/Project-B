using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField]
	private		GameObject		targetObject;           // 아이템 오브젝트
	[SerializeField]
	private		GameObject		destroyParticle;		// 파괴 이펙트
	[SerializeField]
	private		int				itemCount;				// 아이템 개수


	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 플레이어 = 아이템 획득
		if (collision.CompareTag("Player"))
		{
			if (InventoryManager.instance != null)
			{
				if (InventoryManager.instance.AddItem(targetObject, itemCount))
				{
					Vector3 position = PlayerController.playerTransform.position;
					//Vector3 position = transform.position;

					position.z = -15;
					Instantiate(destroyParticle, position, Quaternion.identity, PlayerController.playerTransform);

					Destroy(transform.parent.gameObject);
				}
			}
		}
	}
}
