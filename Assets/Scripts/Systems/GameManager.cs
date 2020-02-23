/*


##### 여기에 대부분의 필요한 정보를 메모할 예정 #####

## 주석 관련 규칙 ##
(!! 수정이 필요한 내용 !!)
(@@ 코드 삽입시 주의가 필요한 부분 @@)

## sprite z value ##
 -4.8 NoCreateZone
 -4.9 CustomBlock
 -5 Base
 -5.1 Danger
 -5.2 Sub
 -6 Monster
 -7 Player
 -7.5 Effect
 -8 Fairy(Light)
 (!! #define으로 정형화 !!)
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
