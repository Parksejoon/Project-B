using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelecter : MonoBehaviour
{
	// 꿈 시작
	public void StartDream()
	{
		SceneManager.LoadScene("MainFightScene");
	}
}
