using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
public abstract class NPC : MonoBehaviour, IInteractionHandler
{
	private Interacter currentInteracter = null;			// 현재 상호작용중인 상호작용자

	[SerializeField] TextPrinter textPrinter; public TextPrinter TextPrinter		// 텍스트 출력기
	{
		get { return textPrinter; }
		private set { textPrinter = value; }
	}


	// NPC에게 대화를 거는 함수
	public abstract TextPrinter Converse();

	// 대화 창 출력
	protected virtual void OnConverseWindow(Queue<string> textQueue, string title)
	{
		// 종료 콜백 함수 설정
		textPrinter.SetCallbackFunction(currentInteracter.EndInteract);

		// 대화 창 출력
		textPrinter.PrintTextQueue(textQueue, title);
	}

	// 상호작용 받음
	public void Interact(Interacter interacter)
	{
		currentInteracter = interacter;
		
		Converse();
	}
}
