using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class AudioVoice_DoNotTouch : AudioVoiceParentChange
{
	// Token: 0x06001156 RID: 4438 RVA: 0x0001FC32 File Offset: 0x0001E032
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x0001FC47 File Offset: 0x0001E047
	protected override void whenNewVoiceStarts()
	{
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.stopReached));
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0001FC7C File Offset: 0x0001E07C
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.stopReached));
		this.voice.start(true);
		this.voice.setParameter("GotoSingle", 1f);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x0001FCF4 File Offset: 0x0001E0F4
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Loop"))
			{
				if (!(markerName == "Touched"))
				{
					if (!(markerName == "DisableExit"))
					{
						if (markerName == "EnableExit")
						{
							Global.self.canBePaused = true;
							Global.self.canExitEndScreen = true;
							base.endVoicedEnding(this.voice);
						}
					}
					else
					{
						Global.self.canExitEndScreen = false;
						Global.self.canBePaused = false;
					}
				}
				else
				{
					this.voice.setParameter("Touched", 0f);
				}
			}
			else
			{
				this.loopCount++;
				if (this.loopCount == 5)
				{
					this.voice.setParameter("GotoSingle", 1f);
				}
				else if (this.loopCount == 9)
				{
					this.voice.setParameter("GotoSingle", 2f);
				}
				else if (this.loopCount == 13)
				{
					this.voice.setParameter("GotoSingle", 3f);
				}
				else
				{
					this.voice.setParameter("GotoSingle", 0f);
				}
			}
		}
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0001FE4C File Offset: 0x0001E24C
	private void stopReached(VoiceLine line)
	{
		this.voiceLine = this.nextVoiceLine;
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0001FE7C File Offset: 0x0001E27C
	public void touched()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.isTouched)
		{
			this.voice.setParameter("Touched", 1f);
		}
		this.isTouched = true;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0001FEB1 File Offset: 0x0001E2B1
	public void finishLevel()
	{
		if (!this.active)
		{
			return;
		}
		this.voice.setParameter("GotoEnd", 1f);
		Global.self.canExitEndScreen = false;
		base.StartCoroutine(this.DelayMusic());
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0001FEEC File Offset: 0x0001E2EC
	private IEnumerator DelayMusic()
	{
		yield return new WaitForSeconds(3f);
		Audio.self.ChangeMusicParameter("757e3a0a-c20a-4728-ab16-74dc9cf91a6b", "Voice Temper", 0.25f);
		yield break;
	}

	// Token: 0x04000E58 RID: 3672
	private int loopCount;

	// Token: 0x04000E59 RID: 3673
	public StandaloneLevelVoice nextVoiceLine;

	// Token: 0x04000E5A RID: 3674
	private bool isTouched;
}
