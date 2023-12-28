using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000404 RID: 1028
public class PuzzleFoodPhoto_Phone : EnhancedDraggable
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x00064754 File Offset: 0x00062B54
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.movingCoroutine != null)
		{
			base.StopCoroutine(this.movingCoroutine);
		}
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x00064774 File Offset: 0x00062B74
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.transform.position.y > this.minimalPosition)
		{
			base.StartCoroutine(this.PositioningCoroutine());
		}
		else
		{
			this.movingCoroutine = base.StartCoroutine(this.PositioningCoroutine(this.startingPosition));
		}
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x000647D4 File Offset: 0x00062BD4
	public void Move(float time)
	{
		base.StartCoroutine(this.MovingCoroutine(time));
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000647E4 File Offset: 0x00062BE4
	private IEnumerator MovingCoroutine(float time)
	{
		float deltaX = UnityEngine.Random.Range(-this.deltaPosition.x, this.deltaPosition.x);
		float deltaY = UnityEngine.Random.Range(-this.deltaPosition.y, this.deltaPosition.y);
		Vector2 end = this.centerPosition + new Vector2(deltaX, deltaY);
		while (base.transform.localPosition != end)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, end, Time.deltaTime * this.lerpSpeed);
			base.transform.position = Vector2.MoveTowards(base.transform.position, end, Time.deltaTime * this.moveSpeed);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x00064800 File Offset: 0x00062C00
	private IEnumerator PositioningCoroutine()
	{
		UnityEngine.Object.Destroy(base.GetComponent<PolygonCollider2D>());
		base.enabled = false;
		yield return base.StartCoroutine(this.PositioningCoroutine(new Vector2(base.transform.position.x, this.beautifulPosition)));
		this.GetPuzzleStats().subtitlesYShift = 0f;
		UIControl.positionSubtitles(null);
		float fadeTimer = 0f;
		SpriteRenderer rend = this.blackScreen.GetComponent<SpriteRenderer>();
		Color color = rend.color;
		Color newColor = color;
		newColor.a = 0f;
		while (fadeTimer != this.screenFadeTime)
		{
			fadeTimer = Mathf.MoveTowards(fadeTimer, this.screenFadeTime, Time.deltaTime);
			float t = Mathf.Sin(fadeTimer / this.screenFadeTime * 3.1415927f * 0.5f);
			this.blackScreen.GetComponent<SpriteRenderer>().color = Color.Lerp(color, newColor, t);
			yield return null;
		}
		this.centerPosition = base.transform.localPosition;
		yield break;
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x0006481C File Offset: 0x00062C1C
	private IEnumerator PositioningCoroutine(Vector2 end)
	{
		while (base.transform.position.y != end.y)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, end, Time.deltaTime * this.lerpSpeed);
			base.transform.position = Vector2.MoveTowards(base.transform.position, end, Time.deltaTime * this.moveSpeed);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400181C RID: 6172
	[Header("Fading")]
	public Transform blackScreen;

	// Token: 0x0400181D RID: 6173
	public float screenFadeTime;

	// Token: 0x0400181E RID: 6174
	[Header("Positioning")]
	public float beautifulPosition;

	// Token: 0x0400181F RID: 6175
	public float minimalPosition;

	// Token: 0x04001820 RID: 6176
	public float moveSpeed;

	// Token: 0x04001821 RID: 6177
	public float lerpSpeed;

	// Token: 0x04001822 RID: 6178
	public Vector2 deltaPosition;

	// Token: 0x04001823 RID: 6179
	private Vector2 centerPosition;

	// Token: 0x04001824 RID: 6180
	private Coroutine movingCoroutine;
}
