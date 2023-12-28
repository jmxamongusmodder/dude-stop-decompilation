using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class AudioVoice_Letter : AudioVoiceDefault
{
	// Token: 0x060011B6 RID: 4534 RVA: 0x00022996 File Offset: 0x00020D96
	public void setPercentage(float prog)
	{
		this.progress = prog;
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0002299F File Offset: 0x00020D9F
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x000229A9 File Offset: 0x00020DA9
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.progress < this.distToGet && Global.self.GetCup(AwardName.POSTMARK))
		{
			base.playSpecificEnd(this.achievEndLine, item);
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000229E4 File Offset: 0x00020DE4
	protected override void setEndText(endTextControl item, string text, bool controlled)
	{
		if (this.progress > 0f && this.voice != null)
		{
			string endText;
			if (this.progress < 0.15f)
			{
				endText = LevelVoice.getEndText(this.voice.info, Global.self.currLanguage, "amazing");
			}
			else
			{
				endText = LevelVoice.getEndText(this.voice.info, Global.self.currLanguage, "sort_of");
			}
			if (!string.IsNullOrEmpty(endText))
			{
				text = endText;
			}
		}
		base.setEndText(item, text, controlled);
	}

	// Token: 0x04000EC8 RID: 3784
	private float progress = 1f;

	// Token: 0x04000EC9 RID: 3785
	[Header("Achievement")]
	public StandaloneLevelVoice achievEndLine;

	// Token: 0x04000ECA RID: 3786
	public float distToGet = 0.1f;
}
