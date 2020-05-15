using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObj : MonoBehaviour
{
	public float temp;

	// 초기화
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	// 시작
	private void Start()
	{
	}
}
