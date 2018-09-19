using System.Collections;
using UnityEngine;

public class ObjectGiver : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반 
	[SerializeField]
	private Slot				targetSlot;				// 지급될 슬롯
	[SerializeField]
	private GameObject			targetObject;			// 지급 아이템

	// 수치
	[SerializeField]
	private float				givePeriod;             // 지급 주기
	[SerializeField]
	private int					maxItemCount;			// 최대 아이템 개수


	// 초기화
	private void Awake()
	{
		targetSlot = GetComponent<Slot>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine("GiveBlock");
	}

	// 블럭 지급 코루틴
	private IEnumerator GiveBlock()
	{
		while (targetSlot.itemCount >= maxItemCount)
		{
			yield return null;
		}

		yield return new WaitForSeconds(givePeriod);

		targetSlot.AddItem(targetObject, 1);

		StartCoroutine("GiveBlock");
	}
}
