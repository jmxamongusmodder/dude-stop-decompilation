using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class AudioVoice_SwipePhoto : AudioVoiceDefault
{
	// Token: 0x0600130D RID: 4877 RVA: 0x0002E0A8 File Offset: 0x0002C4A8
	public void ReturnBeforeLastGood()
	{
		this.PlayVoice(this.returnBeforeLast);
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x0002E0B7 File Offset: 0x0002C4B7
	public void SwipeLastGood()
	{
		this.PlayVoice(this.swipeLastGood);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0002E0C6 File Offset: 0x0002C4C6
	public void SwipeBackFromLastGood()
	{
		this.PlayVoice(this.swipeBackFromLast);
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0002E0D5 File Offset: 0x0002C4D5
	public void SwipeToAlbum()
	{
		if (this.albumWarningPlayed)
		{
			return;
		}
		if (this.PlayVoice(this.swipeToAlbum))
		{
			this.albumWarningPlayed = true;
		}
		else
		{
			this.SwipeToAlbumAfterAllGood();
		}
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x0002E106 File Offset: 0x0002C506
	public void SwipeToAlbumAgain()
	{
		if (!this.secondWarningPlayed && this.PlayVoice(this.swipeToAlbum2))
		{
			this.secondWarningPlayed = true;
		}
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0002E12B File Offset: 0x0002C52B
	private bool PlayVoice(StandaloneLevelVoice line)
	{
		return SerializablePuzzleStats.Get(base.transform.name).playedTimes <= 0 && base.playVoice(line, true, true);
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0002E153 File Offset: 0x0002C553
	public void SwipeToAlbumAfterAllGood()
	{
		if (!this.albumWarningPlayed)
		{
			this.playUniqueLine(this.toAlbumAfterGood);
			this.albumWarningPlayed = true;
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x0002E173 File Offset: 0x0002C573
	public void OpenSecondAlbum()
	{
		if (!this.secondWarningPlayed)
		{
			this.playUniqueLine(this.openSecondAlbum);
			this.secondWarningPlayed = true;
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0002E194 File Offset: 0x0002C594
	private void playUniqueLine(StandaloneLevelVoice line)
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(AudioVoice.getNotRepeatingVoice(base.transform.name, line, LevelVoice.Type.NotSet, null));
		this.voice.start(true);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x0002E20A File Offset: 0x0002C60A
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "showDuck")
		{
			base.StartCoroutine(AudioVoice_WashClothes.showDuck());
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x0002E230 File Offset: 0x0002C630
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

	// Token: 0x0400100A RID: 4106
	[Space(10f)]
	public StandaloneLevelVoice returnBeforeLast;

	// Token: 0x0400100B RID: 4107
	public StandaloneLevelVoice swipeLastGood;

	// Token: 0x0400100C RID: 4108
	public StandaloneLevelVoice swipeBackFromLast;

	// Token: 0x0400100D RID: 4109
	public StandaloneLevelVoice swipeToAlbum;

	// Token: 0x0400100E RID: 4110
	public StandaloneLevelVoice swipeToAlbum2;

	// Token: 0x0400100F RID: 4111
	public StandaloneLevelVoice toAlbumAfterGood;

	// Token: 0x04001010 RID: 4112
	public StandaloneLevelVoice openSecondAlbum;

	// Token: 0x04001011 RID: 4113
	[Space(10f)]
	public StandaloneLevelVoice duckEnd;

	// Token: 0x04001012 RID: 4114
	private bool albumWarningPlayed;

	// Token: 0x04001013 RID: 4115
	private bool secondWarningPlayed;
}
