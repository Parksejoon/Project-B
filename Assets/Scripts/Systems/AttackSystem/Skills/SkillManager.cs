using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	[SerializeField]
	private		UITexture[]	skillCoolDownTextures;      // 스킬 ui 텍스쳐 목록
	public		Skill[]		skills;						// 스킬 목록
	protected	bool[]		isCoolDowningFlags;			// 스킬 쿨다운 플래그 목록


	// 초기화
	private void Awake()
	{
		//if (skillQ == null)
		//{
		//	skillQ = new Skill();
		//}

		//if (skillE == null)
		//{
		//	skillE = new Skill();
		//}

		//skillQ.skillCoolDownTexture = skillCoolDownTextureQ;
		//skillE.skillCoolDownTexture = skillCoolDownTextureE;

		isCoolDowningFlags = new bool[skills.Length];
		for (int i = 0; i < isCoolDowningFlags.Length; i++)
		{
			isCoolDowningFlags[i] = false;
		}

		gameObject.AddComponent<BlockReset>();
	}

	// 프레임
	private void Update()
	{
		// (!! 현재 임시로 키보드 Q E에만 스킬 할당해놓음 !!)
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

	// 쿨다운
	protected IEnumerator CoolDown(int index)
	{
		isCoolDowningFlags[index] = true;
		
		yield return new WaitForSeconds(skills[index].CoolDownTime);

		isCoolDowningFlags[index] = false;
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
