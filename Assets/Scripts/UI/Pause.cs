using UnityEngine;

public class Pause : MonoBehaviour
{
	// 정지
	public void StopOrStart()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;
		}
	}

}
