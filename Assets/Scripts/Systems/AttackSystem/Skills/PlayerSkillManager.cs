using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어의 스킬 관리
// 키 입력, 스킬 쿨타임, UI 텍스쳐 등
public class PlayerSkillManager : MonoBehaviour
{
	[SerializeField]
	private		UITexture[]		skillCoolDownTextures;			// 스킬 ui 텍스쳐 목록
	private		Skill[]			skills;							// 스킬 목록
	protected	bool[]			isCoolDowningFlags;             // 스킬 쿨다운 플래그 목록


	// 초기화
	private void Awake()
	{
		SetSkillList();

		isCoolDowningFlags = new bool[skills.Length];
		for (int i = 0; i < isCoolDowningFlags.Length; i++)
		{
			isCoolDowningFlags[i] = false;
		}
	}

	// 프레임
	private void Update()
	{
		// (## 스킬 키 ##)
		// 임시로 Q E에만 할당
		if (Input.GetKeyDown(KeyCode.Q))
		{
			ShotSkill(0);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			ShotSkill(1);
		}
	}

	// index의 스킬 사용
	private void ShotSkill(int index)
	{

		// 쿨타임 확인
		if (!isCoolDowningFlags[index])
		{
			// 스킬 사용
			skills[index].ShotSkill();
			StartCoroutine(CoolDown(index));
		}
	}

	// 스킬 목록 초기화
	// (** 나중에 SkillCode를 불러와서 초기화하는 방식으로 변경 **)
	private void SetSkillList()
	{
		skills = new Skill[4];

		skills[0] = SkillManager.GetSkill(SkillCode.Skill_BlockReset);
		skills[1] = SkillManager.GetSkill(0);
	}

	// 쿨다운
	protected IEnumerator CoolDown(int index)
	{
		isCoolDowningFlags[index] = true;

		yield return new WaitForSeconds(skills[index].CoolDownTime);

		isCoolDowningFlags[index] = false;
	}
}
