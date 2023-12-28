using System;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class AudioVoiceDefault : AudioVoice
{
	// Token: 0x0600133F RID: 4927 RVA: 0x00015B83 File Offset: 0x00013F83
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.setActiveDefault();
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x00015B9E File Offset: 0x00013F9E
	protected virtual void setActiveDefault()
	{
		this.playStartVoice();
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x00015BA8 File Offset: 0x00013FA8
	protected void playStartVoice()
	{
		if (Audio.self.muteVoiceInEditor)
		{
			return;
		}
		if (Global.self.skipFirstLineOnPack07)
		{
			Global.self.skipFirstLineOnPack07 = false;
			return;
		}
		StandaloneLevelVoiceGuid guidToPlay = AudioVoice.getGuidToPlay(base.transform.name, this.FirstLoad, this.OtherLoad, LevelVoice.Type.Start, Global.self.previousPuzzleSolvedAsMonster, 0);
		if (guidToPlay != null)
		{
			this.voice = Audio.self.playVoice(guidToPlay);
			this.voice.start(true);
		}
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x00015C2C File Offset: 0x0001402C
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Audio.self.muteVoiceInEditor)
		{
			item.SetEnding(LevelVoice.getEndText(base.ps.solvedAsBad == true, Global.self.currLanguage), false);
			return;
		}
		VoiceLine voice;
		string text;
		bool endingToPlay = AudioVoice.getEndingToPlay(base.transform.name, this.FirstLoad, this.OtherLoad, this.TextEndings, base.ps.solvedAsBad == true, out voice, out text);
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = voice;
		if (endingToPlay && this.voice != null)
		{
			base.subscribeToMarkers(item, true);
		}
		else
		{
			this.setEndText(item, text, false);
		}
		if (this.voice != null)
		{
			Global.self.canExitEndScreen = false;
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				Global.self.canExitEndScreen = true;
			});
			this.voice.start(true);
		}
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x00015D56 File Offset: 0x00014156
	protected void playSpecificEnd(StandaloneLevelVoice vLine, endTextControl item)
	{
		this.playSpecificEnd(vLine, item, base.ps.solvedAsBad);
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x00015D6C File Offset: 0x0001416C
	protected void playSpecificEnd(StandaloneLevelVoice vLine, endTextControl item, bool? monster)
	{
		StandaloneLevelVoiceEnd voice = LevelVoice.getVoice(vLine, LevelVoice.Type.End, monster, Global.self.currLanguage);
		if (voice == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Voice wasn't found: \nBank: ",
				vLine.bankName,
				"\nID: ",
				vLine.levelVoiceId,
				"\nSolved: ",
				base.ps.solvedAsBad,
				" monster: ",
				monster
			}));
			return;
		}
		if (string.IsNullOrEmpty(voice.fmodName))
		{
			item.SetEnding(voice.endText, false);
		}
		else
		{
			if (this.voice != null)
			{
				this.voice.stop();
				this.voice = null;
			}
			this.voice = Audio.self.playVoice(voice.entry);
			if (voice.fmodName.Contains("event:/"))
			{
				base.subscribeToMarkers(item, true);
			}
			else
			{
				string endText = voice.endText;
				if (string.IsNullOrEmpty(endText))
				{
					endText = LevelVoice.getEndText(this.TextEndings, null, monster == true, Global.self.currLanguage, false);
				}
				item.SetEnding(endText, false);
			}
			Global.self.canExitEndScreen = false;
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				Global.self.canExitEndScreen = true;
			});
			this.voice.start(true);
		}
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x00015EEE File Offset: 0x000142EE
	protected virtual void setEndText(endTextControl item, string text, bool controlled)
	{
		item.SetEnding(text, controlled);
	}

	// Token: 0x04001035 RID: 4149
	[Header("Puzzle lines")]
	public StandaloneLevelVoice FirstLoad;

	// Token: 0x04001036 RID: 4150
	public StandaloneLevelVoice OtherLoad;

	// Token: 0x04001037 RID: 4151
	public StandaloneLevelVoice TextEndings;
}
