using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class AudioVoice_Pack05 : AudioVoice
{
	// Token: 0x06001200 RID: 4608 RVA: 0x000250A4 File Offset: 0x000234A4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = null;
		if (Global.self.lastPackCompletionState == CompletionState.None)
		{
			this.PlayCheeseLine();
			return;
		}
		CompletionState lastPackCompletionState = Global.self.lastPackCompletionState;
		if (lastPackCompletionState != CompletionState.Monster)
		{
			if (lastPackCompletionState != CompletionState.Good)
			{
				if (lastPackCompletionState == CompletionState.Mixed)
				{
					if (Global.self.CountPackPlayedTimes(0) == 1)
					{
						this.voice = Audio.self.playVoice(this.afterMix);
					}
				}
			}
			else if (Global.self.CountPackPlayedTimes(false, 0) == 1 && Global.self.CountPackPlayedTimes(true, 0) == 0)
			{
				this.voice = Audio.self.playVoice(this.afterGood);
			}
		}
		else if (Global.self.CountPackPlayedTimes(true, 0) == 1 && Global.self.CountPackPlayedTimes(false, 0) == 0)
		{
			this.voice = Audio.self.playVoice(this.afterBad);
		}
		if (this.PlayCheeseLine())
		{
			return;
		}
		if (this.voice == null)
		{
			return;
		}
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		this.canExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x00025210 File Offset: 0x00023610
	private bool PlayCheeseLine()
	{
		if (this.voice != null || !this.cheeseCup)
		{
			return false;
		}
		if (!SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.cheeseLine.levelVoiceId))
		{
			return false;
		}
		this.voice = Audio.self.playVoice(this.cheeseLine);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		this.canExit = false;
		return true;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x0002529E File Offset: 0x0002369E
	public void getCheeseCup()
	{
		this.cheeseCup = true;
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000252A8 File Offset: 0x000236A8
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Unlock"))
			{
				if (markerName == "EI")
				{
					this.canExit = true;
					base.SetPackStartButton(true);
				}
			}
			else
			{
				Global.self.unlockNextPack = this.unlockPack;
			}
		}
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x00025310 File Offset: 0x00023710
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active || !this.canExit)
		{
			base.SetPackStartButton(false);
			return false;
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x04000F17 RID: 3863
	[Space(10f)]
	public StandaloneLevelVoice afterBad;

	// Token: 0x04000F18 RID: 3864
	public StandaloneLevelVoice afterGood;

	// Token: 0x04000F19 RID: 3865
	public StandaloneLevelVoice afterMix;

	// Token: 0x04000F1A RID: 3866
	public StandaloneLevelVoice cheeseLine;

	// Token: 0x04000F1B RID: 3867
	private bool canExit = true;

	// Token: 0x04000F1C RID: 3868
	private bool unlockPack;

	// Token: 0x04000F1D RID: 3869
	private bool cheeseCup;
}
