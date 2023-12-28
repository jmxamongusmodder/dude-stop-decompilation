using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class AudioVoice_ParkOnLine : AudioVoiceSpecialEnding
{
	// Token: 0x06001294 RID: 4756 RVA: 0x00028EAE File Offset: 0x000272AE
	protected override void setActiveDefault()
	{
		this.timeFromStart = Time.time;
		base.setActiveDefault();
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x00028EC4 File Offset: 0x000272C4
	public void startMoving()
	{
		bool? flag = this.kittenPlayed;
		if (flag == null && UnityEngine.Random.value > 0.9f)
		{
			this.kittenPlayed = new bool?(true);
			base.StartCoroutine(this.playKitten());
		}
		else
		{
			this.kittenPlayed = new bool?(false);
		}
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x00028F20 File Offset: 0x00027320
	private IEnumerator playKitten()
	{
		yield return new WaitForSeconds(0.2f);
		if (!this.canPlayKitten)
		{
			yield break;
		}
		base.playVoice(this.onCarMoveLine, true, true);
		yield break;
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x00028F3C File Offset: 0x0002733C
	public void collideWithLine()
	{
		if (this.timeFromStart == -1f)
		{
			return;
		}
		this.collCount++;
		if (this.collCount > 5 && Time.time - this.timeFromStart > 10f)
		{
			base.playVoice(this.waitLine, true, true);
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x00028F99 File Offset: 0x00027399
	public override void subsctibeToEnding(endTextControl item)
	{
		this.canPlayKitten = false;
		this.timeFromStart = -1f;
		if (base.playEndOnBadProgress(item))
		{
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04000F95 RID: 3989
	[Space(10f)]
	public StandaloneLevelVoice onCarMoveLine;

	// Token: 0x04000F96 RID: 3990
	public StandaloneLevelVoice waitLine;

	// Token: 0x04000F97 RID: 3991
	private bool? kittenPlayed;

	// Token: 0x04000F98 RID: 3992
	private bool canPlayKitten = true;

	// Token: 0x04000F99 RID: 3993
	private float timeFromStart;

	// Token: 0x04000F9A RID: 3994
	private int collCount;
}
