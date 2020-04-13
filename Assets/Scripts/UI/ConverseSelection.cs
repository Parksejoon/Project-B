using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverseSelection : MonoBehaviour
{
	public delegate void ChoiceCallback();

	private ChoiceCallback[] callbacks;		// 선택시 호출할 콜백 함수 목록

	[SerializeField]
	private GameObject	choicePrefab;       // 선택지 프리팹

	private int			choiceCount;		// 선택지 갯수


	// 선택지 생성
	public void CreateChoices(ChoiceCallback[] _callbacks)
	{
		gameObject.SetActive(true);

		choiceCount = callbacks.Length;
		callbacks = _callbacks;

		for (int i = 0; i < choiceCount; i++)
		{
			// (!! 오브젝트 풀 사용 !!)
			Instantiate(choicePrefab, transform);
		}
	}

	// 선택
	public void ChooseChoice(int index)
	{
		callbacks[index]();
		gameObject.SetActive(false);
	}
}
