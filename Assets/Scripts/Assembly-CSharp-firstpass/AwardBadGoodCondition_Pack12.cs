using System;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class AwardBadGoodCondition_Pack12 : AwardBadGoodCondition
{
	// Token: 0x060013CA RID: 5066 RVA: 0x000318EC File Offset: 0x0002FCEC
	public override void setAward()
	{
		if (this.showIfEndScene)
		{
			if (Global.self.endCutscenePack12)
			{
				Global.self.endCutscenePack12 = false;
				this.toGetReward = 0;
				base.setAward();
			}
			else
			{
				this.canGetCup = false;
				base.setAward();
			}
		}
		else if (SerializableGameStats.self.isGameFinished)
		{
			base.setAward();
		}
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x00031957 File Offset: 0x0002FD57
	protected override void setReward(Transform rewardPuzzle)
	{
		if (!this.canGetCup)
		{
			return;
		}
		base.setReward(rewardPuzzle);
	}

	// Token: 0x04001084 RID: 4228
	[Space(10f)]
	public bool showIfEndScene;

	// Token: 0x04001085 RID: 4229
	private bool canGetCup = true;
}
