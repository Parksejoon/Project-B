using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneChanger : MonoBehaviour
{
	[SerializeField]
	private SceneNumber targetScene;

	// 시작
	private void Start()
	{
		SceneManager.LoadScene((int)targetScene);
	}
}
