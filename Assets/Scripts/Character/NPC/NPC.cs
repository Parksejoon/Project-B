using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// NPC의 기본 클래스
public abstract class NPC : MonoBehaviour
{
	
	public TextPrinter	textPrinter;         // 대화 창
	//public TextPrinter TextPrinter
	//{
	//	get { return textPrinter; }
	//	private set { textPrinter = value; }
	//}


	// NPC에게 대화를 거는 함수
	public abstract TextPrinter Converse();

	// 대화 창 출력
	protected virtual void OnConverseWindow()
	{
		// 대화 창 출력
		textPrinter.gameObject.SetActive(true);
		textPrinter.PrintText("Ahhhhhhhhhhhh!\nYou so fuckin' precious when you smile\nABC");
	}
}
