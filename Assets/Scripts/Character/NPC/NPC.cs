using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
public abstract class NPC : MonoBehaviour, IInteractionHooker
{
	private Interacter currentInteracter = null;			// 현재 상호작용중인 상호작용자


	[SerializeField] TextPrinter textPrinter; public TextPrinter TextPrinter		// 텍스트 출력기
	{
		get { return textPrinter; }
		private set { textPrinter = value; }
	}

	// 상호작용 받음
	public void Interact(Interacter interacter)
	{
		currentInteracter = interacter;

		Interacter.InteractionKeyHookCallback targetCallback = InteractKeyDown;

		currentInteracter.HookInteractKey(targetCallback);
		Converse();
	}
	
	// NPC에게 대화를 거는 함수
	public abstract TextPrinter Converse();

	// 상호작용 키 후킹용 델리게이트 함수
	public void InteractKeyDown()
	{
		if (textPrinter.PressConverse() == false)
		{
			currentInteracter.HookInteractKey(null);
		}
	}

	// 대화 창 출력
	protected virtual void OnConverseWindow(Queue<string> textQueue)
	{
		// 대화 창 출력
		textPrinter.gameObject.SetActive(true);
		textPrinter.PrintTextQueue(textQueue);


	}
}
