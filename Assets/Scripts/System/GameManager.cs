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

		// 그래픽 설정 ( 나중에 옮기세요 )
		Application.targetFrameRate = 60;
	}

	// 게임 오버
	public void GameOver()
	{
		Debug.Log("GameOver");
	}
}
