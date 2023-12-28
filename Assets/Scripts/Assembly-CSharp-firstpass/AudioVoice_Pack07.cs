using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class AudioVoice_Pack07 : AudioVoice
{
	// Token: 0x06001213 RID: 4627 RVA: 0x000259F4 File Offset: 0x00023DF4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = null;
		CompletionState lastPackCompletionState = Global.self.lastPackCompletionState;
		if (lastPackCompletionState != CompletionState.Monster)
		{
			if (lastPackCompletionState == CompletionState.Good)
			{
				if (Global.self.CountPackPlayedTimes(true, 0) == 0)
				{
					if (Global.self.CountPackPlayedTimes(false, 0) == 1)
					{
						this.voice = Audio.self.playVoice(this.afterGood);
					}
					else if (Global.self.CountPackPlayedTimes(false, 0) == 2)
					{
						this.voice = Audio.self.playVoice(this.afterGood2);
					}
				}
			}
		}
		else if (Global.self.CountPackPlayedTimes(true, 0) == 1)
		{
			this.voice = Audio.self.playVoice(this.afterBad);
		}
		if (this.voice == null)
		{
			if (Global.self.playIWasRightOnPack07)
			{
				this.voice = Audio.self.playVoice(Voices.VoiceMenu.Pack08_Duck_IwasRight);
				this.voice.start(true);
				Global.self.playIWasRightOnPack07 = false;
			}
			return;
		}
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		this.allowExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x00025B64 File Offset: 0x00023F64
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Unlock"))
			{
				if (markerName == "Start")
				{
					this.startPack();
				}
			}
			else
			{
				this.allowExit = true;
				Global.self.unlockNextPack = this.unlockPack;
				GlitchEffectController.self.startGlitch(0.5f);
			}
		}
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x00025BDA File Offset: 0x00023FDA
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active)
		{
			return true;
		}
		if (!this.allowExit)
		{
			return false;
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x00025BFE File Offset: 0x00023FFE
	private void startPack()
	{
		base.StartMusic(ClickWhileVoice.start);
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
		Global.self.skipFirstLineOnPack07 = true;
	}

	// Token: 0x04000F2C RID: 3884
	[Space(10f)]
	public StandaloneLevelVoice afterBad;

	// Token: 0x04000F2D RID: 3885
	public StandaloneLevelVoice afterGood;

	// Token: 0x04000F2E RID: 3886
	public StandaloneLevelVoice afterGood2;

	// Token: 0x04000F2F RID: 3887
	private bool allowExit = true;

	// Token: 0x04000F30 RID: 3888
	private bool unlockPack;
}
