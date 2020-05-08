using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 꿈의 마녀 NPC
// 던전 진입, 꿈 조각 설정 NPC 
public class DreamWitch : NPC
{
	private bool printingFlag = false;			// 출력 플래그

	// NPC와 대화 시작
	public override void StartConverse()
	{
		// 초기화
		Queue<string> textQueue = new Queue<string>();

		textQueue.Enqueue("Ahhhhhhhhhhhh!\nYou so fuckin' precious when you smile\nABC");
		textQueue.Enqueue("Im waking up.\nI feel it in my bones.\nEnough to make my systems blow.");

		TextPrinterWindow.SetTextQueue(textQueue, "DreamWitch");
		ConverseSelectionWindow.SetChoices(new ConverseSelection.ChoiceCallback[] { Test1, Test2, Test3, Test4, Test5 },
										new string[] { "Fuck", "Shit", "Holy", "No", "Yes" });

		printingFlag = true;
	}

	// 추가 상호작용
	public override void ExtraInteract()
	{
		if (printingFlag)
		{
			if (!TextPrinterWindow.ProgressConverse())
			{
				ConverseSelectionWindow.EnableChoices();
				printingFlag = false;
			}
		}
		else
		{
		}
	}

	// 상호작용 종료
	public override void EndInteract()
	{
		TextPrinterWindow.DisablePrint();

		base.EndInteract();
	}

	// 테스트 함수
	public void Test1()
	{
		Debug.Log("Test1");
		EndInteract();
	}

	public void Test2()
	{
		Debug.Log("Test2");
		EndInteract();
	}

	public void Test3()
	{
		Debug.Log("Test3");
		EndInteract();
	}

	public void Test4()
	{
		Debug.Log("Test4");
		EndInteract();
	}

	public void Test5()
	{
		Debug.Log("Test5");
		EndInteract();
	}
}
