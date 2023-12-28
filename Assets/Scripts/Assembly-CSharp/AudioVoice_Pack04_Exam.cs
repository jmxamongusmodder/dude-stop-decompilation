using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D8 RID: 728
public class AudioVoice_Pack04_Exam : AudioVoice
{
	// Token: 0x060011F6 RID: 4598 RVA: 0x00024C18 File Offset: 0x00023018
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Global.self.justFinishedExamGoodScore == -1f)
		{
			return;
		}
		if (Global.self.justFinishedExamGoodScore >= 99f)
		{
			this.unlockPack = Global.self.unlockNextPack;
			Global.self.unlockNextPack = false;
			if (Global.self.CountPackPlayedTimes(0) == 1)
			{
				this.voice = Audio.self.playVoice(this.solvedGood);
				this.voice.subscribeToStopped(this, delegate(VoiceLine line)
				{
					base.StartCoroutine(this.sayAfterGood());
				});
				this.voice.start(true);
				this.canStart = false;
			}
			else if (Global.self.CountPackPlayedTimes(false, 0) == 1)
			{
				this.voice = Audio.self.playVoice(this.solvedGoodSecond);
				this.voice.subscribeToStopped(this, delegate(VoiceLine line)
				{
					base.StartCoroutine(this.sayAfterGood());
				});
				this.voice.start(true);
				this.canStart = false;
			}
		}
		else if (Global.self.justFinishedExamGoodScore <= 20f)
		{
			if (Global.self.CountPackPlayedTimes(true, 0) == 1 && Global.self.CountPackPlayedTimes(false, 0) == 0)
			{
				this.voice = Audio.self.playVoice(this.solvedBad);
				this.voice.subscribeToStopped(this, delegate(VoiceLine line)
				{
					this.canStart = true;
					base.SetPackStartButton(true);
				});
				this.voice.start(true);
				this.badEndPlaying = true;
				this.canStart = false;
			}
		}
		else if (Global.self.CountPackPlayedTimes(0) == 1)
		{
			this.voice = Audio.self.playVoice(this.almostGood);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.canStart = true;
				base.SetPackStartButton(true);
			});
			this.voice.start(true);
			this.canStart = false;
		}
		Global.self.justFinishedExamGoodScore = -1f;
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x00024E10 File Offset: 0x00023210
	private IEnumerator sayAfterGood()
	{
		yield return new WaitForSeconds(0.5f);
		this.voice = Audio.self.playVoice(this.solvedGoodContinue);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canStart = true;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x00024E2B File Offset: 0x0002322B
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "Unlock")
		{
			Global.self.unlockNextPack = this.unlockPack;
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x00024E58 File Offset: 0x00023258
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (this.startActivated || !this.canStart)
		{
			base.SetPackStartButton(false);
			return false;
		}
		if (!this.active)
		{
			return true;
		}
		if (this.badEndPlaying && click == ClickWhileVoice.start)
		{
			if (this.voice != null)
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.solvedBad_Start);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
			});
			this.voice.start(true);
			this.startActivated = true;
			this.badEndPlaying = false;
			base.SetPackStartButton(false);
			return false;
		}
		if (click == ClickWhileVoice.start)
		{
			Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
		}
		return true;
	}

	// Token: 0x04000F0D RID: 3853
	[Space(10f)]
	public StandaloneLevelVoice almostGood;

	// Token: 0x04000F0E RID: 3854
	public StandaloneLevelVoice solvedBad;

	// Token: 0x04000F0F RID: 3855
	public StandaloneLevelVoice solvedBad_Start;

	// Token: 0x04000F10 RID: 3856
	public StandaloneLevelVoice solvedGood;

	// Token: 0x04000F11 RID: 3857
	public StandaloneLevelVoice solvedGoodSecond;

	// Token: 0x04000F12 RID: 3858
	public StandaloneLevelVoice solvedGoodContinue;

	// Token: 0x04000F13 RID: 3859
	private bool badEndPlaying;

	// Token: 0x04000F14 RID: 3860
	private bool startActivated;

	// Token: 0x04000F15 RID: 3861
	private bool canStart = true;

	// Token: 0x04000F16 RID: 3862
	private bool unlockPack;
}
