using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class AudioVoice_loadingScreen : AudioVoice
{
	// Token: 0x060011BB RID: 4539 RVA: 0x00022A88 File Offset: 0x00020E88
	public void startVoice(ConsoleSubMenu_Contacting console)
	{
		if (!this.active)
		{
			return;
		}
		this.consoleScreen = console;
		if (Global.self.firstTimeLoadingGame)
		{
			this.voice = Audio.self.playVoice(this.voiceLine);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		}
		else if (PlayerPrefs.GetString("Data", string.Empty) == Extensions.getUniqueGUIDForThisPCOnly())
		{
			this.voice = Audio.self.playVoice(this.exitOnPack12);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
			PlayerPrefs.DeleteKey("Data");
		}
		else
		{
			this.voice = Audio.self.playVoice(this.onSecondLoad);
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		}
		this.voice.start(true);
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x00022B99 File Offset: 0x00020F99
	private void voiceStopped(VoiceLine line)
	{
		this.isPlaying = false;
		this.consoleScreen.skipButtons = true;
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x00022BB0 File Offset: 0x00020FB0
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "ShowOptions"))
			{
				if (!(markerName == "ShowButtons"))
				{
					if (!(markerName == "getAchievement"))
					{
						if (markerName == "closeGame")
						{
							Application.Quit();
						}
					}
					else if (!SteamworksAPI.AcquireAchievement(AwardName.CUP_ON_EXIT.ToString()))
					{
						AchievementPopup.self.AddAchievement(AwardName.CUP_ON_EXIT);
					}
				}
				else
				{
					this.consoleScreen.showButtons = true;
				}
			}
			else
			{
				this.consoleScreen.showOptions = true;
			}
		}
		int num;
		if (VoiceLine.isMarkerForSubtitles(markerName, out num))
		{
			this.subtitlesMarker = markerName;
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x00022C7B File Offset: 0x0002107B
	public void setSubtitles()
	{
		if (!this.active || this.voice == null || !this.voice.isPlaying())
		{
			return;
		}
		this.voice.updateSubtitles(this.subtitlesMarker);
	}

	// Token: 0x04000ECB RID: 3787
	private ConsoleSubMenu_Contacting consoleScreen;

	// Token: 0x04000ECC RID: 3788
	private string subtitlesMarker;

	// Token: 0x04000ECD RID: 3789
	public StandaloneLevelVoice onSecondLoad;

	// Token: 0x04000ECE RID: 3790
	public StandaloneLevelVoice christmasLines;

	// Token: 0x04000ECF RID: 3791
	public StandaloneLevelVoice halloweenLines;

	// Token: 0x04000ED0 RID: 3792
	public StandaloneLevelVoice halloweenRareLines;

	// Token: 0x04000ED1 RID: 3793
	[HideInInspector]
	public bool isPlaying = true;

	// Token: 0x04000ED2 RID: 3794
	public StandaloneLevelVoice exitOnPack12;
}
