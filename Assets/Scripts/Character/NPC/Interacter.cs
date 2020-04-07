using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 상호작용을 가능하게 해주는 클래스
public class Interacter : MonoBehaviour
{
	private bool interactionAxisInUse = false;      // 상호작용 키 입력 플래그
	private bool inInteraction = false;				// 상호작용 중인지 플래그


	// 프레임
	private void Update()
	{
		// 입력 트리거
		if (Input.GetAxisRaw("Interaction") != 0)
		{
			if (interactionAxisInUse == false)
			{
				// 상호작용 진행중이 아니면
				if (inInteraction == false)
				{
					// 상호작용 시도
					Interact();
				}
				
				// 플래그 설정
				interactionAxisInUse = true;
			}
		}

		// 입력 해제 트리거
		if (Input.GetAxisRaw("Interaction") == 0)
		{
			interactionAxisInUse = false;
		}
	}

	// 상호작용 종료
	public void EndInteract()
	{
		inInteraction = false;
	}

	// 주위에 상호 작용 가능한 개체를 찾아 상호작용을 진행
	private void Interact()
	{
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0);
		IInteractionHandler interactionHooker = null;

		// 모든 충돌체를 확인
		foreach (Collider2D collider in hitColliders2D)
		{
			interactionHooker = collider.GetComponent<IInteractionHandler>();

			// 충돌체가 상호작용 가능하면
			if (interactionHooker != null)
			{
				// 상호작용 시도
				interactionHooker.Interact(this);
				inInteraction = true;

				break;
			}
		}
	}
}
