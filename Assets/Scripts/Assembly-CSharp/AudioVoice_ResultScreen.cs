using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class AudioVoice_ResultScreen : AudioVoice
{
	// Token: 0x060012B5 RID: 4789 RVA: 0x00029E4C File Offset: 0x0002824C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Global.self.currentLevelPack == 5 && Global.self.CountPackPlayedTimes(0) == 1)
		{
			this.canExit = false;
			this.voice = Audio.self.playVoice(this.storyMixedEnd);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.canExit = true;
				base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<ScreenEndCard_Results>().buttonContinue.GetComponent<ButtonTemplate>().setActive(true);
			});
			this.voice.start(true);
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x00029ED0 File Offset: 0x000282D0
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active || !this.canExit || !base.enabled)
		{
			base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<ScreenEndCard_Results>().buttonContinue.GetComponent<ButtonTemplate>().setActive(false);
			return false;
		}
		return true;
	}

	// Token: 0x04000FB0 RID: 4016
	[Header("Story pack")]
	public StandaloneLevelVoice storyMixedEnd;

	// Token: 0x04000FB1 RID: 4017
	private bool canExit = true;
}
