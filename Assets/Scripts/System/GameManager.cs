using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 게임 오버
	public void GameOver()
	{
		Debug.Log("GameOver");
	}
}
