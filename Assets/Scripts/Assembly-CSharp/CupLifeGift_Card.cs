using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class CupLifeGift_Card : MonoBehaviour
{
	// Token: 0x060000B4 RID: 180 RVA: 0x00008826 File Offset: 0x00006A26
	private void OnDrawGizmosSelected()
	{
		GizmosExtension.DrawPoint(this.upscaledPosition, Color.blue, 0.5f);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00008840 File Offset: 0x00006A40
	private void OnMouseDown()
	{
		if (!this.canBeClicked)
		{
			return;
		}
		Audio.self.playOneShot("a6e8ef07-8c57-4c2c-9ec3-27199d6f5506", 1f);
		if (this.firstClick)
		{
			base.StartCoroutine(this.UpscalingCoroutine());
		}
		else
		{
			base.GetComponent<Rigidbody2D>().isKinematic = false;
			this.canBeClicked = false;
			UnityEngine.Object.Destroy(base.gameObject, 5f);
			this.EnableLid();
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x000088B4 File Offset: 0x00006AB4
	private void OnMouseEnter()
	{
		if (!this.firstClick)
		{
			return;
		}
		base.transform.localScale = Vector2.one * this.hoverScale;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000088E2 File Offset: 0x00006AE2
	private void OnMouseExit()
	{
		if (!this.firstClick)
		{
			return;
		}
		base.transform.localScale = Vector2.one;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00008908 File Offset: 0x00006B08
	private IEnumerator UpscalingCoroutine()
	{
		this.canBeClicked = false;
		this.firstClick = false;
		CupLifeGift_Lid lid = this.GetComponentInPuzzleStats<CupLifeGift_Lid>();
		lid.GetComponent<BoxCollider2D>().enabled = false;
		lid.GetComponent<PolygonCollider2D>().enabled = false;
		lid.GetComponent<Rigidbody2D>().isKinematic = true;
		SpriteRenderer sprite = base.GetComponent<SpriteRenderer>();
		Canvas canvas = base.transform.GetChild(0).GetComponent<Canvas>();
		canvas.gameObject.SetActive(true);
		Vector2 start = base.transform.localPosition;
		Vector2 end = base.transform.parent.InverseTransformPoint(this.upscaledPosition);
		float curveTime = this.upscaleCurve.GetAnimationLength();
		float timer = 0f;
		while (timer != curveTime)
		{
			timer = Mathf.MoveTowards(timer, curveTime, Time.deltaTime);
			float t = this.upscaleCurve.Evaluate(timer);
			sprite.color = Extensions.Color.SetAlpha(sprite.color, 1f - t);
			base.transform.localPosition = Vector2.Lerp(start, end, t);
			base.transform.localScale = Vector2.one * Mathf.Lerp(1f, this.spriteCardScale, t);
			yield return null;
		}
		this.canBeClicked = true;
		yield break;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00008924 File Offset: 0x00006B24
	private void EnableLid()
	{
		CupLifeGift_Lid componentInPuzzleStats = this.GetComponentInPuzzleStats<CupLifeGift_Lid>();
		componentInPuzzleStats.dragEnabled = true;
		componentInPuzzleStats.GetComponent<PolygonCollider2D>().enabled = true;
		componentInPuzzleStats.GetComponent<BoxCollider2D>().enabled = true;
		componentInPuzzleStats.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	// Token: 0x04000134 RID: 308
	public Vector2 upscaledPosition;

	// Token: 0x04000135 RID: 309
	public float spriteCardScale = 5.7f;

	// Token: 0x04000136 RID: 310
	public float hoverScale = 1.1f;

	// Token: 0x04000137 RID: 311
	public AnimationCurve upscaleCurve;

	// Token: 0x04000138 RID: 312
	private bool firstClick = true;

	// Token: 0x04000139 RID: 313
	private bool canBeClicked = true;
}
