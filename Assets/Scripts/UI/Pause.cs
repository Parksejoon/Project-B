using UnityEngine;

public class Pause : MonoBehaviour
{
	public static bool isPause = false;					// 일시정지 상태 확인 변수

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private		GameObject		pauseMenu;              // 일시정지 메뉴화면


	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseOrContinue();
		}
	}

	// 정지
	public void PauseOrContinue()
	{
		if (isPause)
		{
			Time.timeScale = 1f;

			isPause = false;
			pauseMenu.SetActive(false);
		}
		else
		{
			Time.timeScale = 0f;

			isPause = true;
			pauseMenu.SetActive(true);
		}
	}

}
