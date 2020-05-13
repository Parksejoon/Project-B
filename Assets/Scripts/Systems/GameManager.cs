/*


##### 여기에 대부분의 필요한 정보를 메모할 예정 #####

## 주석 관련 규칙 ##
(!! 수정이 필요한 내용 !!)
(@@ 코드 삽입시 주의가 필요한 부분 @@)
(## 키/마 입력에서 패드 호환 가능으로 수정이 필요한 내용 ##)
($$ 테스트용으로 만들어둔 코드 $$)

*/

using UnityEngine;

public enum SceneNumber
{
	BattleScene = 0,
	VillageScene = 1
}

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

