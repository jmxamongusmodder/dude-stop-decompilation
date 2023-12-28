using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class AudioVoice_Painting : AudioVoiceParentChange
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600128B RID: 4747 RVA: 0x00028BBF File Offset: 0x00026FBF
	public bool canFinishLevel
	{
		get
		{
			return !base.checkCondition() || this._canFinishLevel;
		}
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x00028BD4 File Offset: 0x00026FD4
	protected override void whenNewVoiceStarts()
	{
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x00028C08 File Offset: 0x00027008
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceStopped));
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x00028C68 File Offset: 0x00027068
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "EnableExit"))
			{
				if (!(markerName == "EndLevel"))
				{
					if (markerName == "ChangeMusic")
					{
						Audio.self.ChangeMusicParameter("757e3a0a-c20a-4728-ab16-74dc9cf91a6b", "Voice Temper", 0.75f);
					}
				}
				else
				{
					this._canFinishLevel = true;
				}
			}
			else
			{
				Global.self.canExitEndScreen = true;
				Global.self.canBePaused = true;
				base.endVoicedEnding(this.voice);
			}
		}
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x00028D0C File Offset: 0x0002710C
	private void voiceStopped(VoiceLine line)
	{
		this.voiceLine = this.nextVoiceLine;
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x00028D60 File Offset: 0x00027160
	public void finishLevel()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.canExitEndScreen = false;
		Global.self.canBePaused = false;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x00028D84 File Offset: 0x00027184
	public void fixedLevel(Transform paint)
	{
		if (!this.active)
		{
			return;
		}
		if (this.paintingList.Contains(paint))
		{
			this.paintingList.Remove(paint);
		}
		if (this.paintingList.Count <= 0)
		{
			this.voice.setParameter("Multiple", 0f);
			if (this.paintingList.Count == 0)
			{
				this.voice.setParameter("LevelEnd", 0f);
			}
		}
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x00028E08 File Offset: 0x00027208
	public void failedLevel(Transform paint)
	{
		if (!this.active)
		{
			return;
		}
		if (!this.paintingList.Contains(paint))
		{
			this.paintingList.Add(paint);
		}
		if (this.paintingList.Count > 0)
		{
			this.voice.setParameter("Touched", 1f);
			this.voice.setParameter("LevelEnd", 1f);
			if (this.paintingList.Count > 1)
			{
				this.voice.setParameter("Multiple", 1f);
			}
		}
	}

	// Token: 0x04000F92 RID: 3986
	public StandaloneLevelVoice nextVoiceLine;

	// Token: 0x04000F93 RID: 3987
	private List<Transform> paintingList = new List<Transform>();

	// Token: 0x04000F94 RID: 3988
	[HideInInspector]
	private bool _canFinishLevel;
}
