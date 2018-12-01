using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	public Skill	skillQ;           // q스킬


	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			skillQ.ShotSkill();
		}
	}
}

public abstract class Skill : MonoBehaviour
{
	// 변수
	[SerializeField]
	protected float		CoolDownTime;                   // 쿨타임
	protected bool		isCoolDowning = false;			// 쿨다운 중인지

	// 함수
	public abstract void ShotSkill();					// 스킬 사용
	protected abstract IEnumerator CoolDown();			// 쿨다운 코루틴
}
