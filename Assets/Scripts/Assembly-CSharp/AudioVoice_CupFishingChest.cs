using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class AudioVoice_CupFishingChest : AudioVoice
{
	// Token: 0x060010EC RID: 4332 RVA: 0x0001C794 File Offset: 0x0001AB94
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.onLoadLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0001C7F0 File Offset: 0x0001ABF0
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "give"))
			{
				if (markerName == "teach")
				{
					this.canShowRod = true;
				}
			}
			else
			{
				this.canDropCoins = true;
			}
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0001C848 File Offset: 0x0001AC48
	public void trashVisible()
	{
		this.playVoice(this.showTrashLine);
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0001C857 File Offset: 0x0001AC57
	public bool middleReached()
	{
		return this.playVoice(this.onMidLine);
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0001C865 File Offset: 0x0001AC65
	public void cupReached()
	{
		this.playVoice(this.showCupLine);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0001C874 File Offset: 0x0001AC74
	public void pullOutCup()
	{
		base.StartCoroutine(this.waitForVoice());
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0001C884 File Offset: 0x0001AC84
	private IEnumerator waitForVoice()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.2f);
		this.voice = Audio.self.playVoice(this.pullingLine);
		Global.self.canExitEndScreen = false;
		this.voice.subscribeToStopped(this, delegate(VoiceLine l)
		{
			Global.self.canExitEndScreen = true;
		});
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x0001C8A0 File Offset: 0x0001ACA0
	public void hitObstacle()
	{
		if (this.wrongInd > this.wrongPullLine.Length - 1)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.wrongPullLine[this.wrongInd++]);
		this.voice.start(true);
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0001C91D File Offset: 0x0001AD1D
	public void readyToClick()
	{
		this.canPlayWait = true;
		base.StartCoroutine(this.waiting());
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0001C933 File Offset: 0x0001AD33
	public void mouseDown()
	{
		this.canPlayWait = false;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0001C93C File Offset: 0x0001AD3C
	private IEnumerator waiting()
	{
		yield return new WaitForSeconds(3f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!this.canPlayWait)
		{
			yield break;
		}
		this.playVoice(this.waitLine);
		yield break;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0001C958 File Offset: 0x0001AD58
	private bool playVoice(StandaloneLevelVoice line)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			return false;
		}
		if (this.played.Contains(line.levelVoiceId))
		{
			return false;
		}
		this.played += line.levelVoiceId;
		this.voice = Audio.self.playVoice(line);
		this.voice.start(true);
		return true;
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0001C9CF File Offset: 0x0001ADCF
	public override void subsctibeToEnding(endTextControl item)
	{
		item.SetEnding(LevelVoice.getEndText(this.endText, Global.self.currLanguage), false);
	}

	// Token: 0x04000DE9 RID: 3561
	[Space(10f)]
	public StandaloneLevelVoice onLoadLine;

	// Token: 0x04000DEA RID: 3562
	public StandaloneLevelVoice showTrashLine;

	// Token: 0x04000DEB RID: 3563
	public StandaloneLevelVoice onMidLine;

	// Token: 0x04000DEC RID: 3564
	public StandaloneLevelVoice showCupLine;

	// Token: 0x04000DED RID: 3565
	public StandaloneLevelVoice pullingLine;

	// Token: 0x04000DEE RID: 3566
	public StandaloneLevelVoice[] wrongPullLine;

	// Token: 0x04000DEF RID: 3567
	public StandaloneLevelVoice waitLine;

	// Token: 0x04000DF0 RID: 3568
	public StandaloneLevelVoice endText;

	// Token: 0x04000DF1 RID: 3569
	private string played = string.Empty;

	// Token: 0x04000DF2 RID: 3570
	private bool canPlayWait = true;

	// Token: 0x04000DF3 RID: 3571
	private int wrongInd;

	// Token: 0x04000DF4 RID: 3572
	[HideInInspector]
	public bool canDropCoins;

	// Token: 0x04000DF5 RID: 3573
	[HideInInspector]
	public bool canShowRod;
}
