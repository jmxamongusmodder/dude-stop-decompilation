using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class AudioVoice_WrongRecycleBin : AudioVoice
{
	// Token: 0x06001335 RID: 4917 RVA: 0x0002EFE0 File Offset: 0x0002D3E0
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		global::Console.self.canOpen = false;
		base.ps.UIScreenSecondary = base.ps.UIScreenCurr;
		base.ps.UIScreenCurr = null;
		base.ps.UIScreen = null;
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.pressEsc));
		this.voice.start(true);
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x0002F074 File Offset: 0x0002D474
	private void pressEsc(VoiceLine line)
	{
		if (!AudioVoice_Pack09_Intro_1.escapePressed && !this.interrupted)
		{
			this.voice = Audio.self.playVoice(this.pressESC);
			this.voice.start(true);
			base.StartCoroutine(this.waitForESC());
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x0002F0C8 File Offset: 0x0002D4C8
	private IEnumerator waitForESC()
	{
		while (!Input.GetButtonDown("Cancel"))
		{
			yield return null;
		}
		if (this.voice == null || this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.NoESC);
		this.voice.start(true);
		yield break;
		yield break;
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x0002F0E4 File Offset: 0x0002D4E4
	public void throwObject()
	{
		if (this.throwIndex >= this.throwLine.Length)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.throwLine[this.throwIndex]);
		this.voice.start(true);
		this.throwIndex++;
		this.interrupted = true;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x0002F169 File Offset: 0x0002D569
	public void snapObject()
	{
		if (this.throwIndex == 2)
		{
			this.throwObject();
		}
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0002F180 File Offset: 0x0002D580
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.onEnd);
		base.subscribeToMarkers(item, true);
		this.voice.start(true);
		if (AwardController.self != null)
		{
			AwardController.self.removeBadSolution();
		}
	}

	// Token: 0x0400102C RID: 4140
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x0400102D RID: 4141
	public StandaloneLevelVoice onEnd;

	// Token: 0x0400102E RID: 4142
	public StandaloneLevelVoice pressESC;

	// Token: 0x0400102F RID: 4143
	public StandaloneLevelVoice NoESC;

	// Token: 0x04001030 RID: 4144
	public StandaloneLevelVoice[] throwLine;

	// Token: 0x04001031 RID: 4145
	private int throwIndex;

	// Token: 0x04001032 RID: 4146
	private bool interrupted;
}
