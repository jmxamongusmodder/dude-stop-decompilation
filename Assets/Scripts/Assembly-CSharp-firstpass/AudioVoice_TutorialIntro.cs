using System;

// Token: 0x020002FC RID: 764
public class AudioVoice_TutorialIntro : AudioVoice
{
	// Token: 0x06001321 RID: 4897 RVA: 0x0002E564 File Offset: 0x0002C964
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (Global.self.isGameIntroJustFinished)
		{
			this.active = false;
		}
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0002E5D8 File Offset: 0x0002C9D8
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "HideStart":
			this.setStartButton(false);
			break;
		case "ShowStart":
			this.setStartButton(true);
			this.interruptCount = 0;
			break;
		case "DisableStart":
			this.canClickStart = false;
			break;
		case "EI":
			this.interruptAllowed = true;
			break;
		case "DI":
			this.interruptAllowed = false;
			break;
		case "Save":
			this.sentenceStart = this.voice.getPosition();
			break;
		case "Set0":
			this.voice.setParameter("Interrupt", 0f);
			break;
		case "Set":
			this.voice.setParameter("Interrupt", (float)this.interruptCount);
			break;
		case "EndInterrupt":
			this.voice.gotoPosition(this.sentenceStart);
			break;
		case "EnableStart":
			this.canClickStart = true;
			break;
		case "EndLevel":
			this.canClickStart = true;
			base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().bStart();
			Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
			break;
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x0002E7B8 File Offset: 0x0002CBB8
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active)
		{
			return true;
		}
		if (this.canClickStart && click == ClickWhileVoice.back)
		{
			UIControl.self.hideSubtitles();
		}
		if (this.voice == null || !this.voice.isPlaying())
		{
			if (click == ClickWhileVoice.start)
			{
				Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
			}
			return true;
		}
		if (!this.canClickStart && click != ClickWhileVoice.start)
		{
			return false;
		}
		if (this.interruptAllowed)
		{
			this.interruptCount++;
			this.voice.setParameter("Interrupt", (float)this.interruptCount);
			this.interruptAllowed = false;
		}
		if (this.canClickStart)
		{
			if (click == ClickWhileVoice.start)
			{
				this.voice.setParameter("Interrupt2", 1f);
				Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
			}
			else
			{
				this.voice.stop();
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x0002E8AF File Offset: 0x0002CCAF
	private void setStartButton(bool on)
	{
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().breakStartButton(!on);
	}

	// Token: 0x04001018 RID: 4120
	private int interruptCount;

	// Token: 0x04001019 RID: 4121
	private bool interruptAllowed;

	// Token: 0x0400101A RID: 4122
	private int sentenceStart = -1;

	// Token: 0x0400101B RID: 4123
	private bool canClickStart;
}
