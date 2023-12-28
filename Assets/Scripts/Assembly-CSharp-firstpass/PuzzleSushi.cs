using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class PuzzleSushi : MonoBehaviour
{
	// Token: 0x06001C96 RID: 7318 RVA: 0x00079FE8 File Offset: 0x000783E8
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.transform.localScale = Vector3.one * this.originalScale;
		this.otherUtensil.localScale = Vector3.one * this.originalScale;
		this.mouseDown = true;
		Audio.self.playOneShot("9a2807b3-2f3b-4cb9-bf96-74d6f045b2a2", 1f);
	}

	// Token: 0x06001C97 RID: 7319 RVA: 0x0007A053 File Offset: 0x00078453
	private void OnMouseUp()
	{
		if (!base.enabled || !this.mouseDown || !this.mouseIsOver)
		{
			return;
		}
		this.FinishingCoroutine();
	}

	// Token: 0x06001C98 RID: 7320 RVA: 0x0007A080 File Offset: 0x00078480
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		this.mouseIsOver = true;
		this.otherUtensil.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
		base.transform.localScale = Vector3.one * this.hoverScale;
		this.otherUtensil.localScale = Vector3.one * this.hoverScale;
		Audio.self.playOneShot("bad7f403-41d4-4674-9ce1-b5ee949363ed", 1f);
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x0007A128 File Offset: 0x00078528
	private void OnMouseExit()
	{
		this.mouseIsOver = false;
		this.otherUtensil.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		if (!base.enabled)
		{
			return;
		}
		base.transform.localScale = Vector3.one * this.originalScale;
		this.otherUtensil.localScale = Vector3.one * this.originalScale;
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x0007A1B8 File Offset: 0x000785B8
	private void FinishingCoroutine()
	{
		this.GetComponentsInPuzzleStats(false).ToList<PuzzleSushi>().ForEach(delegate(PuzzleSushi x)
		{
			x.enabled = false;
		});
		base.transform.localScale = Vector3.one * this.hoverScale;
		this.otherUtensil.localScale = Vector3.one * this.hoverScale;
		if (this.fail)
		{
			Global.LevelFailed(0f, true);
		}
		else
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x04001B0A RID: 6922
	public Transform otherUtensil;

	// Token: 0x04001B0B RID: 6923
	public float whiten = 0.1f;

	// Token: 0x04001B0C RID: 6924
	public float originalScale = 0.5446808f;

	// Token: 0x04001B0D RID: 6925
	public float hoverScale = 0.65f;

	// Token: 0x04001B0E RID: 6926
	public float scaleTimeout = 0.08f;

	// Token: 0x04001B0F RID: 6927
	public bool fail;

	// Token: 0x04001B10 RID: 6928
	private bool mouseDown;

	// Token: 0x04001B11 RID: 6929
	private bool mouseIsOver;
}
