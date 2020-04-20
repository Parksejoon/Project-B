using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 상호작용을 가능하게 해주는 클래스
public class Interacter : MonoBehaviour
{
	private bool interactionAxisInUse = false;      // 상호작용 키 입력 플래그

	private IInteractedHandler currentHandler;		// 현재 잡고있는 상호작용 핸들러

	// 입력 제어용 스크립트 목록
	[SerializeField]
	private PlayerController		playerController;
	[SerializeField]
	private CustomBlockBuilder		customBlockBuilder;
	
	// 프레임
	private void Update()
	{
		if (InputManager.GetButtonDown("Interaction"))
		{
			// 상호작용 진행중이 아니면
			if (currentHandler == null)
			{
				// 상호작용 시도
				Interact();
			}
			// 상호작용 진행중이면
			else
			{
				// 추가 상효작용
				currentHandler.ExtraInteract();
			}
		}
	}

	// 상호작용 종료
	public void EndInteract()
	{
		// 입력 제어 해제
		playerController.enabled = true;
		customBlockBuilder.enabled = true;

		currentHandler = null;
	}

	// 상호작용 시작
	private void StartInteract(IInteractedHandler interactedHandler)
	{
		// 상호작용 시작
		interactedHandler.Interact(this);

		// 핸들러 설정
		currentHandler = interactedHandler;

		// 입력 제어
		playerController.enabled = false;
		customBlockBuilder.enabled = false;
	}

	// 주위에 상호 작용 가능한 개체를 찾아 상호작용을 진행
	private void Interact()
	{
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0);
		IInteractedHandler interactedHandler = null;

		// 모든 충돌체를 확인
		foreach (Collider2D collider in hitColliders2D)
		{
			interactedHandler = collider.GetComponent<IInteractedHandler>();

			// 충돌체가 상호작용 가능하면
			if (interactedHandler != null)
			{
				StartInteract(interactedHandler);

				break;
			}
		}
	}
}
