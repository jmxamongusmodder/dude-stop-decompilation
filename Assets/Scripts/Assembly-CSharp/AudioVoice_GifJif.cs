using System;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class AudioVoice_GifJif : AudioVoiceDefault
{
	// Token: 0x06001181 RID: 4481 RVA: 0x00020D2C File Offset: 0x0001F12C
	public void openCover()
	{
		if (!this.active)
		{
			return;
		}
		float num = (float)SerializablePuzzleStats.Get(base.transform.name).loadedTimes;
		if (num == 0f)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.onOpenFirstLoad);
			this.voice.start(true);
		}
		else if (UnityEngine.Random.value > num * 0.2f)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.onOpen);
			this.voice.start(true);
		}
	}

	// Token: 0x04000E8D RID: 3725
	[Space(10f)]
	public StandaloneLevelVoice onOpenFirstLoad;

	// Token: 0x04000E8E RID: 3726
	public StandaloneLevelVoice onOpen;
}
