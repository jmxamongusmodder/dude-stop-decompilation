using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class AudioVoice_Pack08 : AudioVoice
{
	// Token: 0x06001218 RID: 4632 RVA: 0x00025C38 File Offset: 0x00024038
	private void Awake()
	{
		SerializablePuzzleStats serializablePuzzleStats = SerializablePuzzleStats.Get(base.transform.name);
		if (serializablePuzzleStats.loadedTimes == 0 && Global.self.DuckEnabled)
		{
			this.duckCup.SetActive(true);
			this.duckShowed = true;
		}
		else
		{
			this.duckCup.SetActive(false);
		}
		if (Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(false, 0) == 0 && Global.self.CountPackPlayedTimes(true, 0) == 1)
		{
			this.playBadLine = true;
			base.StartCoroutine(this.hideButton());
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(true, 0) == 0 && Global.self.CountPackPlayedTimes(false, 0) == 1)
		{
			this.playGoodLine = true;
			base.StartCoroutine(this.hideButton());
		}
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x00025D28 File Offset: 0x00024128
	private IEnumerator hideButton()
	{
		yield return null;
		base.SetPackStartButton(false);
		yield break;
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00025D44 File Offset: 0x00024144
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (this.duckShowed)
		{
			this.voice = Audio.self.playVoice(this.duckStart);
			this.voice.start(true);
			base.StartCoroutine(this.waitLine());
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
			this.voice = null;
		}
		if (this.playBadLine)
		{
			this.voice = Audio.self.playVoice(this.afterBad);
		}
		else if (this.playGoodLine)
		{
			this.voice = Audio.self.playVoice(this.afterGood);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.allowExit = true;
				base.SetPackStartButton(true);
			});
		}
		if (this.voice == null)
		{
			return;
		}
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		this.allowExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00025E80 File Offset: 0x00024280
	private void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.H))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.afterBad);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.start(true);
		}
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x00025F0C File Offset: 0x0002430C
	public void killDuck()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.MenuMusic, true);
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.duckNo);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.allowExit = true;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		Global.self.unlockNextPack = this.unlockPack;
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x00025F98 File Offset: 0x00024398
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Unlock"))
			{
				if (markerName == "ShowDuck")
				{
					Audio.self.playOneShot("d50e2e51-e976-4b01-ae8c-8c4257c8c4e1", 1f);
					Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
					this.flyingDuck.SetActive(true);
				}
			}
			else
			{
				Global.self.unlockNextPack = this.unlockPack;
			}
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x00026020 File Offset: 0x00024420
	private IEnumerator waitLine()
	{
		this.playWait = true;
		yield return new WaitForSeconds(5f);
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		if (!this.playWait)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.duckComeOn);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x0002603C File Offset: 0x0002443C
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.active)
		{
			return true;
		}
		if (!this.duckShowed)
		{
			if (this.allowExit)
			{
				base.StartMusic(click);
			}
			return this.allowExit;
		}
		this.playWait = false;
		if (click == ClickWhileVoice.start)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.duckFine);
			this.voice.start(true);
			base.StartMusic(ClickWhileVoice.start);
			return true;
		}
		if (click == ClickWhileVoice.pack)
		{
			Global.self.playIWasRightOnPack07 = true;
			return true;
		}
		return true;
	}

	// Token: 0x04000F31 RID: 3889
	[Space(10f)]
	public GameObject duckCup;

	// Token: 0x04000F32 RID: 3890
	public StandaloneLevelVoice duckStart;

	// Token: 0x04000F33 RID: 3891
	public StandaloneLevelVoice duckFine;

	// Token: 0x04000F34 RID: 3892
	public StandaloneLevelVoice duckComeOn;

	// Token: 0x04000F35 RID: 3893
	private bool duckShowed;

	// Token: 0x04000F36 RID: 3894
	private bool playWait;

	// Token: 0x04000F37 RID: 3895
	[Space(10f)]
	public StandaloneLevelVoice afterBad;

	// Token: 0x04000F38 RID: 3896
	public StandaloneLevelVoice duckNo;

	// Token: 0x04000F39 RID: 3897
	public StandaloneLevelVoice afterGood;

	// Token: 0x04000F3A RID: 3898
	private bool allowExit = true;

	// Token: 0x04000F3B RID: 3899
	private bool unlockPack;

	// Token: 0x04000F3C RID: 3900
	public GameObject flyingDuck;

	// Token: 0x04000F3D RID: 3901
	private bool playBadLine;

	// Token: 0x04000F3E RID: 3902
	private bool playGoodLine;
}
