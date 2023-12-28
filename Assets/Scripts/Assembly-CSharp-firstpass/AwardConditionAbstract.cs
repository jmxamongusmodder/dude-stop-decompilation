using System;
using UnityEngine;

// Token: 0x0200031C RID: 796
[RequireComponent(typeof(AwardController))]
public abstract class AwardConditionAbstract : MonoBehaviour
{
	// Token: 0x060013CD RID: 5069 RVA: 0x0003158D File Offset: 0x0002F98D
	public virtual void startPack()
	{
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x0003158F File Offset: 0x0002F98F
	public virtual void setAward()
	{
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x00031594 File Offset: 0x0002F994
	protected void setGoodBadReward(bool good, int toGet, Transform rewardPuzzle)
	{
		AwardController.self.setBestProgress(good);
		if (AwardController.self.getProgress(good) < toGet)
		{
			return;
		}
		if (rewardPuzzle != null)
		{
			Global.self.CountPackPlayedTimes(!good, 1);
			if (good)
			{
				Global.self.lastPackCompletionState = CompletionState.Good;
			}
			else
			{
				Global.self.lastPackCompletionState = CompletionState.Monster;
			}
		}
		this.setReward(rewardPuzzle);
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x00031602 File Offset: 0x0002FA02
	protected virtual void setReward(Transform rewardPuzzle)
	{
		if (Global.self.cupList[this.award] != CupStatus.Exist)
		{
			this.isSaved = Global.self.GetCup(this.award);
			this.nextPuzzle = rewardPuzzle;
		}
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x0003163C File Offset: 0x0002FA3C
	public bool isPackSaved()
	{
		return this.isSaved;
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x00031644 File Offset: 0x0002FA44
	public Transform getNextPuzzle()
	{
		return this.nextPuzzle;
	}

	// Token: 0x04001086 RID: 4230
	[Space(10f)]
	[Tooltip("Reward to give player with this condition")]
	public AwardName award;

	// Token: 0x04001087 RID: 4231
	protected bool isSaved;

	// Token: 0x04001088 RID: 4232
	protected Transform nextPuzzle;
}
