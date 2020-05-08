using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPGauge : MonoBehaviour
{
	// 싱글톤
	public static HPGauge instance;

	public		UISlider	hpSlider;       // 체력 슬라이더

	
	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			hpSlider = GetComponentInChildren<UISlider>();
		}
	}

	// 체력 설정
	public void SetHpSlider(float hp)
	{
		hpSlider.value = hp;
	}
}
