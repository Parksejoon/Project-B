using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverseChoice : MonoBehaviour
{
	private		int					index;				// 선택지 인덱스
	private		UILabel				nameLabel;			// 선택지 이름 label
	private		UIEventTrigger		uIEventTrigger;     // 선택지 ui 이벤트 트리거
	private		ConverseSelection	converseSelection;	// 선택지 전체 제어 클래스

	// 초기화
	private void Awake()
	{
		nameLabel = GetComponentInChildren<UILabel>();
		uIEventTrigger = GetComponent<UIEventTrigger>();
		converseSelection = GetComponentInParent<ConverseSelection>();
	}

	// 셋팅
	public void SetChoice(int _index, string name)
	{
		index = _index;
		nameLabel.text = name;

		uIEventTrigger.onPress = new List<EventDelegate>() { new EventDelegate(Choose) };
	}

	// 선택지 선택시 실행될 함수
	public void Choose()
	{
		converseSelection.ChooseChoice(index);
	}
}
