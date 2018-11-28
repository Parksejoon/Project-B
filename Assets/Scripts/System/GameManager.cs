using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ObjectGiver			normalBlockGiver;	// 일반 블럭 공급자


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 일반 블록 공급 초기화
	public void NormalBlockReset()
	{
		normalBlockGiver.GiveAllBlock();
	}

	// 게임 오버
	public void GameOver()
	{
		Debug.Log("GameOver");
	}
}
