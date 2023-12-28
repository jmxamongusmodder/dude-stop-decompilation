using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class AudioVoice_VerticalVideo : AudioVoiceDefault
{
	// Token: 0x06001328 RID: 4904 RVA: 0x0002E980 File Offset: 0x0002CD80
	public void takePhoto()
	{
		if (this.voice.isPlaying() || !this.active || this.photoCount > 1)
		{
			return;
		}
		if (this.photoCount == 0)
		{
			this.voice = Audio.self.playVoice(this.onFirstPhoto);
			this.voice.start(true);
		}
		else
		{
			this.voice = Audio.self.playVoice(this.onSecondPhoto);
			this.voice.start(true);
		}
		this.photoCount++;
	}

	// Token: 0x0400101E RID: 4126
	[Space(10f)]
	public StandaloneLevelVoice onFirstPhoto;

	// Token: 0x0400101F RID: 4127
	public StandaloneLevelVoice onSecondPhoto;

	// Token: 0x04001020 RID: 4128
	private int photoCount;
}
