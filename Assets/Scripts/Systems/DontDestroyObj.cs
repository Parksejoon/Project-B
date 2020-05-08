using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObj : MonoBehaviour
{

	// 초기화
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
