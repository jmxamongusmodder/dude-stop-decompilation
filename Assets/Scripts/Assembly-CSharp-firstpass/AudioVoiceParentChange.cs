using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public abstract class AudioVoiceParentChange : AudioVoice
{
	// Token: 0x06001350 RID: 4944 RVA: 0x00016367 File Offset: 0x00014767
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (this.active)
		{
			Global.self.sendVoiceLineToNextPuzzle(this);
			if (!this.alreadyPlaying)
			{
				this.whenPreviousVoiceStops(null);
			}
		}
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00016398 File Offset: 0x00014798
	public void setExistingVoice(VoiceLine line, StandaloneLevelVoice vLine)
	{
		this.active = true;
		if (!base.checkCondition())
		{
			if (line != null)
			{
				Debug.LogError("Sending voice to a script that couldn't be started because of ifCondition. Change ifCondition or stop sending here voices");
				line.stop();
			}
			this.active = false;
			return;
		}
		this.active = false;
		if (vLine.bankName == this.voiceLine.bankName && vLine.levelVoiceId == this.voiceLine.levelVoiceId)
		{
			if (line != null)
			{
				this.voice = line;
			}
			else
			{
				this.voice = Audio.self.playVoice(this.voiceLine);
				this.voice.start(true);
			}
			this.whenNewVoiceStarts();
		}
		else
		{
			line.subscribeToStopped(this, new Action<VoiceLine>(this.whenPreviousVoiceStops));
		}
		this.alreadyPlaying = true;
	}

	// Token: 0x06001352 RID: 4946
	protected abstract void whenNewVoiceStarts();

	// Token: 0x06001353 RID: 4947
	protected abstract void whenPreviousVoiceStops(VoiceLine line);

	// Token: 0x06001354 RID: 4948 RVA: 0x0001646C File Offset: 0x0001486C
	public void setVoiceToSend()
	{
		this.voiceToSend = this.voice;
		this.voiceLineToSend = this.voiceLine;
		this.unsubscribeAll();
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x0001648C File Offset: 0x0001488C
	public void sendFailed()
	{
		this.unsubscribeAll();
		if (this.voiceToSend != null)
		{
			this.voiceToSend.stop();
		}
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000164AA File Offset: 0x000148AA
	private void unsubscribeAll()
	{
		if (this.voice == null)
		{
			return;
		}
		this.voice.unsubscribeFromAll(this);
	}

	// Token: 0x0400103E RID: 4158
	[Space(10f)]
	public UnityEngine.Object SendVoiceTo;

	// Token: 0x0400103F RID: 4159
	[HideInInspector]
	public StandaloneLevelVoice voiceLineToSend;

	// Token: 0x04001040 RID: 4160
	[HideInInspector]
	public VoiceLine voiceToSend;

	// Token: 0x04001041 RID: 4161
	private bool alreadyPlaying;
}
