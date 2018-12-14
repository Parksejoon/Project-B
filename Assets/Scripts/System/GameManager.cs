using UnityEngine;

// sprite z value
// -4.8 NoCreateZone
// -4.9 CustomBlock
// -5 Base
// -5.1 Danger
// -5.2 Sub
// -6 Monster
// -7 Player
// -7.5 Effect
// -8 Fairy(Light)

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
