using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class AudioVoiceEndAchievement : AudioVoiceDefault
{
	// Token: 0x06001349 RID: 4937 RVA: 0x00023104 File Offset: 0x00021504
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Audio.self.muteVoiceInEditor)
		{
			item.SetEnding(LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage), false);
			return;
		}
		if (this.trophy)
		{
			base.playSpecificEnd(this.achievEnd, item);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x0002317B File Offset: 0x0002157B
	public void getTrophy()
	{
		if (!this.active)
		{
			return;
		}
		this.trophy = true;
	}

	// Token: 0x0400103A RID: 4154
	[Space(10f)]
	public StandaloneLevelVoice achievEnd;

	// Token: 0x0400103B RID: 4155
	private bool trophy;
}
