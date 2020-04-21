using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
// 기본적인 대화, 선택지 선택
public abstract class NPC : MonoBehaviour, IInteractedHandler
{
	private delegate void ConverseFunction();			// 대화 큐에 사용될 델리게이트

	// 텍스트 출력기
	[SerializeField] TextPrinter textPrinter; public TextPrinter TextPrinter		
	{
		get { return textPrinter; }
		private set { textPrinter = value; }
	}

	// 선택지
	[SerializeField] ConverseSelection converseSelection; public ConverseSelection ConverseSelection
	{
		get { return converseSelection; }
		private set { converseSelection = value; }
	}

	private bool onInteraction = false;						// 상호작용 중인지 체크하는 플래그
	private Interacter currentInteracter = null;            // 현재 상호작용중인 상호작용자

	private Queue<ConverseFunction> converseQueue;          // 대화 진행 큐


	// NPC에게 대화를 거는 함수
	public abstract void Converse();

	// 텍스트 창 출력
	protected virtual void OnTextPrintWindow(Queue<string> textQueue, string title)
	{
		// 대화 창 출력
		textPrinter.PrintTextQueue(textQueue, title);
	}

	// 선택지 창 출력
	protected void OnConverseSelectionWindow(ConverseSelection.ChoiceCallback[] callback)
	{
		converseSelection.CreateChoices(callback);
	}

	// 상호작용 받음
	public void Interact(Interacter interacter)
	{
		// 상호작용 설정
		currentInteracter = interacter;
		
		Converse();
	}

	// 추가 상호작용
	public void ExtraInteract()
	{
		if (converseQueue.Count > 0)
		{
			textPrinter.NextConverse();
		}
		else
		{
			// 상호작용 end
			currentInteracter.EndInteract();
		}
	}
}
