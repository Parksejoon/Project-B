using UnityEngine;

public class BlockReset : Skill
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ObjectGiver normalBlockGiver;   // 일반 블럭 공급자


	// 스킬 사용
	public override void ShotSkill()
	{
		if (PlayerManager.instance.isGround)
		{
			Debug.Log("a");
			normalBlockGiver.GiveAllBlock();
		}
		else
		{
			Debug.Log("b");
		}
	}
}
