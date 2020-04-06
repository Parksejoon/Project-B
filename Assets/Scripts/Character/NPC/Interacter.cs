using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 상호작용을 가능하게 해주는 클래스
public class Interacter : MonoBehaviour
{
	public delegate void InteractionKeyHookCallback();  // 상호작용 키 후킹시 사용될 콜백 함수의 델리게이트

	private InteractionKeyHookCallback interactionKeyHookCallback;

	private bool interactionAxisInUse = false;      // 상호작용 키 입력 플래그


	// 초기화
	private void Awake()
	{
		interactionKeyHookCallback = null;
	}

	// 프레임
	private void Update()
	{
		// 입력 트리거
		if (Input.GetAxisRaw("Interaction") != 0)
		{
			if (interactionAxisInUse == false)
			{
				// 후킹중이 아니면
				if (interactionKeyHookCallback == null)
				{
					// 상호작용 시도
					Interact();
				}
				// 후킹에 걸려있으면
				else
				{
					// 지정된 델리게이트 실행
					interactionKeyHookCallback();
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

	// 상호작용 키 후킹
	public void HookInteractKey(InteractionKeyHookCallback func)
	{
		interactionKeyHookCallback = func;
	}

	// 주위에 상호 작용 가능한 개체를 찾아 상호작용을 진행
	private void Interact()
	{
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0);
		IInteractionHooker interactionHooker = null;

		// 모든 충돌체를 확인
		foreach (Collider2D collider in hitColliders2D)
		{
			interactionHooker = collider.GetComponent<IInteractionHooker>();

			// 충돌체가 상호작용 가능하면
			if (interactionHooker != null)
			{
				interactionHooker.Interact(this);

				break;
			}
		}
	}
}
