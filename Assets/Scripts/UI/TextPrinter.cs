using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TextPrinter : MonoBehaviour
{
	// 출력 상태 열거형
	public enum PrintStatus
	{
		Nothing = 0,
		Printing = 1,
		Done = 2
	};

	[SerializeField]
	private UILabel			uiText;					// 라벨
	[SerializeField]
	private UILabel			uiTitle;				// 라벨

	private IEnumerator		printEachTextRoutine;   // 한글자씩 출력하는 코루틴

	private Queue<string>	textQueue;				// 출력 텍스트 큐
	private string			currentText;            // 현재 문자열
	private PrintStatus		printStats;             // 출력 상태

	// (## 스크립트를 넘어오자 마자 true가되서 바로 텍스트가 스킵되는 현상을 막기위해 초기화값을 true로 설정 ##)
	private bool interactionAxisInUse = true;      // 상호작용 키 입력 플래그 


	// 초기화
	private void Awake()
	{
		printEachTextRoutine = PrintEachText();

		currentText = "";
		printStats = PrintStatus.Nothing;
	}

	// 대화 계속하기
	public void NextConverse()
	{
		// 출력 상태 (0: 미출력, 1: 출력중, 2: 출력완료)
		switch (printStats)
		{
			case PrintStatus.Nothing:
				Debug.Log("Now, text does not printing");
				return;

			case PrintStatus.Printing:
				SkipPrint();
				return;

			case PrintStatus.Done:
				PrintText();
				return; 

			default:
				return;
		}
	}

	// 출력 스킵
	private void SkipPrint()
	{
		// 출력중인 텍스트 스킵
		StopCoroutine(printEachTextRoutine);
		uiText.text = currentText;

		// 상태 초기화
		printStats = PrintStatus.Done;
	}

	// 출력 종료
	private void EndPrint()
	{
		// 텍스트 초기화
		uiText.text = "";

		// 상태 초기화
		printStats = 0;

		// 키 입력 초기화
		interactionAxisInUse = true;

		// 오브젝트 비활성화
		gameObject.SetActive(false);
	}

	// 텍스트 출력
	private void PrintText()
	{
		// 텍스트 초기화
		uiText.text = "";

		// 큐에 텍스트가 남아있으면
		if (textQueue.Count > 0)
		{
			// 큐에 있는 새로운 텍스트 출력
			currentText = textQueue.Dequeue();
			printEachTextRoutine = PrintEachText();
			StartCoroutine(printEachTextRoutine);

			// 상태 초기화
			printStats = PrintStatus.Printing;
		}
		// 큐가 비어있으면
		else
		{
			// 출력 종료
			EndPrint();
		}
	}

	// 텍스트 큐 출력
	public void PrintTextQueue(Queue<string> text, string title)
	{
		// 초기화
		gameObject.SetActive(true);
		StopCoroutine(printEachTextRoutine);

		// 텍스트 초기화
		uiText.text = "";
		textQueue = new Queue<string>(text);

		// 타이틀 초기화
		uiTitle.text = title;

		// 텍스트 출력
		PrintText();
	}

	// 텍스트 한글자씩 출력 코루틴
	private IEnumerator PrintEachText()
	{
		for (int i = 0; i < currentText.Length; i++)
		{
			uiText.text += currentText[i];

			yield return new WaitForSeconds(0.1f);
		}

		// 상태 초기화
		printStats = PrintStatus.Done;
	}
}
