using System.Collections;
using UnityEngine;

public class ObjectGiver : MonoBehaviour
{
	[SerializeField]
	private Slot				targetSlot;				// 지급될 슬롯
	[SerializeField]
	private GameObject			targetObject;			// 지급 아이템
	[SerializeField]
	private float				givePeriod;             // 지급 주기


	// 초기화
	private void Awake()
	{
		targetSlot = GetComponent<Slot>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(GiveBlock());
	}

	// 블럭 한번에 지급
	public void GiveAllBlock()
	{
		while (targetSlot.itemCount < InventoryManager.instance.maxItemCount)
		{
			targetSlot.AddItem(targetObject, 1);
		}
	}

	// 블럭 지급 코루틴
	private IEnumerator GiveBlock()
	{
		while (targetSlot.itemCount >= InventoryManager.instance.maxItemCount)
		{
			yield return null;
		}

		while (!PlayerJumpManager.IsGround)
		{
			yield return null;
		}

		yield return new WaitForSeconds(givePeriod);
		
		targetSlot.AddItem(targetObject, 1);

		StartCoroutine(GiveBlock());
	}
}
