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
		uiLabel.text = text;
	}
}
