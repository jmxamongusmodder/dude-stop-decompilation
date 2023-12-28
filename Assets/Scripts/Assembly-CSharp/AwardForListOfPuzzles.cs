using System;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class AwardForListOfPuzzles : AwardConditionAbstract
{
	// Token: 0x060013F4 RID: 5108 RVA: 0x000321C0 File Offset: 0x000305C0
	public override void setAward()
	{
		base.setAward();
		if (this.completedAsBad == this.completedAsGood)
		{
			return;
		}
		if (this.completedAsBad)
		{
			this.isSaved = Global.self.GetCup(this.award);
		}
		if (this.completedAsGood)
		{
			this.isSaved = Global.self.GetCup(this.awardGood);
		}
	}

	// Token: 0x060013F5 RID: 5109 RVA: 0x00032228 File Offset: 0x00030628
	public void setPuzzle(Transform puzzle, bool solvedAsBad)
	{
		if (puzzle == null)
		{
			return;
		}
		bool flag = false;
		foreach (Transform transform in this.listToSolve)
		{
			if (puzzle.name == transform.name)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			if (solvedAsBad)
			{
				this.completedAsGood = false;
			}
			if (!solvedAsBad)
			{
				this.completedAsBad = false;
			}
		}
	}

	// Token: 0x040010A8 RID: 4264
	[Tooltip("Award for GOOD completion. normal Award is for the BAD completion")]
	public AwardName awardGood;

	// Token: 0x040010A9 RID: 4265
	[Tooltip("List of puzzle to check for how they are completed")]
	public Transform[] listToSolve;

	// Token: 0x040010AA RID: 4266
	private bool completedAsGood = true;

	// Token: 0x040010AB RID: 4267
	private bool completedAsBad = true;
}
