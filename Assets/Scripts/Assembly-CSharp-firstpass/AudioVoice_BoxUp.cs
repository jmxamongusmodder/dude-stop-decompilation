using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A0 RID: 672
public class AudioVoice_BoxUp : AudioVoiceParentChange
{
	// Token: 0x06001069 RID: 4201 RVA: 0x0001672C File Offset: 0x00014B2C
	protected override void whenNewVoiceStarts()
	{
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x00016747 File Offset: 0x00014B47
	protected override void whenPreviousVoiceStops(VoiceLine line)
	{
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.start(true);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x00016784 File Offset: 0x00014B84
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "Save":
			this.sentenceStart = this.voice.getPosition();
			break;
		case "InterruptEnd":
			this.voice.gotoPosition(this.sentenceStart);
			break;
		case "Touched":
			this.voice.setParameter("BoxTouched", 0f);
			break;
		case "EI":
			this.interruptionAllowed = true;
			break;
		case "DI":
			this.interruptionAllowed = false;
			break;
		case "Waiting":
			this.canStopSpeech = false;
			break;
		case "PointingDown":
			this.voice.setParameter("PointingDown", 0f);
			break;
		case "First":
			this.voice.setParameter("InterruptNum", 0f);
			break;
		case "ShowLogo":
			Camera.main.GetComponent<BackgroundControl>().swapInstantly(this.newBG.transform);
			foreach (GameObject gameObject in this.listToHide)
			{
				gameObject.SetActive(false);
			}
			base.ps.UIScreenCurr.GetComponent<AbstractUIScreen>().removeScreen();
			base.ps.UIScreenCurr = null;
			base.ps.UIScreen = this.UIEndScreen.transform;
			UIControl.makeUIScreen(base.transform);
			break;
		case "HideLogo":
			base.StartCoroutine(this.gotoNextLevel());
			break;
		case "StopMusic":
			Audio.self.StopMusic("757e3a0a-c20a-4728-ab16-74dc9cf91a6b");
			break;
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x000169D8 File Offset: 0x00014DD8
	private IEnumerator gotoNextLevel()
	{
		yield return null;
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x000169EC File Offset: 0x00014DEC
	public void pointingDown()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.pointingDownPlayed)
		{
			this.voice.setParameter("PointingDown", 1f);
			this.pointingDownPlayed = true;
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x00016A24 File Offset: 0x00014E24
	public void boxTouched()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.interruptionAllowed)
		{
			return;
		}
		if (this.canStopSpeech)
		{
			if (this.interruptCount == 0)
			{
				this.voice.setParameter("InterruptNum", (float)(++this.interruptCount));
			}
			else if (this.interruptCount == 1 && UnityEngine.Random.value > 0.6f)
			{
				this.voice.setParameter("InterruptNum", (float)(++this.interruptCount));
				this.canStopSpeech = false;
			}
			return;
		}
		if (++this.interruptCount > 5 && UnityEngine.Random.value > 0.5f)
		{
			this.voice.setParameter("BoxTouched", 1f);
			this.interruptCount = 0;
		}
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00016B10 File Offset: 0x00014F10
	public void finishLevel()
	{
		if (!this.active)
		{
			return;
		}
		this.voice.setParameter("LevelEnd", 1f);
		Global.self.canExitEndScreen = false;
		Global.self.canBePaused = false;
		Audio.self.ChangeMusicParameter("757e3a0a-c20a-4728-ab16-74dc9cf91a6b", "Voice Temper", 1f);
	}

	// Token: 0x04000D72 RID: 3442
	private int sentenceStart = -1;

	// Token: 0x04000D73 RID: 3443
	private bool interruptionAllowed;

	// Token: 0x04000D74 RID: 3444
	private int interruptCount;

	// Token: 0x04000D75 RID: 3445
	private bool canStopSpeech = true;

	// Token: 0x04000D76 RID: 3446
	private bool pointingDownPlayed;

	// Token: 0x04000D77 RID: 3447
	[Space(10f)]
	public GameObject UIEndScreen;

	// Token: 0x04000D78 RID: 3448
	[Space(10f)]
	public GameObject newBG;

	// Token: 0x04000D79 RID: 3449
	public GameObject[] listToHide;
}
