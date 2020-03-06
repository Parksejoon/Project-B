/*


##### 여기에 대부분의 필요한 정보를 메모할 예정 #####

## 주석 관련 규칙 ##
(!! 수정이 필요한 내용 !!)
(@@ 코드 삽입시 주의가 필요한 부분 @@)

*/

using UnityEngine;

// 전투 게임 매니저
// 게임 오버, 시작 초기화, 연출 연결
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

		// 그래픽 설정 (!! 나중에 그래픽 관련 스크립트로 옮기세요 !!)
		Application.targetFrameRate = 60;
	}

	// 게임 오버
	public void GameOver()
	{
		Debug.Log("GameOver");
	}
}

/*

## sprite z value ##

 -4.5 CustomBlock
 -5 Base
 -6 Monster
 -7 Player
 -7.5 Effect
 -8 Fairy & Light

 */
 

public static class Depth
{
	public const float CustomBlock	= -4.5f;
	public const float Base			= -5f;
	public const float Monster		= -6f;
	public const float Player		= -7f;
	public const float Effect		= -7.5f;
	public const float Fairy		= -8f;
	public const float Light		= -8f;
}
