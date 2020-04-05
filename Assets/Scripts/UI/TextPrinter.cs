using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPrinter : MonoBehaviour
{
	[SerializeField]
	private UILabel		uiLabel;		// 라벨


	// 텍스트 출력
	public void PrintText(string text)
	{
		uiLabel.text = "";
		StartCoroutine(PrintEachText(text));
	}

	// 텍스트 한글자씩 출력 코루틴
	private IEnumerator PrintEachText(string text)
	{
		for (int i = 0; i < text.Length; i++)
		{
			uiLabel.text += text[i];

			yield return null;
		}
	}
}
