using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class AudioVoice_KeysAndPhone : AudioVoiceScrollable
{
	// Token: 0x0600119B RID: 4507 RVA: 0x00021ADA File Offset: 0x0001FEDA
	public override void onTransitionIn()
	{
		if (!this.active)
		{
			return;
		}
		this.yesnoCount = -1;
		base.playVoice(this.onLoad, true, true, false, true);
		this.idleCor = base.StartCoroutine(this.idleCoroutine());
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00021B12 File Offset: 0x0001FF12
	public override bool onTransitionOut()
	{
		if (!this.active)
		{
			return true;
		}
		if (this.idleCor != null)
		{
			base.StopCoroutine(this.idleCor);
		}
		return true;
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00021B3C File Offset: 0x0001FF3C
	private IEnumerator idleCoroutine()
	{
		if (this.farted)
		{
			this.idleCor = null;
			yield break;
		}
		this.idling = false;
		while (!this.idling)
		{
			this.idling = true;
			yield return new WaitForSeconds(10f);
		}
		this.farted = base.playVoice(this.idle, false, true, false, true);
		yield break;
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x00021B58 File Offset: 0x0001FF58
	public void phoneWithKeys()
	{
		this.allIn = true;
		if (this.badSolved)
		{
			if (UnityEngine.Random.value < 0.1f && this.yesnoCount < 0)
			{
				this.yesnoCount = 0;
			}
			if (this.yesnoCount < 5 && this.yesnoCount >= 0)
			{
				this.idling = false;
				base.playVoice(this.no, false, false, true, true);
				this.yesnoCount++;
			}
			return;
		}
		this.idling = false;
		this.badSolved = base.playVoice(this.bad, true, false, true, true);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x00021BF8 File Offset: 0x0001FFF8
	public void takeOutAfterBad()
	{
		if (this.allIn && this.yesnoCount < 5 && this.yesnoCount >= 0)
		{
			this.idling = false;
			base.playVoice(this.yes, false, false, true, true);
			this.yesnoCount++;
		}
		this.allIn = false;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00021C55 File Offset: 0x00020055
	public void solveGood()
	{
		this.allIn = true;
		if (this.goodSolved)
		{
			return;
		}
		this.idling = false;
		this.goodSolved = base.playVoice(this.good, true, false, true, true);
	}

	// Token: 0x04000EA8 RID: 3752
	[Space(10f)]
	public StandaloneLevelVoice[] onLoad;

	// Token: 0x04000EA9 RID: 3753
	public StandaloneLevelVoice[] idle;

	// Token: 0x04000EAA RID: 3754
	public StandaloneLevelVoice[] bad;

	// Token: 0x04000EAB RID: 3755
	public StandaloneLevelVoice[] no;

	// Token: 0x04000EAC RID: 3756
	public StandaloneLevelVoice[] yes;

	// Token: 0x04000EAD RID: 3757
	public StandaloneLevelVoice[] good;

	// Token: 0x04000EAE RID: 3758
	private Coroutine idleCor;

	// Token: 0x04000EAF RID: 3759
	private bool idling;

	// Token: 0x04000EB0 RID: 3760
	private bool farted;

	// Token: 0x04000EB1 RID: 3761
	private bool badSolved;

	// Token: 0x04000EB2 RID: 3762
	private bool goodSolved;

	// Token: 0x04000EB3 RID: 3763
	private bool allIn;

	// Token: 0x04000EB4 RID: 3764
	private int yesnoCount = -1;
}
