using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class AwardTimed : AwardConditionAbstract
{
	// Token: 0x06001401 RID: 5121 RVA: 0x0003282C File Offset: 0x00030C2C
	private void Update()
	{
		if (!this.inTime || !this.packIsActive)
		{
			return;
		}
		this.secondsToBit -= Time.deltaTime;
		if (this.secondsToBit <= 0f)
		{
			this.inTime = false;
		}
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x00032879 File Offset: 0x00030C79
	public override void startPack()
	{
		base.startPack();
		this.packIsActive = true;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x00032888 File Offset: 0x00030C88
	public override void setAward()
	{
		base.setAward();
		this.packIsActive = false;
		if (this.inTime)
		{
			this.giveCup();
			this.inTime = false;
		}
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000328AF File Offset: 0x00030CAF
	public void giveCup()
	{
		this.isSaved = Global.self.GetCup(this.award);
	}

	// Token: 0x04001101 RID: 4353
	public float secondsToBit;

	// Token: 0x04001102 RID: 4354
	private bool packIsActive;

	// Token: 0x04001103 RID: 4355
	[HideInInspector]
	public bool inTime = true;
}
