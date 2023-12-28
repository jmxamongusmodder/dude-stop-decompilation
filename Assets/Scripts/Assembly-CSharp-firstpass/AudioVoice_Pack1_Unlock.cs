using System;

// Token: 0x020002E3 RID: 739
public class AudioVoice_Pack1_Unlock : AudioVoice
{
	// Token: 0x06001247 RID: 4679 RVA: 0x00027018 File Offset: 0x00025418
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!Global.self.isGameIntroJustFinished)
		{
			this.active = false;
		}
		if (!this.active)
		{
			return;
		}
		Global.self.isGameIntroJustFinished = false;
		Global.self.isGameIntroActive = false;
		Global.self.isThirdPackAfterGamesIntro = true;
		this.voice = Audio.self.playVoice(this.voiceLine);
		base.SetPackStartButton(false);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		Global.self.unlockNextPack = false;
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000270D4 File Offset: 0x000254D4
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Unlock"))
			{
				if (markerName == "EI")
				{
					this.canClick = true;
				}
			}
			else
			{
				Global.self.unlockNextPack = true;
			}
		}
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00027130 File Offset: 0x00025530
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active)
		{
			return true;
		}
		if (!this.canClick)
		{
			return false;
		}
		if (this.voice != null)
		{
			this.voice.stop();
			UIControl.self.hideSubtitles();
		}
		if (click == ClickWhileVoice.pack)
		{
			this.voice = Audio.self.playVoice(this.onPack);
			this.voice.start(true);
		}
		if (click == ClickWhileVoice.start && !this.warnedOnce)
		{
			this.voice = Audio.self.playVoice(this.onStart);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			base.SetPackStartButton(false);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				base.SetPackStartButton(true);
			});
			this.voice.start(true);
			this.warnedOnce = true;
			return false;
		}
		if (click == ClickWhileVoice.back)
		{
			this.voice = Audio.self.playVoice(this.onExit);
			this.voice.start(true);
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00027246 File Offset: 0x00025646
	private void startPack()
	{
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
	}

	// Token: 0x04000F54 RID: 3924
	private bool canClick;

	// Token: 0x04000F55 RID: 3925
	public StandaloneLevelVoice onPack;

	// Token: 0x04000F56 RID: 3926
	public StandaloneLevelVoice onStart;

	// Token: 0x04000F57 RID: 3927
	public StandaloneLevelVoice onExit;

	// Token: 0x04000F58 RID: 3928
	private bool warnedOnce;
}
