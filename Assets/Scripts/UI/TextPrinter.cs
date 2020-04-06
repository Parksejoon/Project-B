using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextPrinter : MonoBehaviour
{

	[SerializeField]
	private UILabel		uiLabel;					// 라벨

	private IEnumerator printEachTextRoutine;       // 한글자씩 출력하는 코루틴

	private string		currentText;                // 현재 문자열

	private bool		interactionAxisInUse = false;		// 대화 입력 플래그


	// 초기화
	private void Awake()
	{
		printEachTextRoutine = PrintEachText();

		currentText = "";
	}

	// (## 대화 스킵 ##)
	public void SkipText()
	{
		StopCoroutine(printEachTextRoutine);
		uiLabel.text = currentText;
	}

	// 텍스트 출력
	public void PrintText(string text)
	{
		StopCoroutine(printEachTextRoutine);

		uiLabel.text = "";
		currentText = text;

		printEachTextRoutine = PrintEachText();
		StartCoroutine(printEachTextRoutine);
	}

	// 텍스트 한글자씩 출력 코루틴
	private IEnumerator PrintEachText()
	{
		for (int i = 0; i < currentText.Length; i++)
		{
			uiLabel.text += currentText[i];

			yield return new WaitForSeconds(0.1f);
		}
	}
}
