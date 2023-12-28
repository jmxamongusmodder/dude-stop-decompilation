using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class PuzzleMotherCall_RedPen : InventoryDraggable
{
	// Token: 0x17000061 RID: 97
	// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0006F9C8 File Offset: 0x0006DDC8
	protected bool intersected
	{
		set
		{
			if (value == this._intersected)
			{
				return;
			}
			if (value)
			{
				if (!base.Snapped())
				{
					this.SetLayers(this.originalLayer);
					this.verticalSnap.enabled = false;
				}
				else if (base.GetSnapPoint().type == Draggable.Snap.X)
				{
					this.lockSnapPoint = true;
					this.insertionPoint.enabled = false;
					this.SetLayers(this.behindHolderLayer);
				}
			}
			else if (this.lockSnapPoint)
			{
				this.lockSnapPoint = false;
				this.insertionPoint.enabled = true;
				this.SetLayers(this.originalLayer);
			}
			else
			{
				this.verticalSnap.enabled = true;
			}
			this._intersected = value;
		}
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x0006FA87 File Offset: 0x0006DE87
	public virtual void Start()
	{
		this.penColl = base.GetComponent<BoxCollider2D>();
		this.holderColl = this.holder.GetComponent<BoxCollider2D>();
		this.InitSnap();
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x0006FAAC File Offset: 0x0006DEAC
	private void InitSnap()
	{
		this.verticalSnap = new SnapPoint(Draggable.Snap.X, this.holder.position.x + this.holderOffset.x, this.snapDistance);
		base.AddSnapPoint(this.verticalSnap, this.snapToHolder);
		this.insertionPoint = new SnapPoint(Draggable.Snap.XY, this.holder.position + this.holderOffset, this.snapDistance);
		this.insertionPoint.enabled = !this.snapToHolder;
		base.AddSnapPoint(this.insertionPoint, false);
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x0006FB4F File Offset: 0x0006DF4F
	public virtual void Update()
	{
		if (this.dragged)
		{
			this.CheckIntersection();
		}
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x0006FB62 File Offset: 0x0006DF62
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		if (this.activeCoroutine != null)
		{
			base.StopCoroutine(this.activeCoroutine);
			this.activeCoroutine = null;
		}
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x0006FB94 File Offset: 0x0006DF94
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
		if (this.returnToInventory)
		{
			return;
		}
		if (base.Snapped() && base.GetSnapPoint().type == Draggable.Snap.XY)
		{
			this.activeCoroutine = base.StartCoroutine(this.InsertionCoroutine());
		}
		else if (base.Snapped() && base.GetSnapPoint().type == Draggable.Snap.X && this.lockSnapPoint)
		{
			this.activeCoroutine = base.StartCoroutine(this.SlidingCoroutine());
		}
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x0006FC3B File Offset: 0x0006E03B
	protected override void ChangeLooks()
	{
		base.transform.localScale = new Vector3(this.scale, this.scale);
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x0006FC5C File Offset: 0x0006E05C
	private void CheckIntersection()
	{
		this.intersected = this.holderColl.bounds.Intersects(this.penColl.bounds);
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x0006FC90 File Offset: 0x0006E090
	protected void SetLayers(int layer)
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingOrder = layer;
		}
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x0006FCC4 File Offset: 0x0006E0C4
	private IEnumerator InsertionCoroutine()
	{
		float totalTime = this.insertionCurve.GetAnimationLength() * this.insertionTime;
		float timer = 0f;
		float startY = base.transform.position.y;
		bool intersectionProcessed = false;
		bool canProcessIntersection = false;
		while (timer != totalTime)
		{
			timer = Mathf.MoveTowards(timer, totalTime, Time.deltaTime);
			base.transform.SetY(startY + this.insertionCurve.Evaluate(timer / this.insertionTime), false);
			this.CheckIntersection();
			if (!this._intersected && !canProcessIntersection)
			{
				canProcessIntersection = true;
			}
			if (canProcessIntersection && this._intersected && !intersectionProcessed)
			{
				base.snapPoint = this.verticalSnap;
				this.verticalSnap.enabled = true;
				this.lockSnapPoint = true;
				this.SetLayers(this.behindHolderLayer);
				intersectionProcessed = true;
			}
			yield return null;
		}
		this.FinishedInsertion();
		yield break;
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x0006FCE0 File Offset: 0x0006E0E0
	private IEnumerator SlidingCoroutine()
	{
		Global.self.scrollableUI.GetComponent<scrollablePackArrows>().pauseScrolling(0.4f);
		while (base.transform.position.y != this.holder.position.y + this.holderOffset.y)
		{
			base.transform.SetY(Mathf.MoveTowards(base.transform.position.y, this.holder.position.y + this.holderOffset.y, this.returnSpeed * Time.deltaTime), false);
			yield return null;
		}
		this.FinishedInsertion();
		yield break;
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0006FCFB File Offset: 0x0006E0FB
	protected virtual void FinishedInsertion()
	{
	}

	// Token: 0x040019AB RID: 6571
	public Transform holder;

	// Token: 0x040019AC RID: 6572
	public Vector2 holderOffset;

	// Token: 0x040019AD RID: 6573
	public AnimationCurve insertionCurve;

	// Token: 0x040019AE RID: 6574
	public float insertionTime;

	// Token: 0x040019AF RID: 6575
	public float returnSpeed;

	// Token: 0x040019B0 RID: 6576
	public float snapDistance;

	// Token: 0x040019B1 RID: 6577
	public float scale;

	// Token: 0x040019B2 RID: 6578
	protected bool snapToHolder = true;

	// Token: 0x040019B3 RID: 6579
	protected SnapPoint insertionPoint;

	// Token: 0x040019B4 RID: 6580
	protected SnapPoint verticalSnap;

	// Token: 0x040019B5 RID: 6581
	private Coroutine activeCoroutine;

	// Token: 0x040019B6 RID: 6582
	public int originalLayer = 30;

	// Token: 0x040019B7 RID: 6583
	public int behindHolderLayer = 20;

	// Token: 0x040019B8 RID: 6584
	private bool _intersected;

	// Token: 0x040019B9 RID: 6585
	protected BoxCollider2D holderColl;

	// Token: 0x040019BA RID: 6586
	protected BoxCollider2D penColl;
}
