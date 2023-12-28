using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class timeLineControl : MonoBehaviour
{
	// Token: 0x060020A6 RID: 8358 RVA: 0x000A0724 File Offset: 0x0009EB24
	private void Update()
	{
		if (this.solved)
		{
			this.timePerPuzzle = Mathf.Lerp(this.timePerPuzzle, 0f, Time.deltaTime * 5f);
			RectTransform component = this.timeImg.GetComponent<RectTransform>();
			Vector2 anchoredPosition = component.anchoredPosition;
			anchoredPosition.y = Mathf.Lerp(anchoredPosition.y, 50f, Time.deltaTime * 5f);
			component.anchoredPosition = anchoredPosition;
			return;
		}
		if (this.scaleUp)
		{
			this.timeImg.localScale = Vector2.Lerp(this.timeImg.localScale, Vector2.one, Time.deltaTime * 4f);
			this.timeImg.localScale = Vector2.MoveTowards(this.timeImg.localScale, Vector2.one, Time.deltaTime);
			if (this.timeImg.localScale.x >= 1f)
			{
				this.timeImg.localScale = Vector2.one;
				this.scaleUp = false;
			}
		}
		else if (this.timePerPuzzle > 0f)
		{
			this.timePerPuzzle -= Time.deltaTime;
			if (this.timePerPuzzle <= 0f)
			{
				this.gotoNextPuzzle();
			}
			Vector2 v = this.timeImg.localScale;
			v.x = Mathf.Clamp(this.timePerPuzzle / this.timePerPuzzleMaxCurr, 0f, 1f);
			this.timeImg.localScale = v;
		}
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x000A08C8 File Offset: 0x0009ECC8
	private void gotoNextPuzzle()
	{
		if (this.solved)
		{
			return;
		}
		Global.self.currPuzzle.GetComponent<PuzzleStats>().TimeHasEnded();
		if (Global.self.currPuzzle.GetComponent<PuzzleStats>().doNotEndRapidFire)
		{
			base.StartCoroutine(this.WaitForPuzzleLock());
		}
		else
		{
			this.Finish();
		}
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000A0928 File Offset: 0x0009ED28
	private IEnumerator WaitForPuzzleLock()
	{
		while (Global.self.currPuzzle.GetComponent<PuzzleStats>().doNotEndRapidFire)
		{
			yield return null;
		}
		this.Finish();
		yield break;
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x000A0943 File Offset: 0x0009ED43
	private void Finish()
	{
		if (Global.self.currPuzzle.GetComponent<PuzzleStats>().goBadAfterTime)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x000A0979 File Offset: 0x0009ED79
	public void stopTime()
	{
		this.solved = true;
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000A0984 File Offset: 0x0009ED84
	public void resetLine()
	{
		this.scaleUp = true;
		this.solved = false;
		this.timeImg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		this.timeImg.localScale = Vector2.up;
		this.timePerPuzzle = 0f;
		if (base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.timeImg.localScale = Vector2.up;
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x000A0A06 File Offset: 0x0009EE06
	public void startTime(float time)
	{
		if (time == 0f)
		{
			this.timePerPuzzleMaxCurr = this.timePerPuzzleMax;
			this.timePerPuzzle = this.timePerPuzzleMax;
		}
		else
		{
			this.timePerPuzzleMaxCurr = time;
			this.timePerPuzzle = time;
		}
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x000A0A3E File Offset: 0x0009EE3E
	public void hideTimeLine()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x040023FB RID: 9211
	[Tooltip("Time line image to scale")]
	public Transform timeImg;

	// Token: 0x040023FC RID: 9212
	[Header("Setting")]
	[Tooltip("How long wait between puzzles")]
	public float timePerPuzzleMax = 5f;

	// Token: 0x040023FD RID: 9213
	private float timePerPuzzleMaxCurr;

	// Token: 0x040023FE RID: 9214
	private float timePerPuzzle;

	// Token: 0x040023FF RID: 9215
	private bool scaleUp;

	// Token: 0x04002400 RID: 9216
	private bool solved;
}
