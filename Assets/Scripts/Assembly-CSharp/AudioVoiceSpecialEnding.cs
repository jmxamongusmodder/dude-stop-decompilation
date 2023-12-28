using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class AudioVoiceSpecialEnding : AudioVoiceDefault
{
	// Token: 0x06001369 RID: 4969 RVA: 0x0002394E File Offset: 0x00021D4E
	protected override void setActiveDefault()
	{
		base.setActiveDefault();
		this.badSoFar = (AwardController.self != null && AwardController.self.getCurrentProgress(true) == 100);
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x00023980 File Offset: 0x00021D80
	protected bool playEndOnBadProgress(endTextControl item)
	{
		if (SerializableGameStats.self.isGameFinished)
		{
			return false;
		}
		if (!this.badSoFar)
		{
			return false;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.ENDFirst.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.ENDFirst);
		}
		else
		{
			StandaloneLevelVoiceGuid standaloneLevelVoiceGuid = AudioVoice.getNotRepeatingVoice(base.transform.name, this.ENDEnd, LevelVoice.Type.End, new bool?(base.ps.solvedAsBad == true));
			if (standaloneLevelVoiceGuid == null)
			{
				standaloneLevelVoiceGuid = LevelVoice.getVoice(this.ENDEnd, LevelVoice.Type.End, new bool?(base.ps.solvedAsBad == true));
			}
			this.voice = Audio.self.playVoice(standaloneLevelVoiceGuid);
		}
		if (this.voice.info.fmodName.Contains("event:/"))
		{
			base.subscribeToMarkers(item, true);
		}
		else
		{
			item.SetEnding(LevelVoice.getEndText(this.voice.info, null, base.ps.solvedAsBad == true, Global.self.currLanguage), false);
			Global.self.canExitEndScreen = false;
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				Global.self.canExitEndScreen = true;
			});
		}
		this.voice.start(true);
		return true;
	}

	// Token: 0x04001045 RID: 4165
	[Space(10f)]
	public StandaloneLevelVoice ENDFirst;

	// Token: 0x04001046 RID: 4166
	public StandaloneLevelVoice ENDEnd;

	// Token: 0x04001047 RID: 4167
	private bool badSoFar;
}
