using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
public abstract class NPC : MonoBehaviour
{
	[SerializeField]
	private TextPrinter	converseWindow;         // 대화 창


	// NPC에게 대화를 거는 함수
	public abstract void Converse();

	// 대화 창 출력
	protected virtual void OnConverseWindow()
	{
		// 대화 창 출력
		converseWindow.gameObject.SetActive(true);
		converseWindow.PrintText("Ahahahahahaha!\nYou so fuckin crazy guy!");
	}
}
