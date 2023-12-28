using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class AudioVoice_Pack12 : AudioVoice
{
	// Token: 0x06001265 RID: 4709 RVA: 0x00027F6C File Offset: 0x0002636C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes == 0)
		{
			this.voice = Audio.self.playVoice(this.startLine);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.canStart = true;
				this.canInterrupt = false;
				base.SetPackStartButton(true);
			});
			this.voice.start(true);
			this.canStart = false;
			base.SetPackStartButton(false);
			return;
		}
		if (!SerializableGameStats.self.isGameFinished && (Global.self.lastPackCompletionState == CompletionState.Mixed || Global.self.lastPackCompletionState == CompletionState.Good) && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.goodEndLine.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.goodEndLine);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.canStart = true;
				this.hideStartOnClick = false;
				base.SetPackStartButton(true);
			});
			this.voice.start(true);
			this.canStart = false;
			this.hideStartOnClick = true;
			base.SetPackStartButton(false);
		}
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000280AB File Offset: 0x000264AB
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "EI")
		{
			this.canInterrupt = true;
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000280CC File Offset: 0x000264CC
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (this.canInterrupt && click == ClickWhileVoice.start)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.canInterrupt = false;
			this.interruptPlaying = true;
			base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().gameObject.SetActive(false);
			this.voice = Audio.self.playVoice(this.interruptLine);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
			});
			this.voice.start(true);
		}
		if (this.interruptPlaying)
		{
			return false;
		}
		if (this.hideStartOnClick && click == ClickWhileVoice.start)
		{
			base.SetPackStartButton(false);
			this.hideStartOnClick = false;
		}
		if (this.canStart)
		{
			base.StartMusic(click);
			return true;
		}
		return false;
	}

	// Token: 0x04000F70 RID: 3952
	[Space(10f)]
	public StandaloneLevelVoice startLine;

	// Token: 0x04000F71 RID: 3953
	public StandaloneLevelVoice interruptLine;

	// Token: 0x04000F72 RID: 3954
	public StandaloneLevelVoice goodEndLine;

	// Token: 0x04000F73 RID: 3955
	private bool canStart = true;

	// Token: 0x04000F74 RID: 3956
	private bool canInterrupt;

	// Token: 0x04000F75 RID: 3957
	private bool interruptPlaying;

	// Token: 0x04000F76 RID: 3958
	private bool hideStartOnClick;
}
