using System;

// Token: 0x020002CA RID: 714
public class AudioVoice_JigSawPuzzle : AudioVoice
{
	// Token: 0x06001195 RID: 4501 RVA: 0x00021750 File Offset: 0x0001FB50
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Global.self.unlockedJigsawPieces == 20 && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.allCollected.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.allCollected);
		}
		else if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes == 0 && Global.self.unlockedJigsawPieces < 20)
		{
			this.voice = Audio.self.playVoice(this.introLine);
		}
		else if (Global.self.unlockedJigsawPieces > 5 && Global.self.unlockedJigsawPieces < 20 && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.songLine.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.songLine);
		}
		if (this.voice == null)
		{
			return;
		}
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
			global::Console.self.canOpen = true;
		});
		this.voice.start(true);
		this.canExit = false;
		global::Console.self.canOpen = false;
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000218A8 File Offset: 0x0001FCA8
	public void placeWrongPiece()
	{
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.rndLines[this.rndLinesInd]);
		this.voice.start(true);
		this.rndLinesInd++;
		if (this.rndLinesInd >= this.rndLines.Length)
		{
			this.rndLinesInd = 0;
		}
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0002191C File Offset: 0x0001FD1C
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Audio.self.muteVoiceInEditor)
		{
			item.SetEnding(LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage), false);
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		bool flag = false;
		if (base.ps.solvedAsBad == true)
		{
			if (SerializablePuzzleStats.Get(base.transform.name).solvedAsBad == 1)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack02.JigSaw_badEnd);
				flag = true;
			}
		}
		else if (SerializablePuzzleStats.Get(base.transform.name).solvedAsGood == 1)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack02.JigSaw_goodEnd);
			flag = true;
		}
		if (flag)
		{
			base.subsctibeToEnding(item);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(base.endVoicedEnding));
			this.voice.start(true);
		}
		else
		{
			item.SetEnding(LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage), false);
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x00021A9A File Offset: 0x0001FE9A
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		return !this.active || this.canExit;
	}

	// Token: 0x04000EA2 RID: 3746
	private bool canExit = true;

	// Token: 0x04000EA3 RID: 3747
	public StandaloneLevelVoice introLine;

	// Token: 0x04000EA4 RID: 3748
	public StandaloneLevelVoice songLine;

	// Token: 0x04000EA5 RID: 3749
	public StandaloneLevelVoice allCollected;

	// Token: 0x04000EA6 RID: 3750
	public StandaloneLevelVoice[] rndLines;

	// Token: 0x04000EA7 RID: 3751
	private int rndLinesInd;
}
