using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class AudioVoice_Pizza : AudioVoiceDefault
{
	// Token: 0x060012A3 RID: 4771 RVA: 0x0002947C File Offset: 0x0002787C
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Audio.self.muteVoiceInEditor)
		{
			item.SetEnding(LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage), false);
			return;
		}
		if (this.isNoSosage)
		{
			base.playSpecificEnd(this.noSosage, item);
		}
		else if (this.isNoCrust)
		{
			base.playSpecificEnd(this.noCrust, item);
		}
		else if (this.isMaxPiece)
		{
			base.playSpecificEnd(this.maxPiece, item);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0002952D File Offset: 0x0002792D
	public void playNoSosage()
	{
		this.isNoSosage = true;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x00029536 File Offset: 0x00027936
	public void playNoCrust()
	{
		this.isNoCrust = true;
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x0002953F File Offset: 0x0002793F
	public void playMaxPiece()
	{
		this.isMaxPiece = true;
	}

	// Token: 0x04000FA3 RID: 4003
	[Space(10f)]
	public StandaloneLevelVoice noSosage;

	// Token: 0x04000FA4 RID: 4004
	public StandaloneLevelVoice noCrust;

	// Token: 0x04000FA5 RID: 4005
	public StandaloneLevelVoice maxPiece;

	// Token: 0x04000FA6 RID: 4006
	private bool isNoSosage;

	// Token: 0x04000FA7 RID: 4007
	private bool isNoCrust;

	// Token: 0x04000FA8 RID: 4008
	private bool isMaxPiece;
}
