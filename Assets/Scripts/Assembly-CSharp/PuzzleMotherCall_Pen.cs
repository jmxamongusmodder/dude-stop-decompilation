using System;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class PuzzleMotherCall_Pen : InventoryDraggable
{
	// Token: 0x06001B59 RID: 7001 RVA: 0x00070EC0 File Offset: 0x0006F2C0
	private void Start()
	{
		this.penColl = base.GetComponent<BoxCollider2D>();
		this.holderColl = this.holder.GetComponent<BoxCollider2D>();
		this.holderRend = this.holder.GetComponent<SpriteRenderer>();
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, this.holder.position.x, this.snapDistance), true);
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00070F24 File Offset: 0x0006F324
	private void Update()
	{
		if (!this.dragged)
		{
			return;
		}
		if (this.intersected && !this.holderColl.bounds.Intersects(this.penColl.bounds))
		{
			this.intersected = false;
			base.snapEnabled = true;
			this.lockSnapPoint = false;
		}
		else if (!this.intersected && this.holderColl.bounds.Intersects(this.penColl.bounds))
		{
			this.intersected = true;
			if (this.snapped)
			{
				this.holderRend.sortingLayerName = "Top";
				base.snapEnabled = true;
				this.lockSnapPoint = true;
			}
			else
			{
				this.holderRend.sortingLayerName = "Background";
				base.snapEnabled = false;
				this.lockSnapPoint = false;
			}
		}
		this.snapped = base.Snapped();
	}

	// Token: 0x0400199B RID: 6555
	public Transform holder;

	// Token: 0x0400199C RID: 6556
	public float snapDistance;

	// Token: 0x0400199D RID: 6557
	private bool snapped = true;

	// Token: 0x0400199E RID: 6558
	private bool intersected;

	// Token: 0x0400199F RID: 6559
	private BoxCollider2D holderColl;

	// Token: 0x040019A0 RID: 6560
	private SpriteRenderer holderRend;

	// Token: 0x040019A1 RID: 6561
	private BoxCollider2D penColl;
}
