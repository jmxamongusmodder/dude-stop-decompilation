using System;
using UnityEngine;

// Token: 0x0200029D RID: 669
public class AudioVoice_6AmNoise : AudioVoiceReceive
{
	// Token: 0x0600105B RID: 4187 RVA: 0x0001607C File Offset: 0x0001447C
	public void onSwitchPressed()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.switchInd++;
			return;
		}
		if (this.switchInd >= this.switchLines.Length)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.switchLines[this.switchInd].levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.switchLines[this.switchInd]);
			this.voice.start(true);
		}
		this.switchInd++;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0001612C File Offset: 0x0001452C
	public void onDrillPickUp()
	{
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.drillLine.levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.drillLine);
			this.voice.start(true);
		}
	}

	// Token: 0x04000D67 RID: 3431
	[Space(10f)]
	public StandaloneLevelVoice[] switchLines;

	// Token: 0x04000D68 RID: 3432
	public StandaloneLevelVoice drillLine;

	// Token: 0x04000D69 RID: 3433
	private int switchInd;
}
