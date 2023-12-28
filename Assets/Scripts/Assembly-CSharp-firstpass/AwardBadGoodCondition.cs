using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class AwardBadGoodCondition : AwardConditionAbstract
{
	// Token: 0x060013C8 RID: 5064 RVA: 0x000318BC File Offset: 0x0002FCBC
	public override void setAward()
	{
		base.setAward();
		base.setGoodBadReward(this.goodReward, this.toGetReward, this.rewardPuzzle);
	}

	// Token: 0x04001081 RID: 4225
	[Space(10f)]
	public bool goodReward;

	// Token: 0x04001082 RID: 4226
	[Tooltip("What % of level needs to be complete to get reward")]
	[Range(0f, 100f)]
	public int toGetReward = 80;

	// Token: 0x04001083 RID: 4227
	[Tooltip("Puzzle to show on getting this reward")]
	public Transform rewardPuzzle;
}
