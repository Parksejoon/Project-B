using UnityEngine;

public class Pause : MonoBehaviour
{
	// 정지
	public void Stop()
	{
		Time.timeScale = 0f;
	}

}
