using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager		instance;           // 싱글톤 인스턴스

	[SerializeField]
	private GameObject normalBlockItem;     // 드랍용 노말 블럭


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 노말 블럭 드랍
	public void DropNormalBlock(float shotWay)
	{
		if (InventoryManager.instance.UseItem(1) != null)
		{
			GameObject target = Instantiate(normalBlockItem, transform.position, Quaternion.identity);

			target.GetComponent<Rigidbody2D>().AddForce(new Vector2(shotWay * 2, 1.5f) * 7, ForceMode2D.Impulse);
		}
	}
}
