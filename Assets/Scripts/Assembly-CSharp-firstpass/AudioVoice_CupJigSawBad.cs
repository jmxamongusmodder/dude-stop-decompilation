using System;

// Token: 0x020002B5 RID: 693
public class AudioVoice_CupJigSawBad : AudioVoiceCups
{
	// Token: 0x06001106 RID: 4358 RVA: 0x0001D080 File Offset: 0x0001B480
	public void onClick()
	{
		if (!this.active && this.ind > 2)
		{
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.onClicks[this.ind]);
		this.voice.start(true);
		this.ind++;
	}

	// Token: 0x04000E09 RID: 3593
	private int ind;

	// Token: 0x04000E0A RID: 3594
	public StandaloneLevelVoice[] onClicks;
}
