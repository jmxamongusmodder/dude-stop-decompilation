using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class PuzzleHomework_Paper : DrawingCanvas
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06001AAE RID: 6830 RVA: 0x00069277 File Offset: 0x00067677
	public bool filled
	{
		get
		{
			return base.fill >= this.requiredFill;
		}
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x0006928C File Offset: 0x0006768C
	public override void Awake()
	{
		base.Awake();
		foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = "Front";
		}
		this.calendar = this.GetComponentInPuzzleStats<PuzzleHomework_Calendar>();
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x000692E0 File Offset: 0x000676E0
	public override int DrawPixel(Vector3 pixelPosition, Color color, bool apply = true)
	{
		if (this.finished)
		{
			return 0;
		}
		int result = base.DrawPixel(pixelPosition, color, true);
		if (this.filled)
		{
			this.Finished();
		}
		if (!this.drawStarted)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_HomeWork>().startDrawing();
			this.drawStarted = true;
		}
		return result;
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x0006933C File Offset: 0x0006773C
	protected virtual void Finished()
	{
		this.finished = true;
		if (this.calendar.friday)
		{
			this.onFriday = true;
		}
		base.StartCoroutine(this.MovingCoroutine());
		base.StartCoroutine(this.RotationCoroutine(null));
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x0006938C File Offset: 0x0006778C
	protected virtual IEnumerator MovingCoroutine()
	{
		base.enabled = false;
		Vector2 initialScale = base.transform.parent.localScale;
		Vector2 fullScale = Vector2.one * this.scale;
		this.startPosition = base.transform.parent.localPosition;
		yield return base.StartCoroutine(this.MovingCoroutine(this.scaleMove, this.scaleTime, initialScale, fullScale));
		Audio.self.playOneShot("671fd2b6-1591-4a96-aa2c-365a8bd036ce", 1f);
		yield return base.StartCoroutine(this.MovingCoroutine(this.middleMove, this.middleMoveTime, fullScale, fullScale));
		yield return base.StartCoroutine(this.MovingCoroutine(this.scaleMove, this.scaleTime, fullScale, initialScale));
		this.ChangeSprites();
		if (this.nextPaper != null)
		{
			this.nextPaper.gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x000693A8 File Offset: 0x000677A8
	private IEnumerator MovingCoroutine(float moveDelta, float moveTime, Vector2 startScale, Vector2 endScale)
	{
		Global.self.canBePaused = false;
		Vector3 startPosition = base.transform.parent.localPosition;
		Vector3 endPosition = startPosition + Vector3.right * moveDelta;
		float timer = 0f;
		while (timer != moveTime)
		{
			timer = Mathf.MoveTowards(timer, moveTime, Time.deltaTime);
			base.transform.parent.localScale = Vector3.Lerp(startScale, endScale, timer / moveTime);
			base.transform.parent.localPosition = Vector3.Lerp(startPosition, endPosition, timer / moveTime);
			yield return null;
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x000693E0 File Offset: 0x000677E0
	protected IEnumerator RotationCoroutine(float? angle = null)
	{
		float timer = 0f;
		float totalTime = 2f * this.scaleTime + this.middleMoveTime;
		float startAngle = base.transform.parent.eulerAngles.z;
		float endAngle = (angle != null) ? angle.Value : UnityEngine.Random.Range(-this.randomAngle, this.randomAngle);
		while (timer != totalTime)
		{
			timer = Mathf.MoveTowards(timer, totalTime, Time.deltaTime);
			float t = Mathf.LerpAngle(startAngle, endAngle, timer / totalTime);
			base.transform.parent.rotation = Quaternion.Euler(0f, 0f, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00069404 File Offset: 0x00067804
	public virtual void ChangeSprites()
	{
		foreach (SpriteRenderer spriteRenderer in base.transform.parent.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = this.layerAfterMove;
		}
	}

	// Token: 0x040018CA RID: 6346
	public Transform nextPaper;

	// Token: 0x040018CB RID: 6347
	[Range(0f, 1f)]
	public float requiredFill;

	// Token: 0x040018CC RID: 6348
	[Header("Moving stuff")]
	public float scale;

	// Token: 0x040018CD RID: 6349
	public float scaleTime;

	// Token: 0x040018CE RID: 6350
	public float scaleMove;

	// Token: 0x040018CF RID: 6351
	public float middleMove;

	// Token: 0x040018D0 RID: 6352
	public float middleMoveTime;

	// Token: 0x040018D1 RID: 6353
	public float randomAngle;

	// Token: 0x040018D2 RID: 6354
	public string layerAfterMove;

	// Token: 0x040018D3 RID: 6355
	[HideInInspector]
	public bool onFriday;

	// Token: 0x040018D4 RID: 6356
	[HideInInspector]
	public Vector2 startPosition;

	// Token: 0x040018D5 RID: 6357
	protected bool finished;

	// Token: 0x040018D6 RID: 6358
	protected PuzzleHomework_Calendar calendar;

	// Token: 0x040018D7 RID: 6359
	private bool drawStarted;
}
