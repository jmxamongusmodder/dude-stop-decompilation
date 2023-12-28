using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public class AudioVoice_DogEatsShoe : AudioVoiceStory
{
	// Token: 0x0600114D RID: 4429 RVA: 0x0001F308 File Offset: 0x0001D708
	protected override IEnumerator mainCoroutine()
	{
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_lesson3, -1));
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_LetsLook, -1));
		if (this.notalllowed != true)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_ThisIsDog, -1));
		}
		yield return base.StartCoroutine(base.tryInterrupOverTime(0.5f));
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_DogInKitchen, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(0.5f));
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_WhatColor, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(2f));
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_WhyExist, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(2f));
		if (!this.notAllowedSaid)
		{
			base.ps.GetComponentsInChildren<PuzzleDogEatsShoes_Dog>(true)[0].showArrow();
			yield return base.StartCoroutine(this.sayNotAllowed());
		}
		while (!this.canExitPuzzle)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		yield break;
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0001F324 File Offset: 0x0001D724
	protected override IEnumerator tryInterrupt()
	{
		this.interruptHappened = false;
		if (this.shoe == true)
		{
			this.shoe = new bool?(false);
			this.interruptHappened = true;
			yield return new WaitForSeconds(0.5f);
			if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes < 2)
			{
				yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_BadDoggy, -1));
			}
			else
			{
				yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_BadDoggyShort, -1));
			}
			this.canExitPuzzle = true;
		}
		else if (this.notalllowed == true)
		{
			this.notalllowed = new bool?(false);
			this.interruptHappened = true;
			if (SerializablePuzzleStats.Get(base.transform.name).loadedTimes < 1)
			{
				yield return base.StartCoroutine(this.sayNotAllowed());
			}
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_ByeByeDoggy, -1));
			this.canExitPuzzle = true;
		}
		yield break;
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0001F340 File Offset: 0x0001D740
	private IEnumerator sayNotAllowed()
	{
		if (this.notAllowedSaid)
		{
			yield break;
		}
		this.notAllowedSaid = true;
		if (Global.getCompletionState(this.firstCatPuzzle) == CompletionState.Monster)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_NotAllowed, -1));
		}
		else
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.DogShoes_NotAllowedRepeat, -1));
		}
		yield break;
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0001F35C File Offset: 0x0001D75C
	public void eatShoe()
	{
		bool? flag = this.shoe;
		if (flag == null)
		{
			this.shoe = new bool?(true);
		}
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0001F38C File Offset: 0x0001D78C
	public void hitArrow()
	{
		bool? flag = this.notalllowed;
		if (flag == null)
		{
			this.notalllowed = new bool?(true);
		}
	}

	// Token: 0x04000E52 RID: 3666
	public Transform firstCatPuzzle;

	// Token: 0x04000E53 RID: 3667
	private bool? shoe;

	// Token: 0x04000E54 RID: 3668
	private bool? notalllowed;

	// Token: 0x04000E55 RID: 3669
	private bool notAllowedSaid;
}
