using System;

// Token: 0x020002B4 RID: 692
public class AudioVoice_CupInventoryGood : AudioVoiceCups
{
	// Token: 0x06001104 RID: 4356 RVA: 0x0001D014 File Offset: 0x0001B414
	public void interruptVoice()
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
			this.voice = Audio.self.playVoice(this.onInterrupt);
			this.voice.start(true);
		}
	}

	// Token: 0x04000E08 RID: 3592
	public StandaloneLevelVoice onInterrupt;
}
