using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class AudioVoice_CatOnBlackDress : AudioVoiceStory
{
	// Token: 0x06001086 RID: 4230 RVA: 0x00018AE4 File Offset: 0x00016EE4
	private IEnumerator countTimers()
	{
		for (;;)
		{
			if (this.hair > 0f)
			{
				this.hair -= Time.deltaTime;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00018B00 File Offset: 0x00016F00
	protected override IEnumerator mainCoroutine()
	{
		base.StartCoroutine(this.countTimers());
		int loaded = SerializablePuzzleStats.Get(base.transform.name).loadedTimes;
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_lesson3, -1));
		if (loaded > 1)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		if (!this.saidHair)
		{
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_ExaminePic, -1));
		}
		if (loaded > 1)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
			yield return base.StartCoroutine(this.tryInterrupt());
		}
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_WhereIsCat, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(0.5f));
		yield return base.StartCoroutine(this.tryInterrupt());
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_LikeToSleep, -1));
		yield return base.StartCoroutine(base.tryInterrupOverTime(5f));
		yield return base.StartCoroutine(this.tryInterrupt());
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_AfterSleep, -1));
		yield return new WaitForSeconds(0.5f);
		base.ps.GetComponentsInChildren<PuzzleCatOnBlackDress>(true)[0].showArrow();
		yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_GoForWalk, -1));
		this.repeatLine = Voices.VoicePack06.CatDress_GoForWalkRepeat;
		while (!this.canExitPuzzle || !this.canExit)
		{
			yield return base.StartCoroutine(this.tryInterrupt());
			yield return base.StartCoroutine(base.loopLine(this.repeatLine));
		}
		yield break;
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00018B1C File Offset: 0x00016F1C
	protected override IEnumerator tryInterrupt()
	{
		this.interruptHappened = false;
		if (this.sleep == true)
		{
			this.sleep = new bool?(false);
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_LindaAndMery, -1));
			yield return new WaitForSeconds(0.5f);
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_Next, -1));
			this.canExitPuzzle = true;
		}
		else if (this.wall == true)
		{
			this.wall = new bool?(false);
			this.interruptHappened = true;
			this.canClimb = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_CatsWeird, -1));
			this.canExitPuzzle = true;
		}
		else if (this.endClimb == true)
		{
			this.endClimb = new bool?(false);
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLineWithoutCheck(Voices.VoicePack06.CatDress_Next));
			this.canExit = true;
		}
		else if (this.hair > 0f)
		{
			this.saidHair = true;
			this.interruptHappened = true;
			yield return base.StartCoroutine(base.playLine(Voices.VoicePack06.CatDress_LetsCount, -1));
		}
		yield break;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00018B37 File Offset: 0x00016F37
	public void dropHair()
	{
		if (!this.saidHair)
		{
			this.hair = 2f;
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00018B50 File Offset: 0x00016F50
	public void sleepOnDress()
	{
		bool? flag = this.sleep;
		if (flag == null)
		{
			this.sleep = new bool?(true);
		}
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00018B80 File Offset: 0x00016F80
	public void wallClimb()
	{
		bool? flag = this.wall;
		if (flag == null)
		{
			this.wall = new bool?(true);
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00018BB0 File Offset: 0x00016FB0
	public void EndClimb()
	{
		bool? flag = this.endClimb;
		if (flag == null)
		{
			this.endClimb = new bool?(true);
		}
	}

	// Token: 0x04000D89 RID: 3465
	private float hair = -1f;

	// Token: 0x04000D8A RID: 3466
	private bool saidHair;

	// Token: 0x04000D8B RID: 3467
	private bool? sleep;

	// Token: 0x04000D8C RID: 3468
	private bool? wall;

	// Token: 0x04000D8D RID: 3469
	private bool? endClimb;

	// Token: 0x04000D8E RID: 3470
	public bool canClimb;

	// Token: 0x04000D8F RID: 3471
	public bool canExit;
}
