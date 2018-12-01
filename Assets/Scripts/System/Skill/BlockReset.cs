using System.Collections;
using UnityEngine;

public class BlockReset : Skill
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ObjectGiver normalBlockGiver;			// 일반 블럭 공급자

	// 인스펙터 비노출 변수
	private bool		isCoolDowning = false;		// 쿨다운 중인지


	// 스킬 사용
	public override void ShotSkill()
	{
		Debug.Log(isCoolDowning);

		if (PlayerManager.instance.isGround && !isCoolDowning)
		{
			StartCoroutine(CoolDown());

			normalBlockGiver.GiveAllBlock();
		}
	}

	// 쿨다운
	protected override IEnumerator CoolDown()
	{
		isCoolDowning = true;

		yield return new WaitForSeconds(CoolDownTime);

		isCoolDowning = false;
	}

}
