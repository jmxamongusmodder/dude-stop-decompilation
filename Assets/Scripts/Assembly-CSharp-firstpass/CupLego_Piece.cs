using System;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class CupLego_Piece : LegoPiece
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06001523 RID: 5411 RVA: 0x0003D4DD File Offset: 0x0003B8DD
	private CupLego_Controller controller
	{
		get
		{
			if (this._controller == null)
			{
				this._controller = this.GetComponentInPuzzleStats<CupLego_Controller>();
			}
			return this._controller;
		}
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x0003D502 File Offset: 0x0003B902
	private void Update()
	{
		this.CheckReturning();
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x0003D50C File Offset: 0x0003B90C
	protected override float GetHeight()
	{
		return base.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.extents.y;
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0003D53F File Offset: 0x0003B93F
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.returning = false;
		this.SetColliderStatusTo(true);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x0003D558 File Offset: 0x0003B958
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if ((!this.lockX && !this.IsInsideSpawnZone()) || (this.lockX && this.insideRestrictedZone))
		{
			this.ReturnTo(this.controller.GetRandomPoint(base.transform.position));
		}
		else if (this.IsInsideSpawnZone())
		{
			this.SetColliderStatusTo(false);
		}
		base.body.SetKinematic();
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0003D5DA File Offset: 0x0003B9DA
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.insideRestrictedZone = true;
		}
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x0003D5F8 File Offset: 0x0003B9F8
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.insideRestrictedZone = false;
		}
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x0003D616 File Offset: 0x0003BA16
	public void ReturnTo(Vector2 position)
	{
		this.returning = true;
		this.returnStart = base.transform.position;
		this.returnEnd = position;
		this.timer = 0f;
		this.SetColliderStatusTo(false);
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x0003D650 File Offset: 0x0003BA50
	public void SetColliderStatusTo(bool status)
	{
		foreach (BoxCollider2D boxCollider2D in base.GetComponentsInChildren<BoxCollider2D>())
		{
			boxCollider2D.isTrigger = !status;
		}
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x0003D688 File Offset: 0x0003BA88
	private bool IsInsideSpawnZone()
	{
		foreach (BoxCollider2D boxCollider2D in this.spawn.GetComponentsInChildren<BoxCollider2D>())
		{
			if (boxCollider2D.OverlapPoint(base.transform.position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x0003D6D8 File Offset: 0x0003BAD8
	private void CheckReturning()
	{
		if (!this.returning)
		{
			return;
		}
		this.timer = Mathf.MoveTowards(this.timer, this.returnTime, Time.deltaTime);
		float t = Mathf.Sin(this.timer / this.returnTime * 3.1415927f * 0.5f);
		Vector2 v = Vector2.Lerp(this.returnStart, this.returnEnd, t);
		base.transform.localPosition = v;
		base.Upd();
		if (this.timer == this.returnTime)
		{
			this.returning = false;
		}
	}

	// Token: 0x040012B5 RID: 4789
	public float returnTime;

	// Token: 0x040012B6 RID: 4790
	public Transform spawn;

	// Token: 0x040012B7 RID: 4791
	public bool insideRestrictedZone;

	// Token: 0x040012B8 RID: 4792
	private bool returning;

	// Token: 0x040012B9 RID: 4793
	private Vector2 returnStart;

	// Token: 0x040012BA RID: 4794
	private Vector2 returnEnd;

	// Token: 0x040012BB RID: 4795
	private float timer;

	// Token: 0x040012BC RID: 4796
	private CupLego_Controller _controller;
}
