using UnityEngine;

public class Pause : MonoBehaviour
{
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
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;

			pauseMenu.SetActive(false);
		}
		else
		{
			Time.timeScale = 0f;

			pauseMenu.SetActive(true);
		}
	}

}
