using System.Collections;
using UnityEngine;

public class Skill_BlockReset : Skill
{
	[SerializeField]
	private CustomBlockBuilder customBlockBuilder;


	// 스킬 사용
	public override void ShotSkill()
	{
		customBlockBuilder.ResetBlocks();
	}
}
