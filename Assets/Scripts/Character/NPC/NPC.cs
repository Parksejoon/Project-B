using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
// 기본적인 대화, 선택지 선택
public abstract class NPC : MonoBehaviour, IInteractedHandler
{
	public delegate bool ConverseFunction();			// 대화 큐에 사용될 델리게이트

	// 텍스트 출력기
	[SerializeField] TextPrinter textPrinterWindow; public TextPrinter TextPrinterWindow	
	{
		get { return textPrinterWindow; }
		private set { textPrinterWindow = value; }
	}

	// 선택지
	[SerializeField] ConverseSelection converseSelectionWindow; public ConverseSelection ConverseSelectionWindow
	{
		get { return converseSelectionWindow; }
		private set { converseSelectionWindow = value; }
	}

	private bool onInteraction = false;						// 상호작용 중인지 체크하는 플래그
	private Interacter currentInteracter = null;            // 현재 상호작용중인 상호작용자

	protected Queue<ConverseFunction> converseQueue;		// 대화 진행 큐


	// 대화 시작 함수 (대화 진행 큐 설정)
	public abstract void StartConverse();

	// 상호작용 받음
	public void Interact(Interacter interacter)
	{
		// 상호작용 설정
		currentInteracter = interacter;
		
		// 대화 시작
		StartConverse();
	}

	// 추가 상호작용
	public void ExtraInteract()
	{
		if (converseQueue.Count > 0)
		{
			converseQueue.Dequeue()();
		}
		else
		{
			// 상호작용 end
			currentInteracter.EndInteract();
		}
	}
}
