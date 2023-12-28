using System;

// Token: 0x020002A7 RID: 679
public class AudioVoice_CinemaPhone : AudioVoiceDefault
{
	// Token: 0x0600109C RID: 4252 RVA: 0x00019C7F File Offset: 0x0001807F
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x00019CA0 File Offset: 0x000180A0
	public void pullOut()
	{
		this.playOnce(this.pullOutLine);
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00019CAE File Offset: 0x000180AE
	public void onOptionsChange()
	{
		this.optionsCount++;
		if (this.optionsCount == 5)
		{
			this.playOnce(this.optionsChangeLine);
		}
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x00019CD6 File Offset: 0x000180D6
	public void leftUnmuted()
	{
		this.playOnce(this.leftUnmutedLine);
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x00019CE4 File Offset: 0x000180E4
	private void playOnce(StandaloneLevelVoice line)
	{
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(line.levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(line);
			this.voice.start(true);
		}
	}

	// Token: 0x04000D9E RID: 3486
	public StandaloneLevelVoice pullOutLine;

	// Token: 0x04000D9F RID: 3487
	public StandaloneLevelVoice optionsChangeLine;

	// Token: 0x04000DA0 RID: 3488
	public StandaloneLevelVoice leftUnmutedLine;

	// Token: 0x04000DA1 RID: 3489
	private int optionsCount;
}
