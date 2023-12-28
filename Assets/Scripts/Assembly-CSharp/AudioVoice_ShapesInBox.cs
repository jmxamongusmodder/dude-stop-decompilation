using System;
using System.Linq;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class AudioVoice_ShapesInBox : AudioVoiceReceive
{
	// Token: 0x060012EA RID: 4842 RVA: 0x0002D234 File Offset: 0x0002B634
	public void onLidOpen()
	{
		if (this.openedOnce)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.openLine.levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.openLine);
			this.voice.start(true);
		}
		this.openedOnce = true;
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x0002D2C4 File Offset: 0x0002B6C4
	public void inWrongHole()
	{
		if (this.wrongHoleInd >= this.wrongHoleLine.Length)
		{
			return;
		}
		if (this.wrongHolePlaying)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.wrongHoleLine[this.wrongHoleInd].levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.wrongHoleLine[this.wrongHoleInd++]);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.wrongHolePlaying = false;
			});
			this.voice.start(true);
			this.wrongHolePlaying = true;
		}
		else
		{
			this.wrongHoleInd++;
			this.inWrongHole();
		}
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0002D3AF File Offset: 0x0002B7AF
	public void inHole()
	{
		this.objectIsIn(true);
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x0002D3B8 File Offset: 0x0002B7B8
	public void missHole()
	{
		this.objectIsIn(false);
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x0002D3C4 File Offset: 0x0002B7C4
	private void objectIsIn(bool In)
	{
		if (UnityEngine.Random.value + 0.2f < (float)Global.self.CountPackPlayedTimes(0) * 0.1f)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		if (this.currentLine == null)
		{
			string prev = SerializablePuzzleStats.Get(base.transform.name).getPrevEnding(In);
			this.currentLine = (from x in this.endings
			where x.firstIn == In && x.firstLine.levelVoiceId != prev
			select x).ToList<AudioVoice_ShapesInBox.Lines>().GetRandom<AudioVoice_ShapesInBox.Lines>();
			SerializablePuzzleStats.Get(base.transform.name).savePrevEnding(this.currentLine.firstLine.levelVoiceId, In);
			this.voice = Audio.self.playVoice(this.currentLine.firstLine);
			this.voice.start(true);
		}
		else
		{
			StandaloneLevelVoice entry;
			if (In)
			{
				entry = this.currentLine.secondInLine;
			}
			else
			{
				entry = this.currentLine.secondMissLine;
			}
			this.voice = Audio.self.playVoice(entry);
			this.endText = LevelVoice.getEndText(entry, Global.self.currLanguage);
			this.voice.start(true);
		}
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x0002D539 File Offset: 0x0002B939
	public override void subsctibeToEnding(endTextControl item)
	{
		if (!string.IsNullOrEmpty(this.endText))
		{
			item.SetEnding(this.endText, false);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x04000FE9 RID: 4073
	[Space(10f)]
	public StandaloneLevelVoice openLine;

	// Token: 0x04000FEA RID: 4074
	public StandaloneLevelVoice[] wrongHoleLine;

	// Token: 0x04000FEB RID: 4075
	private bool wrongHolePlaying;

	// Token: 0x04000FEC RID: 4076
	private int wrongHoleInd;

	// Token: 0x04000FED RID: 4077
	private bool openedOnce;

	// Token: 0x04000FEE RID: 4078
	[Space(10f)]
	public AudioVoice_ShapesInBox.Lines[] endings;

	// Token: 0x04000FEF RID: 4079
	private AudioVoice_ShapesInBox.Lines currentLine;

	// Token: 0x04000FF0 RID: 4080
	private string endText;

	// Token: 0x020002F5 RID: 757
	[Serializable]
	public class Lines
	{
		// Token: 0x04000FF1 RID: 4081
		public bool firstIn = true;

		// Token: 0x04000FF2 RID: 4082
		public StandaloneLevelVoice firstLine;

		// Token: 0x04000FF3 RID: 4083
		public StandaloneLevelVoice secondInLine;

		// Token: 0x04000FF4 RID: 4084
		public StandaloneLevelVoice secondMissLine;
	}
}
