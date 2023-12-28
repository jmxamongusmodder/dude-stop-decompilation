using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BC RID: 700
public class AudioVoice_CupSmallest : AudioVoice
{
	// Token: 0x06001130 RID: 4400 RVA: 0x0001E2C0 File Offset: 0x0001C6C0
	public void start()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.active)
		{
			return;
		}
		Global.self.canBePaused = false;
		this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_HereIsYour);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.allowExit));
		this.voice.start(true);
		base.StartCoroutine(this.idleVoice());
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x0001E34F File Offset: 0x0001C74F
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "shaffle")
		{
			this.canShaffle = true;
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x0001E370 File Offset: 0x0001C770
	private void allowExit(VoiceLine line)
	{
		Global.self.canBePaused = true;
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0001E380 File Offset: 0x0001C780
	public override void subsctibeToEnding(endTextControl item)
	{
		if (!this.done)
		{
			this.lastVoice();
		}
		string endText = LevelVoice.getEndText(this.endLine, Global.self.currLanguage);
		item.SetEnding(endText, false);
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0001E3BC File Offset: 0x0001C7BC
	public IEnumerator idleVoice()
	{
		while (this.voice.isPlaying())
		{
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(5f);
		if (this.active && !this.touched && (this.voice == null || (this.voice != null && this.voice.removed)))
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_ComeOn);
			this.voice.start(true);
		}
		yield return new WaitForSeconds(5f);
		if (this.active && !this.touched && (this.voice == null || (this.voice != null && this.voice.removed)))
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_NoTricks);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0001E3D7 File Offset: 0x0001C7D7
	private void voiceEnded(VoiceLine line)
	{
		Global.self.canExitEndScreen = true;
		Global.self.canBePaused = true;
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0001E3F0 File Offset: 0x0001C7F0
	public void touch(string name)
	{
		if (!this.active || this.done)
		{
			return;
		}
		this.touched = true;
		if (name.Contains("2"))
		{
			if (this.voice != null)
			{
				this.voice.stop();
			}
			this.lastVoice();
		}
		else
		{
			if (name == this.prevID)
			{
				return;
			}
			if (this.voice != null)
			{
				this.voice.stop();
			}
			if (this.count == 0)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_NoNotThis);
				this.voice.start(true);
			}
			else if (this.count == 1)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_NotThis);
				this.voice.start(true);
			}
			this.prevID = name;
			this.count++;
		}
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0001E4E8 File Offset: 0x0001C8E8
	private void lastVoice()
	{
		this.voice = Audio.self.playVoice(Voices.VoicePack03.CupSmallest_HaILied);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceEnded));
		this.voice.start(true);
		Global.self.canBePaused = false;
		Global.self.canExitEndScreen = false;
		this.done = true;
	}

	// Token: 0x04000E38 RID: 3640
	[Space(10f)]
	public StandaloneLevelVoice endLine;

	// Token: 0x04000E39 RID: 3641
	private bool touched;

	// Token: 0x04000E3A RID: 3642
	private string prevID;

	// Token: 0x04000E3B RID: 3643
	private bool done;

	// Token: 0x04000E3C RID: 3644
	private int count;

	// Token: 0x04000E3D RID: 3645
	public bool canShaffle;
}
