using System.Collections;
using UnityEngine;


// 모든 스킬의 이름/번호 열거형
public enum SkillCode
{
	Skill_None = 0,
	Skill_BlockReset = 1
}

// 해당 씬에서 사용될 스킬들을 관리
// 시작시 불러온 스킬들을 저장
public class SkillManager : MonoBehaviour
{
	//public static SkillManager instance;		// 싱글톤

	private static Skill[]	skills;				// 모든 스킬 목록


	// 초기화
	private void Awake()
	{
		//if (instance == null)
		//{
		//	instance = this;
		//}
		//else
		//{
		//	Destroy(this);
		//}

		skills = GetComponents<Skill>();
	}

	// 스킬 목록에서 가져오기
	public static Skill GetSkill(SkillCode skillCode)
	{
		int index = (int)skillCode;

		if (skills.Length > index)
		{
			return skills[index];
		}
		else
		{
			return skills[0];
		}
	}
}

public abstract class Skill : MonoBehaviour
{
	// 변수
	private		float		coolDownTime;       // 쿨타임
	public		float		CoolDownTime
	{
		get { return coolDownTime; }
		protected set { coolDownTime = value; }
	}

	public		UITexture	skillTexture;       // 스킬 ui 텍스쳐


	// 스킬 사용
	public abstract void ShotSkill();

	//// UI 쿨다운
	//protected IEnumerator UICoolDown()
	//{
	//	skillCoolDownTexture.fillAmount = 1;

	//	while (isCoolDowning)
	//	{
	//		skillCoolDownTexture.fillAmount -= Time.smoothDeltaTime / CoolDownTime;

	//		yield return null;
	//	}

	//	skillCoolDownTexture.fillAmount = 0;
	//}
}
