using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class AudioVoice_ShopClothes : AudioVoiceDefault
{
	// Token: 0x060012F3 RID: 4851 RVA: 0x0002D5C5 File Offset: 0x0002B9C5
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "showDuck")
		{
			base.StartCoroutine(AudioVoice_WashClothes.showDuck());
		}
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x0002D5EC File Offset: 0x0002B9EC
	public void dropShirt()
	{
		if (this.dropCount == 0)
		{
			this.playOnce(this.heyYouDropped, false);
		}
		else if (this.dropCount == 1)
		{
			this.playOnce(this.ISwear, false);
		}
		this.dropCount++;
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x0002D63D File Offset: 0x0002BA3D
	public void takeOtherIfExistsOnTheFloor()
	{
		this.playOnce(this.noWaitPickUp, false);
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x0002D64C File Offset: 0x0002BA4C
	private void playOnce(StandaloneLevelVoice line, bool interrupt = false)
	{
		if (!interrupt && this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(line.levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying() && interrupt)
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(line);
			this.voice.start(true);
		}
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x0002D6E4 File Offset: 0x0002BAE4
	public override void subsctibeToEnding(endTextControl item)
	{
		if (base.ps.solvedAsBad == true && !SerializableGameStats.self.pack09DuckShowed)
		{
			this.voice = Audio.self.playVoice(this.duckEnd);
			base.subscribeToMarkers(item, true);
			this.voice.start(true);
			SerializableGameStats.self.pack09DuckShowed = true;
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04000FF5 RID: 4085
	public StandaloneLevelVoice heyYouDropped;

	// Token: 0x04000FF6 RID: 4086
	public StandaloneLevelVoice noWaitPickUp;

	// Token: 0x04000FF7 RID: 4087
	public StandaloneLevelVoice ISwear;

	// Token: 0x04000FF8 RID: 4088
	private int dropCount;

	// Token: 0x04000FF9 RID: 4089
	[Space(10f)]
	public StandaloneLevelVoice duckEnd;
}
