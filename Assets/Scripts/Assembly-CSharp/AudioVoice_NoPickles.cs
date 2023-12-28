using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class AudioVoice_NoPickles : AudioVoiceSpecialEnding
{
	// Token: 0x060011DC RID: 4572 RVA: 0x00023B5B File Offset: 0x00021F5B
	public void completelyWrong()
	{
		if (this.wrongPlayed)
		{
			return;
		}
		if (base.playVoice(this.wrongLine, false, false))
		{
			this.wrongPlayed = true;
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00023B83 File Offset: 0x00021F83
	public void onDistance()
	{
		base.playVoice(this.travelLine, false, true);
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x00023B94 File Offset: 0x00021F94
	public void noMeat()
	{
		base.playVoice(this.beganLine, false, true);
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x00023BA5 File Offset: 0x00021FA5
	public void OnlyPickles()
	{
		base.playVoice(this.onlyPickles, true, true);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x00023BB8 File Offset: 0x00021FB8
	public override void subsctibeToEnding(endTextControl item)
	{
		if (base.ps.solvedAsBad == true && base.playEndOnBadProgress(item))
		{
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04000EE9 RID: 3817
	[Space(10f)]
	public StandaloneLevelVoice wrongLine;

	// Token: 0x04000EEA RID: 3818
	public StandaloneLevelVoice travelLine;

	// Token: 0x04000EEB RID: 3819
	public StandaloneLevelVoice beganLine;

	// Token: 0x04000EEC RID: 3820
	public StandaloneLevelVoice onlyPickles;

	// Token: 0x04000EED RID: 3821
	private bool wrongPlayed;
}
