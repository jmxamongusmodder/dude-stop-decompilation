using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class AudioVoiceRapidFire : AudioVoice
{
	// Token: 0x06001358 RID: 4952 RVA: 0x0002F388 File Offset: 0x0002D788
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Audio.self.muteVoiceInEditor)
		{
			return;
		}
		if (Global.self.CountPackPlayedTimes(0) == 0 && AwardController.self != null)
		{
			switch (AudioVoiceRapidFire.puzzleIndex)
			{
			case 0:
				this.voice = Audio.self.playVoice(Voices.VoicePack08.FirstPlay_1);
				break;
			case 1:
				this.voice = Audio.self.playVoice(Voices.VoicePack08.FirstPlay_2);
				break;
			case 2:
				this.voice = Audio.self.playVoice(Voices.VoicePack08.FirstPlay_3);
				break;
			case 4:
				if (AwardController.self.getProgress(true) > 50)
				{
					this.voice = Audio.self.playVoice(Voices.VoicePack08.FirstPlay_5);
				}
				else
				{
					this.voice = Audio.self.playVoice(Voices.VoicePack08.FirstPlay_4);
				}
				break;
			}
			if (this.voice != null)
			{
				this.voice.start(true);
			}
			AudioVoiceRapidFire.puzzleIndex++;
			return;
		}
		float num = Mathf.Max(0.1f, (10f - (float)Global.self.CountPackPlayedTimes(0)) * 0.1f);
		if (UnityEngine.Random.value > num)
		{
			return;
		}
		bool? monster = null;
		if (this.hasGoodBadStart)
		{
			monster = new bool?(base.ps.goBadAfterTime);
		}
		StandaloneLevelVoiceGuid guidToPlay = AudioVoice.getGuidToPlay(base.transform.name, null, this.voiceLine, LevelVoice.Type.Start, monster, -1);
		if (guidToPlay != null)
		{
			this.voice = Audio.self.playVoice(guidToPlay);
			this.voice.start(true);
		}
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x0002F54C File Offset: 0x0002D94C
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Audio.self.muteVoiceInEditor)
		{
			return;
		}
		float num = Mathf.Max(0.1f, (10f - (float)Global.self.CountPackPlayedTimes(0)) * 0.1f);
		if ((this.voice == null || !this.voice.isPlaying()) && !Audio.self.muteVoiceInEditor && Global.self.CountPackPlayedTimes(0) > 0 && UnityEngine.Random.value <= num)
		{
			StandaloneLevelVoiceGuid guidToPlay = AudioVoice.getGuidToPlay(base.transform.name, null, this.voiceLine, LevelVoice.Type.End, new bool?(base.ps.solvedAsBad == true), -1);
			if (guidToPlay != null)
			{
				this.voice = Audio.self.playVoice(guidToPlay);
				this.voice.start(true);
			}
		}
		item.SetEnding(LevelVoice.getEndText(Voices.VoicePack08.EndText, null, base.ps.solvedAsBad == true, Global.self.currLanguage, false), false);
	}

	// Token: 0x04001042 RID: 4162
	public static int puzzleIndex;

	// Token: 0x04001043 RID: 4163
	public bool hasGoodBadStart;
}
