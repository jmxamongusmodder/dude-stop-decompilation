using System;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class AwardMultiplePlayCondition : AwardConditionAbstract
{
	// Token: 0x060013FD RID: 5117 RVA: 0x000327D2 File Offset: 0x00030BD2
	public override void setAward()
	{
		base.setAward();
		if (Global.self.CountPackPlayedTimes(0) == this.timesPackPlayed)
		{
			this.isSaved = Global.self.GetCup(this.award);
		}
	}

	// Token: 0x040010B1 RID: 4273
	[Space(10f)]
	[Tooltip("How many time this pack should be completed to get reward")]
	public int timesPackPlayed = 2;
}
