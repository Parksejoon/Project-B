using UnityEngine;

public class SkillManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
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
	public abstract void ShotSkill();
}
