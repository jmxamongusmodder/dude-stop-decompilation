using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class AudioVoice_CatBoxOrHouse : AudioVoiceStory
{
	// Token: 0x06001071 RID: 4209 RVA: 0x000173BC File Offset: 0x000157BC
	protected override IEnumerator mainCoroutine()
	{
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_lesson3, -1));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_Imagine, -1));
		yield return new WaitForSeconds(1f);
		if (this.ended == true)
		{
			yield return base.StartCoroutine(base.tryInterrupOverTime(0.1f));
		}
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_VeryGood, -1));
		if (!this.catOnSaid)
		{
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_WhatObjectsFor, -1));
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(base.tryInterrupOverTime(1f));
			if (!this.isVeryGoodSaid)
			{
				yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_SoundsGreat, -1));
			}
		}
		yield return base.StartCoroutine(base.tryInterrupOverTime(5f));
		if (!this.catOnSaid)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_UseCat, -1));
			yield return base.StartCoroutine(base.tryInterrupOverTime(5f));
		}
		if (!this.catOnSaid)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_UseTable, -1));
			yield return base.StartCoroutine(base.tryInterrupOverTime(5f));
		}
		while (!this.catOnSaid)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_ReadLast, -1));
		while (!this.canExitPuzzle || !this.canExit)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		yield break;
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x000173D8 File Offset: 0x000157D8
	protected override IEnumerator tryInterrupt()
	{
		this.interruptHappened = false;
		if (this.ended == true)
		{
			this.ended = new bool?(false);
			this.interruptHappened = true;
			yield return new WaitForSeconds(1f);
			if (this.endMonster)
			{
				yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_VeryGordrd, -1));
			}
			else
			{
				yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_VeryGood, -1));
			}
			this.canExitPuzzle = true;
		}
		else if (this.gotoNextLevel == true)
		{
			this.gotoNextLevel = new bool?(false);
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLineWithoutCheck(Voices.VoicePack06.CatBox_Next));
			this.canExit = true;
		}
		else if (this.catOnScreen == true)
		{
			this.catOnScreen = new bool?(false);
			this.catOnSaid = true;
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_DescribeCat, -1));
			yield return new WaitForSeconds(1f);
			yield return base.StartCoroutine(base.tryInterrupOverTime(2f));
			this.isVeryGoodSaid = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatBox_SoundsWanderf, -1));
		}
		yield break;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x000173F4 File Offset: 0x000157F4
	public void showCat()
	{
		bool? flag = this.catOnScreen;
		if (flag == null)
		{
			this.catOnScreen = new bool?(true);
		}
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x00017424 File Offset: 0x00015824
	public void end(bool monster)
	{
		this.endMonster = monster;
		bool? flag = this.ended;
		if (flag == null)
		{
			this.ended = new bool?(true);
		}
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0001745C File Offset: 0x0001585C
	public void gotoNext()
	{
		bool? flag = this.gotoNextLevel;
		if (flag == null)
		{
			this.gotoNextLevel = new bool?(true);
		}
	}

	// Token: 0x04000D7B RID: 3451
	private bool? catOnScreen;

	// Token: 0x04000D7C RID: 3452
	private bool catOnSaid;

	// Token: 0x04000D7D RID: 3453
	private bool? ended;

	// Token: 0x04000D7E RID: 3454
	private bool endMonster;

	// Token: 0x04000D7F RID: 3455
	private bool? gotoNextLevel;

	// Token: 0x04000D80 RID: 3456
	public bool canExit;

	// Token: 0x04000D81 RID: 3457
	private bool isVeryGoodSaid;
}
