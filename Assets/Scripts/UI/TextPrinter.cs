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
	private UILabel			uiText;					// 텍스트 라벨
	[SerializeField]
	private UILabel			uiTitle;                // 타이틀 라벨
	[SerializeField]
	private float			printDelay = 0.01f;	// 출력 딜레이

	private IEnumerator		printEachTextRoutine;   // 한글자씩 출력하는 코루틴

	private Queue<string>	textQueue;				// 출력 텍스트 큐
	private string			currentText;            // 현재 문자열
	private PrintStatus		printStats;             // 출력 상태


	// 초기화
	private void Awake()
	{
		printEachTextRoutine = PrintEachText();

		currentText = "";
		printStats = PrintStatus.Nothing;
	}

	/// <summary>
	/// 추가적인 입력에 대해 텍스트 출력을 진행
	/// true: 출력중 / false: 출력종료
	/// </summary>
	public bool ProgressConverse()
	{
		// 출력 상태 (0: 미출력, 1: 출력중, 2: 출력완료)
		switch (printStats)
		{
			case PrintStatus.Nothing:
				Debug.Log("Now, text does not printing");
				return false;

			case PrintStatus.Printing:
				return SkipPrint();

			case PrintStatus.Done:
				return PrintText();

			default:
				return false;
		}
	}
	
	// 출력 스킵
	private bool SkipPrint()
	{
		// 출력중인 텍스트 스킵
		StopCoroutine(printEachTextRoutine);
		uiText.text = currentText;

		// 큐에 텍스트가 남아있으면
		if (textQueue.Count > 0)
		{
			// 상태 초기화
			printStats = PrintStatus.Done;

			return true;
		}
		// 큐가 비어있으면
		else
		{
			// 출력 종료
			EndPrint();

			return false;
		}
	}
	
	// 출력 종료
	private void EndPrint()
	{
		// 상태 초기화
		printStats = PrintStatus.Nothing;
	}

	// 텍스트 출력
	private bool PrintText()
	{
		// 큐에 텍스트가 남아있으면
		if (textQueue.Count > 0)
		{
			// 텍스트 초기화
			uiText.text = "";

			// 큐에 있는 새로운 텍스트 출력
			currentText = textQueue.Dequeue();
			printEachTextRoutine = PrintEachText();
			StartCoroutine(printEachTextRoutine);

			// 상태 초기화
			printStats = PrintStatus.Printing;

			return true;
		}
		// 큐가 비어있으면
		else
		{
			return false;
		}
	}
	
	/// <summary>
	/// 출력될 텍스트 큐를 설정
	/// </summary>
	public void SetTextQueue(Queue<string> text, string title)
	{
		// 초기화
		gameObject.SetActive(true);
		StopCoroutine(printEachTextRoutine);

		// 텍스트 초기화
		uiText.text = "";
		textQueue = new Queue<string>(text);

		// 타이틀 초기화
		uiTitle.text = title;

		// 첫 텍스트 출력
		PrintText();
	}

	// 텍스트 한글자씩 출력 코루틴
	private IEnumerator PrintEachText()
	{
		for (int i = 0; i < currentText.Length; i++)
		{
			uiText.text += currentText[i];

			yield return new WaitForSeconds(printDelay);
		}

		// 상태 초기화
		printStats = PrintStatus.Done;
	}
}
