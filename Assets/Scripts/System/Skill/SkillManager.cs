using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	[SerializeField]
	private	UITexture	skillCoolDownTextureQ;          // q스킬 ui 텍스쳐
	[SerializeField]
	private UITexture	skillCoolDownTextureE;			// e스킬 ui 텍스쳐

	public	Skill		skillQ;							// q스킬
	public	Skill		skillE;							// e스킬


	// 초기화
	private void Awake()
	{
		if (skillQ == null)
		{
			skillQ = new Skill();
		}

		if (skillE == null)
		{
			skillE = new Skill();
		}

		skillQ.skillCoolDownTexture = skillCoolDownTextureQ;
		skillE.skillCoolDownTexture = skillCoolDownTextureE;
	}

	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			skillQ.ShotSkill();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			skillE.ShotSkill();
		}
	}
}

public class Skill : MonoBehaviour
{
	// 변수
	[SerializeField]
	protected	float		CoolDownTime;                   // 쿨타임

	protected	bool		isCoolDowning = false;          // 쿨다운 중인지

	public		UITexture	skillCoolDownTexture;           // 스킬 ui 텍스쳐


	// 스킬 사용
	public virtual void ShotSkill()
	{
		Debug.Log("NONE SKILL");
	}
	
	// UI 쿨다운
	protected IEnumerator UICoolDown()
	{
		skillCoolDownTexture.fillAmount = 1;

		while (isCoolDowning)
		{
			skillCoolDownTexture.fillAmount -= Time.smoothDeltaTime / CoolDownTime;

			yield return null;
		}

		skillCoolDownTexture.fillAmount = 0;
	}

	// 쿨다운
	protected IEnumerator CoolDown()
	{
		isCoolDowning = true;

		StartCoroutine(UICoolDown());
		yield return new WaitForSeconds(CoolDownTime);

		isCoolDowning = false;
	}
}
