using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class AudioVoice_Pack3 : AudioVoice
{
	// Token: 0x06001285 RID: 4741 RVA: 0x0002889C File Offset: 0x00026C9C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(true, 0) == 1)
		{
			if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.onBadLine.levelVoiceId))
			{
				this.voice = Audio.self.playVoice(this.onBadLine);
				this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
				this.unlockPack = Global.self.unlockNextPack;
				Global.self.unlockNextPack = false;
			}
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(true, 0) == 0 && Global.self.CountPackPlayedTimes(false, 0) == 1)
		{
			if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.onGoodLine.levelVoiceId))
			{
				this.voice = Audio.self.playVoice(this.onGoodLine);
			}
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Mixed && Global.self.CountPackPlayedTimes(true, 0) == 0 && Global.self.CountPackPlayedTimes(false, 0) + 1 == Global.self.CountPackPlayedTimes(0) && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.onMixLine.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.onMixLine);
		}
		if (this.voice == null)
		{
			return;
		}
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
			base.SetPackStartButton(true);
		});
		this.canExit = false;
		this.voice.start(true);
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x00028A78 File Offset: 0x00026E78
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "StartPack"))
			{
				if (markerName == "Unlock")
				{
					Global.self.unlockNextPack = this.unlockPack;
				}
			}
			else
			{
				this.startPack();
			}
		}
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x00028AD8 File Offset: 0x00026ED8
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active)
		{
			return true;
		}
		if (this.voiceStarted || !this.canExit)
		{
			base.SetPackStartButton(false);
			return false;
		}
		if (Global.self.isThirdPackAfterGamesIntro && click == ClickWhileVoice.start)
		{
			Global.self.isThirdPackAfterGamesIntro = false;
			this.voiceStarted = true;
			this.voice = Audio.self.playVoice(this.voiceLine);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.start(true);
			base.SetPackStartButton(false);
			return false;
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x00028B85 File Offset: 0x00026F85
	private void startPack()
	{
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
	}

	// Token: 0x04000F8C RID: 3980
	[Space(10f)]
	public StandaloneLevelVoice onBadLine;

	// Token: 0x04000F8D RID: 3981
	public StandaloneLevelVoice onGoodLine;

	// Token: 0x04000F8E RID: 3982
	public StandaloneLevelVoice onMixLine;

	// Token: 0x04000F8F RID: 3983
	private bool voiceStarted;

	// Token: 0x04000F90 RID: 3984
	private bool canExit = true;

	// Token: 0x04000F91 RID: 3985
	private bool unlockPack;
}
