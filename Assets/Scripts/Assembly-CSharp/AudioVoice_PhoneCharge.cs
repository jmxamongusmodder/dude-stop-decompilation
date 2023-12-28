using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class AudioVoice_PhoneCharge : AudioVoiceDefault
{
	// Token: 0x0600129A RID: 4762 RVA: 0x00029085 File Offset: 0x00027485
	private void Awake()
	{
		if (AudioVoice_PhoneCharge.checkIfDuck())
		{
			Audio.self.loadBank(this.bankList, true);
		}
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000290A4 File Offset: 0x000274A4
	public override void setActive(bool on)
	{
		if (!AudioVoice_PhoneCharge.checkIfDuck())
		{
			base.setActive(on);
		}
		else if (on)
		{
			this.active = on;
			StandaloneLevelVoiceGuid notRepeatingVoice = AudioVoice.getNotRepeatingVoice(base.transform.name, this.duckLines, LevelVoice.Type.Start, null);
			this.voice = Audio.self.playVoice(notRepeatingVoice);
			this.voice.start(true);
		}
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x00029112 File Offset: 0x00027512
	public static bool checkIfDuck()
	{
		if (AwardController.self != null)
		{
			if (AwardController.self.isAllPuzzlesSolvedBad())
			{
				return Global.self.CountPackPlayedTimes(true, 0) <= 0;
			}
		}
		return false;
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00029150 File Offset: 0x00027550
	private void InOut()
	{
		if (!this.active)
		{
			return;
		}
		this.count++;
		if (this.count > 6 && !this.played)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.played = true;
			this.voice = Audio.self.playVoice(this.spamming);
			this.voice.start(true);
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000291DD File Offset: 0x000275DD
	public void chargerIn()
	{
		this.InOut();
		if (this.waiting != null)
		{
			base.StopCoroutine(this.waiting);
		}
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000291FC File Offset: 0x000275FC
	public void chargerOut()
	{
		this.InOut();
		this.waiting = null;
		if (!this.waitingPlayed)
		{
			this.waiting = base.StartCoroutine(this.playAfterDelay());
		}
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x00029228 File Offset: 0x00027628
	public override void subsctibeToEnding(endTextControl item)
	{
		this.waitingPlayed = true;
		this.played = true;
		if (this.waiting != null)
		{
			base.StopCoroutine(this.waiting);
		}
		if (!AudioVoice_PhoneCharge.checkIfDuck())
		{
			base.subsctibeToEnding(item);
		}
		else
		{
			if (this.voice != null)
			{
				this.voice.stop();
			}
			if (base.ps.solvedAsBad == true)
			{
				StandaloneLevelVoiceGuid notRepeatingVoice = AudioVoice.getNotRepeatingVoice(base.transform.name, this.duckLines, LevelVoice.Type.End, new bool?(true));
				this.voice = Audio.self.playVoice(notRepeatingVoice);
				this.voice.start(true);
				base.subscribeToMarkers(item, true);
			}
			else
			{
				StandaloneLevelVoiceGuid notRepeatingVoice2 = AudioVoice.getNotRepeatingVoice(base.transform.name, this.duckLines, LevelVoice.Type.End, new bool?(false));
				this.voice = Audio.self.playVoice(notRepeatingVoice2);
				this.voice.start(true);
				string endText = LevelVoice.getEndText(notRepeatingVoice2, null, false, Global.self.currLanguage);
				this.setEndText(item, endText, false);
			}
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x0002934C File Offset: 0x0002774C
	private IEnumerator playAfterDelay()
	{
		yield return new WaitForSeconds(5f);
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.count = 0;
		this.voice = Audio.self.playVoice(this.wait);
		this.voice.start(true);
		this.waitingPlayed = true;
		yield break;
	}

	// Token: 0x04000F9B RID: 3995
	[Space(10f)]
	public StandaloneLevelVoice spamming;

	// Token: 0x04000F9C RID: 3996
	public StandaloneLevelVoice wait;

	// Token: 0x04000F9D RID: 3997
	private int count;

	// Token: 0x04000F9E RID: 3998
	private bool played;

	// Token: 0x04000F9F RID: 3999
	private Coroutine waiting;

	// Token: 0x04000FA0 RID: 4000
	private bool waitingPlayed;

	// Token: 0x04000FA1 RID: 4001
	[Header("DUCK")]
	public StandaloneLevelVoice duckLines;

	// Token: 0x04000FA2 RID: 4002
	[BankRef]
	public string[] bankList;
}
