using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class Award_Pack13 : Award
{
	// Token: 0x060013B8 RID: 5048 RVA: 0x0003140D File Offset: 0x0002F80D
	public override void setCupState()
	{
		if (!this.forceGet)
		{
			this.getCup();
		}
		base.setCupState();
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x00031428 File Offset: 0x0002F828
	protected override void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.K))
		{
			this.forceGet = true;
			Global.self.cupList[this.awardName] = CupStatus.ShowAnimation;
			this.setCupState();
		}
		base.Update();
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x0003147C File Offset: 0x0002F87C
	private void getCup()
	{
		if (Global.self.cupList[this.awardName] == CupStatus.Exist)
		{
			return;
		}
		AwardName[] array = (AwardName[])Enum.GetValues(typeof(AwardName));
		AwardName[] source = Global.self.awardsToSkip.Concat(new AwardName[]
		{
			this.awardName
		}).ToArray<AwardName>();
		foreach (AwardName awardName in array)
		{
			if (!source.Contains(awardName))
			{
				if (Global.self.cupList[awardName] == CupStatus.Empty)
				{
					return;
				}
			}
		}
		this.GetPuzzleStats().GetComponent<AudioVoice_Pack13>().getAllCups();
		Global.self.GetCup(this.awardName);
	}

	// Token: 0x04001077 RID: 4215
	private bool forceGet;
}
