using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class AudioVoice_StuckBalloon : AudioVoiceSpecialEnding
{
	// Token: 0x06001306 RID: 4870 RVA: 0x0002DEA2 File Offset: 0x0002C2A2
	protected override void setActiveDefault()
	{
		this.startTime = Time.time;
		base.setActiveDefault();
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0002DEB5 File Offset: 0x0002C2B5
	public void onSnap()
	{
		base.playVoice(this.onSnapLine, true, true);
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0002DEC6 File Offset: 0x0002C2C6
	public void sayCatchItLine()
	{
		if (this.catchCoroutine != null)
		{
			base.StopCoroutine(this.catchCoroutine);
		}
		this.catchCoroutine = base.StartCoroutine(this.sayCatch());
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0002DEF1 File Offset: 0x0002C2F1
	public void cancelCatchLine()
	{
		if (this.catchCoroutine != null)
		{
			base.StopCoroutine(this.catchCoroutine);
		}
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0002DF0C File Offset: 0x0002C30C
	private IEnumerator sayCatch()
	{
		yield return new WaitForSeconds(2f);
		base.playVoice(this.catchLine, true, true);
		this.catchCoroutine = null;
		yield break;
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0002DF28 File Offset: 0x0002C328
	public override void subsctibeToEnding(endTextControl item)
	{
		if (base.ps.solvedAsBad == true && base.playEndOnBadProgress(item))
		{
			return;
		}
		if (base.ps.solvedAsBad == true && Time.time - this.startTime < 5f && UnityEngine.Random.value > 0.8f && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.fastEndLine.levelVoiceId))
		{
			base.playSpecificEnd(this.fastEndLine, item);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x04001005 RID: 4101
	[Space(10f)]
	public StandaloneLevelVoice onSnapLine;

	// Token: 0x04001006 RID: 4102
	public StandaloneLevelVoice catchLine;

	// Token: 0x04001007 RID: 4103
	public StandaloneLevelVoice fastEndLine;

	// Token: 0x04001008 RID: 4104
	private float startTime;

	// Token: 0x04001009 RID: 4105
	private Coroutine catchCoroutine;
}
