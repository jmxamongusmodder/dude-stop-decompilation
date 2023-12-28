using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class AudioVoice_Pack12_Duck : AudioVoice
{
	// Token: 0x0600126C RID: 4716 RVA: 0x0002820E File Offset: 0x0002660E
	private void Awake()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0002821D File Offset: 0x0002661D
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		Audio.self.ChangeMusicParameterOverTime("ab175daa-8759-4af9-b3b0-74df51ee0d24", "Pack12 Broken", this.breakMusicOver);
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0002824C File Offset: 0x0002664C
	public void playFirstLine()
	{
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x0002828C File Offset: 0x0002668C
	public void playSecondStart()
	{
		this.voice = Audio.self.playVoice(AudioVoice.getNotRepeatingVoice(base.name, this.secondStart, LevelVoice.Type.Start, null));
		this.voice.start(true);
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x000282D0 File Offset: 0x000266D0
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "mailList"))
			{
				if (!(markerName == "contacts"))
				{
					if (!(markerName == "showClose"))
					{
						if (!(markerName == "allowClick"))
						{
							if (markerName == "HeartBeat")
							{
								Audio.self.playLoopSound("1860bea6-b9ce-462e-aeb5-055970f3a421");
								this.isHeartBeatPlaying = true;
							}
						}
						else
						{
							this.allowClick = true;
						}
					}
					else
					{
						this.showClose = true;
					}
				}
				else
				{
					this.contacts = true;
				}
			}
			else
			{
				this.mailList = true;
			}
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x0002838C File Offset: 0x0002678C
	public void playWaitingLine(int ind)
	{
		if (ind == 0)
		{
			this.voice = Audio.self.playVoice(this.waitLine1);
		}
		else
		{
			if (ind != 1)
			{
				return;
			}
			this.voice = Audio.self.playVoice(this.waitLine2);
		}
		this.voice.start(true);
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000283EC File Offset: 0x000267EC
	public void playWaitLineYesNo(int ind)
	{
		if (ind <= this.waitYesNoLines.Length - 1)
		{
			this.voice = Audio.self.playVoice(this.waitYesNoLines[ind]);
		}
		else
		{
			if (ind != this.waitYesNoLines.Length)
			{
				return;
			}
			this.voice = Audio.self.playVoice(this.onExitLine);
			PlayerPrefs.SetString("Data", Extensions.getUniqueGUIDForThisPCOnly());
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				PlayerPrefs.DeleteKey("Data");
			});
		}
		this.voice.start(true);
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x00028494 File Offset: 0x00026894
	public bool isPlaying()
	{
		return this.voice != null && this.voice.isPlaying();
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000284AF File Offset: 0x000268AF
	public void stopVoice()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000284D7 File Offset: 0x000268D7
	public void playShareLine()
	{
		this.voice = Audio.self.playVoice(this.afterConsoleLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00028514 File Offset: 0x00026914
	public void playLastLine(bool yes)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
		Audio.self.StopMusicParameterOverTime();
		if (this.isHeartBeatPlaying)
		{
			Audio.self.stopLoopSound("1860bea6-b9ce-462e-aeb5-055970f3a421", true);
		}
		this.YESClicked = yes;
		if (yes)
		{
			this.voice = Audio.self.playVoice(this.yesLine);
		}
		else if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.noLine.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.noLine);
		}
		else
		{
			this.voice = Audio.self.playVoice(AudioVoice.getNotRepeatingVoice(base.name, this.secondStart, LevelVoice.Type.End, null));
		}
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.showContinue = true;
		});
		this.voice.start(true);
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0002862F File Offset: 0x00026A2F
	public void bContinue()
	{
		if (!this.YESClicked)
		{
			Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
		}
	}

	// Token: 0x04000F77 RID: 3959
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000F78 RID: 3960
	public StandaloneLevelVoice waitLine1;

	// Token: 0x04000F79 RID: 3961
	public StandaloneLevelVoice waitLine2;

	// Token: 0x04000F7A RID: 3962
	public StandaloneLevelVoice afterConsoleLine;

	// Token: 0x04000F7B RID: 3963
	public StandaloneLevelVoice[] waitYesNoLines;

	// Token: 0x04000F7C RID: 3964
	public StandaloneLevelVoice onExitLine;

	// Token: 0x04000F7D RID: 3965
	public StandaloneLevelVoice yesLine;

	// Token: 0x04000F7E RID: 3966
	public StandaloneLevelVoice noLine;

	// Token: 0x04000F7F RID: 3967
	public StandaloneLevelVoice secondStart;

	// Token: 0x04000F80 RID: 3968
	[HideInInspector]
	public bool mailList;

	// Token: 0x04000F81 RID: 3969
	[HideInInspector]
	public bool contacts;

	// Token: 0x04000F82 RID: 3970
	[HideInInspector]
	public bool showClose;

	// Token: 0x04000F83 RID: 3971
	[HideInInspector]
	public bool allowClick;

	// Token: 0x04000F84 RID: 3972
	[HideInInspector]
	public bool showContinue;

	// Token: 0x04000F85 RID: 3973
	[Space(10f)]
	public float breakMusicOver = 25f;

	// Token: 0x04000F86 RID: 3974
	private bool isHeartBeatPlaying;

	// Token: 0x04000F87 RID: 3975
	private bool YESClicked;
}
