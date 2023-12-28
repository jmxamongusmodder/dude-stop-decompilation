using System;

// Token: 0x0200029F RID: 671
public class AudioVoice_Bookmark : AudioVoiceParentChange
{
	// Token: 0x06001062 RID: 4194 RVA: 0x000164CC File Offset: 0x000148CC
	public override void setActive(bool on)
	{
		base.setActive(on);
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x000164D8 File Offset: 0x000148D8
	protected override void whenNewVoiceStarts()
	{
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.setParameter("Start", 1f);
		this.newVoicePlaying = true;
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x00016534 File Offset: 0x00014934
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.setParameter("Start", 1f);
		this.newVoicePlaying = true;
		if (this.levelFinished)
		{
			this.voice.setParameter("LevelEnd", 1f);
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000165D0 File Offset: 0x000149D0
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "EnableExit"))
			{
				if (markerName == "StopVoice")
				{
					this.voice.stop();
				}
			}
			else
			{
				Global.self.canExitEndScreen = true;
				Global.self.canBePaused = true;
				base.endVoicedEnding(this.voice);
			}
		}
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x00016648 File Offset: 0x00014A48
	private void voiceStopped(VoiceLine line)
	{
		this.voiceLine = this.nextVoiceLine;
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0001669C File Offset: 0x00014A9C
	public void finishLevel()
	{
		if (!this.active || this.levelFinished)
		{
			return;
		}
		if (this.newVoicePlaying)
		{
			this.voice.setParameter("LevelEnd", 1f);
		}
		this.levelFinished = true;
		Global.self.canExitEndScreen = false;
		Global.self.canBePaused = false;
		Audio.self.ChangeMusicParameter("757e3a0a-c20a-4728-ab16-74dc9cf91a6b", "Voice Temper", 0.5f);
	}

	// Token: 0x04000D6F RID: 3439
	public StandaloneLevelVoice nextVoiceLine;

	// Token: 0x04000D70 RID: 3440
	private bool levelFinished;

	// Token: 0x04000D71 RID: 3441
	private bool newVoicePlaying;
}
