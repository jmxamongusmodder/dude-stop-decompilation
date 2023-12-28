using System;
using UnityEngine;

// Token: 0x020002FD RID: 765
public class AudioVoice_USB : AudioVoiceDefault
{
	// Token: 0x06001326 RID: 4902 RVA: 0x0002E8D4 File Offset: 0x0002CCD4
	public void playLine()
	{
		if (!this.active)
		{
			return;
		}
		if (Global.self.previousPuzzleSolvedAsMonster != true)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.secondInterrupt.levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				return;
			}
			this.voice = Audio.self.playVoice(this.secondInterrupt);
			this.voice.start(true);
		}
	}

	// Token: 0x0400101D RID: 4125
	[Space(10f)]
	public StandaloneLevelVoice secondInterrupt;
}
