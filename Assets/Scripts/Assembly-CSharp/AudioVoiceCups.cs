using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class AudioVoiceCups : AudioVoice
{
	// Token: 0x0600133C RID: 4924 RVA: 0x0001ACF4 File Offset: 0x000190F4
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
		if (string.IsNullOrEmpty(this.startVoice.bankName))
		{
			return;
		}
		StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(this.startVoice, LevelVoice.Type.Start, null);
		if (voice != null)
		{
			this.voice = Audio.self.playVoice(voice);
		}
		else
		{
			this.voice = Audio.self.playVoice(this.startVoice);
		}
		this.voice.start(true);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x0001AD90 File Offset: 0x00019190
	public override void subsctibeToEnding(endTextControl item)
	{
		StandaloneLevelVoice standaloneLevelVoice;
		if (string.IsNullOrEmpty(this.endVoice.bankName))
		{
			standaloneLevelVoice = this.startVoice;
		}
		else
		{
			standaloneLevelVoice = this.endVoice;
		}
		StandaloneLevelVoiceEnd voice = LevelVoice.getVoice(standaloneLevelVoice, LevelVoice.Type.End, new bool?(true), Global.self.currLanguage);
		if (voice == null)
		{
			Debug.LogError("Voice wasn't found: \nBank: " + standaloneLevelVoice.bankName + "\nID: " + standaloneLevelVoice.levelVoiceId);
			return;
		}
		if (voice.entry.guid != Guid.Empty)
		{
			if (this.voice != null)
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(voice.entry);
			if (voice.fmodName.Contains("event:/"))
			{
				base.subscribeToMarkers(item, true);
			}
			else
			{
				item.SetEnding(voice.endText, false);
			}
			this.voice.start(true);
		}
		else
		{
			item.SetEnding(voice.endText, false);
		}
	}

	// Token: 0x04001033 RID: 4147
	[Space(10f)]
	public StandaloneLevelVoice startVoice;

	// Token: 0x04001034 RID: 4148
	public StandaloneLevelVoice endVoice;
}
