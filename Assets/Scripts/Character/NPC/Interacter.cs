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
		if (InputManager.GetButtonDown("Interaction"))
		{
			// 상호작용 진행중이 아니면
			if (inInteraction == false)
			{
				// 상호작용 시도
				Interact();
			}
		}
	}

	// 상호작용 종료
	public void EndInteract()
	{
		// 입력 제어 해제
		InputManager.SetAxis("Vertical", true);
		InputManager.SetAxis("Horizontal", true);
		InputManager.SetAxis("Jump", true);
		InputManager.SetAxis("Setblock", true);

		inInteraction = false;
	}

	// 상호작용 시작
	private void StartInteract(IInteractionHandler interactionHandler)
	{
		// 상호작용 시작
		interactionHandler.Interact(this);
		inInteraction = true;

		// 입력 제어
		InputManager.SetAxis("Vertical", false);
		InputManager.SetAxis("Horizontal", false);
		InputManager.SetAxis("Jump", false);
		InputManager.SetAxis("Setblock", false);
	}

	// 주위에 상호 작용 가능한 개체를 찾아 상호작용을 진행
	private void Interact()
	{
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0);
		IInteractionHandler interactionHandler = null;

		// 모든 충돌체를 확인
		foreach (Collider2D collider in hitColliders2D)
		{
			interactionHandler = collider.GetComponent<IInteractionHandler>();

			// 충돌체가 상호작용 가능하면
			if (interactionHandler != null)
			{
				StartInteract(interactionHandler);

				break;
			}
		}
	}
}
