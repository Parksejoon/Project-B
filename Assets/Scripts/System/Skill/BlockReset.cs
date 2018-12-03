using System.Collections;
using UnityEngine;

public class BlockReset : Skill
{
	[SerializeField]
	private ObjectGiver normalBlockGiver;           // 일반 블럭 공급자


	// 스킬 사용
	public override void ShotSkill()
	{
		if (PlayerJumpManager.isGround && !isCoolDowning)
		{
			StartCoroutine(CoolDown());

			normalBlockGiver.GiveAllBlock();
		}
	}
}
