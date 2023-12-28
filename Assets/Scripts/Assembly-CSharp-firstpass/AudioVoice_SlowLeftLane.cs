using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class AudioVoice_SlowLeftLane : AudioVoiceDefault
{
	// Token: 0x060012F9 RID: 4857 RVA: 0x0002D76A File Offset: 0x0002BB6A
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0002D78C File Offset: 0x0002BB8C
	public void carBehind()
	{
		if (this.said || !this.active)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.carBehindVoice);
		this.voice.start(true);
		this.said = true;
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x0002D800 File Offset: 0x0002BC00
	public override void subsctibeToEnding(endTextControl item)
	{
		if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes == 0)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			if (base.ps.solvedAsBad == true)
			{
				this.voice = Audio.self.playVoice(this.duckEndBad);
				this.voice.start(true);
				item.SetEnding(LevelVoice.getEndText(this.duckEndBad, Global.self.currLanguage), false);
				base.lockExitUntillVoiceStops(item);
			}
			else
			{
				this.voice = Audio.self.playVoice(this.duckEndGood);
				base.subscribeToMarkers(item, true);
				this.voice.start(true);
			}
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04000FFA RID: 4090
	public StandaloneLevelVoice carBehindVoice;

	// Token: 0x04000FFB RID: 4091
	private bool said;

	// Token: 0x04000FFC RID: 4092
	[Space(10f)]
	public StandaloneLevelVoice duckEndBad;

	// Token: 0x04000FFD RID: 4093
	public StandaloneLevelVoice duckEndGood;
}
