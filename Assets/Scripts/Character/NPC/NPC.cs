using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
// 기본적인 대화, 선택지 선택
public abstract class NPC : MonoBehaviour, IInteractedHandler
{
	public TextPrinter			textPrinterWindow;			// 텍스트 출력 UI
	public ConverseSelection	converseSelectionWindow;	// 선택지 UI

	private bool		onInteraction = false;				// 상호작용 중인지 체크하는 플래그
	private Interacter	currentInteracter = null;           // 현재 상호작용중인 상호작용자


	// 대화 시작 함수
	public abstract void StartConverse();

	// 추가 상호작용
	public abstract void ExtraInteract();

	// 상호작용 받음
	public void Interact(Interacter interacter)
	{
		// 상호작용 설정
		currentInteracter = interacter;
		
		// 대화 시작
		StartConverse();
	}

	// 상호작용 끝
	public virtual void EndInteract()
	{
		currentInteracter.EndInteract();
		currentInteracter = null;
	}
}
