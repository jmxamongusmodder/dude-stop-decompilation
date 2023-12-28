using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class AudioVoice_CatBreakGlass : AudioVoiceStory
{
	// Token: 0x06001077 RID: 4215 RVA: 0x00017D10 File Offset: 0x00016110
	private IEnumerator countTimers()
	{
		for (;;)
		{
			if (this.jump > 0f)
			{
				this.jump -= Time.deltaTime;
			}
			if (this.hitWater > 0f)
			{
				this.hitWater -= Time.deltaTime;
			}
			if (this.breakGlass > 0f)
			{
				this.breakGlass -= Time.deltaTime;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00017D2C File Offset: 0x0001612C
	protected override IEnumerator mainCoroutine()
	{
		base.StartCoroutine(this.countTimers());
		int param = (SerializablePuzzleStats.Get(base.transform.name).loadedTimes <= 0) ? -1 : 1;
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_lesson3, param));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_ThisIsCat, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(0.5f));
		if (this.jump == -1f)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_WhereIsCat, -1));
		}
		yield return base.StartCoroutine(base.tryInterrupOverTime(0.5f));
		yield return base.StartCoroutine(this.tryInterrupt());
		base.ps.GetComponentsInChildren<PuzzleCatBreaksGlass_Cat>(true)[0].ShowArrow();
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_WantsToLeave, -1));
		this.repeatLine = Voices.VoicePack06.CatGlass_WantsToLeave;
		while (!this.canExitPuzzle)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
			yield return base.StartCoroutine(base.loopLine(this.repeatLine));
		}
		yield break;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00017D48 File Offset: 0x00016148
	protected override IEnumerator tryInterrupt()
	{
		this.interruptHappened = false;
		if (this.breakGlass > 0f)
		{
			this.breakGlass = 0f;
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_WhatElse, -1));
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_Correct, -1));
			this.repeatLine = Voices.VoicePack06.CatGlass_WhatElseRepeat;
		}
		else if (this.hitWater > 0f)
		{
			this.saidWater = true;
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_DontLikeWater, -1));
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_MakesWet, -1));
			if (UnityEngine.Random.value > 0.5f)
			{
				this.repeatLine = Voices.VoicePack06.CatGlass_DontLikeWater;
			}
			else
			{
				this.repeatLine = Voices.VoicePack06.CatGlass_MakesWet;
			}
		}
		else if (this.jump > 0f)
		{
			this.jump = 0f;
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_LikeJump, -1));
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_OnTheirFeet, -1));
			this.repeatLine = Voices.VoicePack06.CatGlass_OnTheirFeet;
		}
		else
		{
			if (!(this.exit == true))
			{
				yield break;
			}
			this.exit = new bool?(false);
			this.interruptHappened = true;
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatGlass_NextPicture, -1));
			this.canExitPuzzle = true;
		}
		yield break;
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00017D63 File Offset: 0x00016163
	public void catJump()
	{
		if (this.jump == -1f)
		{
			this.jump = 2f;
		}
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00017D80 File Offset: 0x00016180
	public void catHitWater()
	{
		if (!this.saidWater)
		{
			this.hitWater = 2f;
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x00017D98 File Offset: 0x00016198
	public void catBreakGlass()
	{
		if (this.breakGlass == -1f)
		{
			this.breakGlass = 5f;
		}
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00017DB8 File Offset: 0x000161B8
	public void catExit()
	{
		bool? flag = this.exit;
		if (flag == null)
		{
			this.exit = new bool?(true);
		}
	}

	// Token: 0x04000D82 RID: 3458
	private float jump = -1f;

	// Token: 0x04000D83 RID: 3459
	private float hitWater = -1f;

	// Token: 0x04000D84 RID: 3460
	private bool saidWater;

	// Token: 0x04000D85 RID: 3461
	private float breakGlass = -1f;

	// Token: 0x04000D86 RID: 3462
	private bool? exit;
}
