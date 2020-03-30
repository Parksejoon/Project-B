using System.Collections;
using UnityEngine;

public class BlockReset : Skill
{
	private CustomBlockBuilder customBlockBuilder;


	// 초기화
	public void Awake()
	{
		customBlockBuilder = GetComponent<CustomBlockBuilder>();
	}

	// 스킬 사용
	public override void ShotSkill()
	{
		customBlockBuilder.ResetBlocks();
	}
}
