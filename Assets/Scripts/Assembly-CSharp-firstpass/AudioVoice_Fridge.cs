using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class AudioVoice_Fridge : AudioVoiceScrollable
{
	// Token: 0x06001169 RID: 4457 RVA: 0x00020948 File Offset: 0x0001ED48
	public override void onTransitionIn()
	{
		if (!this.active || this.helloed)
		{
			return;
		}
		this.helloed = base.playVoice(this.onLoad, false, true, false, true);
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x00020978 File Offset: 0x0001ED78
	public void throwPic()
	{
		int num = this.picThrowen;
		if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					base.playVoice(this.binThird, true, false, true, true);
				}
			}
			else
			{
				base.playVoice(this.binSecond, true, false, true, true);
			}
		}
		else
		{
			base.playVoice(this.binFirst, true, false, true, true);
		}
		this.picThrowen++;
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x000209F8 File Offset: 0x0001EDF8
	public void hangPics(bool achiev)
	{
		if (this.hangAll)
		{
			return;
		}
		if (achiev)
		{
			base.playVoice(this.achievement, true, true, false, true);
		}
		else if (!this.hangAll)
		{
			base.playVoice(this.good, true, false, true, true);
		}
		this.hangAll = true;
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00020A50 File Offset: 0x0001EE50
	public void tryToDraw()
	{
		base.playVoice(this.pen, true, true, false, true);
	}

	// Token: 0x04000E72 RID: 3698
	[Space(10f)]
	public StandaloneLevelVoice[] onLoad;

	// Token: 0x04000E73 RID: 3699
	public StandaloneLevelVoice[] binFirst;

	// Token: 0x04000E74 RID: 3700
	public StandaloneLevelVoice[] binSecond;

	// Token: 0x04000E75 RID: 3701
	public StandaloneLevelVoice[] binThird;

	// Token: 0x04000E76 RID: 3702
	public StandaloneLevelVoice[] good;

	// Token: 0x04000E77 RID: 3703
	public StandaloneLevelVoice[] achievement;

	// Token: 0x04000E78 RID: 3704
	public StandaloneLevelVoice[] pen;

	// Token: 0x04000E79 RID: 3705
	private int picThrowen;

	// Token: 0x04000E7A RID: 3706
	private bool hangAll;

	// Token: 0x04000E7B RID: 3707
	private bool helloed;
}
