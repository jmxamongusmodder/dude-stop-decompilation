using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class PuzzleHomework_LastPaper : PuzzleHomework_Paper
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000699C8 File Offset: 0x00067DC8
	private bool monster
	{
		get
		{
			return (from x in this.GetComponentsInPuzzleStats(true)
			where x.onFriday
			select x).Count<PuzzleHomework_Paper>() >= 1;
		}
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000699FE File Offset: 0x00067DFE
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.targetPoint, Color.cyan, 0.5f);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x00069A18 File Offset: 0x00067E18
	protected override void Finished()
	{
		if (this.finished || this.finishProcessed)
		{
			return;
		}
		this.finished = true;
		this.finishProcessed = true;
		if (this.calendar.friday)
		{
			this.onFriday = true;
		}
		if (this.monster)
		{
			this.finished = false;
			base.StartCoroutine(this.MonsterCoroutine());
		}
		else
		{
			base.StartCoroutine(this.MovingCoroutine());
			base.StartCoroutine(base.RotationCoroutine(null));
		}
		PuzzleHomework_Pen componentInPuzzleStats = this.GetComponentInPuzzleStats<PuzzleHomework_Pen>();
		if (componentInPuzzleStats != null)
		{
			componentInPuzzleStats.returnToInventory = true;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_HomeWork>().finishPuzzle(this.monster);
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00069ADC File Offset: 0x00067EDC
	protected override IEnumerator MovingCoroutine()
	{
		this.calendar.active = false;
		Transform puzzle = this.GetPuzzleStats().transform;
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(this.waitBeforeScrolling);
		yield return base.StartCoroutine(base.MovingCoroutine());
		Global.self.canBePaused = false;
		yield return base.StartCoroutine(this.calendar.MoveToFriday());
		this.MoveAllPapersAway();
		this.onNextTransition = true;
		yield return new WaitForSeconds(this.timeBetweenPapers * 3f + this.otherPaperCurve.GetAnimationLength() + 0.3f);
		Global.setCompletionState(CompletionState.Good, puzzle);
		yield return new WaitForSeconds(this.waitBeforePausing);
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x00069AF8 File Offset: 0x00067EF8
	private IEnumerator MonsterCoroutine()
	{
		Transform puzzle = this.GetPuzzleStats().transform;
		Global.self.canBePaused = false;
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(this.waitBeforeScrolling);
		this.startPosition = base.transform.parent.localPosition;
		base.StartCoroutine(this.OtherPaperMovingCoroutine(base.transform.parent, 0f));
		yield return new WaitForSeconds(0.5f);
		int i = 0;
		foreach (PuzzleHomework_Paper puzzleHomework_Paper in (from x in this.GetComponentsInPuzzleStats(false)
		where x != this
		select x).Reverse<PuzzleHomework_Paper>())
		{
			Transform parent = puzzleHomework_Paper.transform.parent;
			int num;
			i = (num = i) + 1;
			base.StartCoroutine(this.OtherPaperMovingCoroutine(parent, (float)num * this.timeBetweenPapers));
		}
		yield return new WaitForSeconds(this.timeBetweenPapers * 2f + this.otherPaperCurve.GetAnimationLength());
		this.onNextTransition = true;
		Global.setCompletionState(CompletionState.Monster, puzzle);
		yield return new WaitForSeconds(this.waitBeforePausing);
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00069B14 File Offset: 0x00067F14
	private void MoveAllPapersAway()
	{
		int num = 0;
		foreach (PuzzleHomework_Paper puzzleHomework_Paper in this.GetComponentsInPuzzleStats(false).Reverse<PuzzleHomework_Paper>())
		{
			base.StartCoroutine(this.OtherPaperMovingCoroutine(puzzleHomework_Paper.transform.parent, (float)num++ * this.timeBetweenPapers));
		}
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x00069B94 File Offset: 0x00067F94
	private IEnumerator OtherPaperMovingCoroutine(Transform paper, float wait)
	{
		Vector2 start = paper.position;
		yield return new WaitForSeconds(wait);
		Audio.self.playOneShot("9e4658f0-a714-44b8-964f-7b4dc1553791", 1f);
		for (float timer = 0f; timer < this.otherPaperCurve.GetAnimationLength(); timer += Time.deltaTime)
		{
			paper.position = Vector2.Lerp(start, this.targetPoint, this.otherPaperCurve.Evaluate(timer));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00069BC0 File Offset: 0x00067FC0
	private void PrepareForTransition()
	{
		foreach (PuzzleHomework_Paper puzzleHomework_Paper in this.GetComponentsInPuzzleStats(true))
		{
			puzzleHomework_Paper.transform.parent.localPosition = puzzleHomework_Paper.startPosition;
		}
		this.calendar.MoveToMonday();
		this.finished = true;
		this.layerAfterMove = "Background";
		this.ChangeSprites();
		PuzzleHomework_FirstPaper componentInPuzzleStats = this.GetComponentInPuzzleStats(true);
		componentInPuzzleStats.layerAfterMove = "Front";
		componentInPuzzleStats.ChangeSprites();
		componentInPuzzleStats.mark.SetActive(true);
		if (Global.getCompletionState(this.GetPuzzleStats().transform) == CompletionState.Monster)
		{
			componentInPuzzleStats.mark.transform.GetChild(0).gameObject.SetActive(true);
		}
		else
		{
			componentInPuzzleStats.mark.transform.GetChild(1).gameObject.SetActive(true);
		}
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x00069CA3 File Offset: 0x000680A3
	public void TransitionStarted()
	{
		if (!this.onNextTransition)
		{
			return;
		}
		this.PrepareForTransition();
		this.onNextTransition = false;
	}

	// Token: 0x040018C2 RID: 6338
	[Header("Moving the papers")]
	public Vector2 targetPoint;

	// Token: 0x040018C3 RID: 6339
	public float timeBetweenPapers;

	// Token: 0x040018C4 RID: 6340
	public AnimationCurve otherPaperCurve;

	// Token: 0x040018C5 RID: 6341
	[Header("End-level waits")]
	public float waitBeforeScrolling = 4f;

	// Token: 0x040018C6 RID: 6342
	public float waitBeforePausing = 1f;

	// Token: 0x040018C7 RID: 6343
	private bool finishProcessed;

	// Token: 0x040018C8 RID: 6344
	private bool onNextTransition;
}
