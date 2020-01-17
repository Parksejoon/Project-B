using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
	enum ItemType
	{
		Bomb,
		GBomb,
		PBomb,
		Rack,
		Teleport
	};

	[SerializeField]
	private ItemType itemType;			// 아이템 종류

	// 아이템들
	public GameObject bomb;				// 폭탄
	public GameObject g_bomb;			// 중력 폭탄
	public GameObject p_bomb;			// 정화 폭탄
	public GameObject rack;				// 점프대
	public GameObject teleport;         // 텔레포트


	// 시작
	private void Start()
	{
		switch (itemType)
		{
			case ItemType.Bomb:
				Instantiate(bomb, transform.position, Quaternion.identity, transform.parent);
				break;
			case ItemType.GBomb:
				Instantiate(g_bomb, transform.position, Quaternion.identity, transform.parent);
				break;
			case ItemType.PBomb:
				Instantiate(p_bomb, transform.position, Quaternion.identity, transform.parent);
				break;
			case ItemType.Rack:
				Instantiate(rack, transform.position, Quaternion.identity, transform.parent);
				break;
			case ItemType.Teleport:
				Instantiate(teleport, transform.position, Quaternion.identity, transform.parent);
				break;
			default:
				break;
		}

		Destroy(gameObject);
	}
}
