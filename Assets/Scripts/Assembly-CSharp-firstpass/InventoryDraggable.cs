using System;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class InventoryDraggable : PivotDraggable
{
	// Token: 0x060016B8 RID: 5816 RVA: 0x000497D6 File Offset: 0x00047BD6
	private void Awake()
	{
		if (this.returnToPoint)
		{
			this.limit.EnableAll();
			this.limit.disableDragOnBorder = false;
		}
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000497FC File Offset: 0x00047BFC
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!this.dragged && this.returnToPoint && base.transform.position != this.inventoryReturnPoint)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.inventoryReturnPoint, this.inventoryReturnSpeed * Time.fixedDeltaTime);
			if (base.transform.position == this.inventoryReturnPoint)
			{
				this.OnReturnPositionReached();
			}
		}
		this.CheckReturnPosition();
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000498A8 File Offset: 0x00047CA8
	public virtual void OnEnable()
	{
		if (this.checkBounds)
		{
			Vector2 v = Camera.main.WorldToViewportPoint(base.transform.position);
			if (v.x < this.limit.leftVal || v.x > this.limit.rightVal || v.y < this.limit.bottomVal || v.y > this.limit.topVal)
			{
				v.x = Mathf.Clamp(v.x, this.limit.leftVal, this.limit.rightVal);
				v.y = Mathf.Clamp(v.y, this.limit.bottomVal, this.limit.topVal);
				this.boundReturnPosition = Camera.main.ViewportToWorldPoint(v);
			}
		}
		if (base.body != null)
		{
			base.body.velocity = Vector2.zero;
		}
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x000499C5 File Offset: 0x00047DC5
	public virtual void OnDisable()
	{
		if (this.dragged)
		{
			base.OnMouseUp();
		}
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000499D8 File Offset: 0x00047DD8
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.boundReturnPosition = Vector2.zero;
		base.OnMouseDown();
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000499F8 File Offset: 0x00047DF8
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.body != null)
		{
			base.body.velocity = Vector2.zero;
		}
		if (this.returnToInventory)
		{
			base.GetComponent<InventoryItem>().moveBackToInventory();
			this.MoveBackToInventory();
		}
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x00049A54 File Offset: 0x00047E54
	public virtual void OnReturnPositionReached()
	{
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x00049A58 File Offset: 0x00047E58
	public virtual void ExitInventory()
	{
		if (!base.IsDragged())
		{
			base.SetDelta(Vector3.zero);
			string pickUpSound = this.pickUpSound;
			this.pickUpSound = null;
			this.OnMouseDown();
			this.pickUpSound = pickUpSound;
			base.EmulateMouseUp();
		}
		this.ChangeLooks();
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x00049AA2 File Offset: 0x00047EA2
	public virtual void EnterInventory()
	{
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x00049AA4 File Offset: 0x00047EA4
	public virtual void DroppedInInventory()
	{
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x00049AA6 File Offset: 0x00047EA6
	protected virtual void ChangeLooks()
	{
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x00049AA8 File Offset: 0x00047EA8
	protected virtual void MoveBackToInventory()
	{
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x00049AAC File Offset: 0x00047EAC
	private void CheckReturnPosition()
	{
		if (this.boundReturnPosition == Vector2.zero)
		{
			return;
		}
		base.transform.position = Vector2.MoveTowards(base.transform.position, this.boundReturnPosition, this.boundReturnSpeed * Time.deltaTime);
		if (base.transform.position == this.boundReturnPosition)
		{
		}
	}

	// Token: 0x04001479 RID: 5241
	[HideInInspector]
	public bool returnToInventory;

	// Token: 0x0400147A RID: 5242
	public bool returnToPoint;

	// Token: 0x0400147B RID: 5243
	public Vector2 inventoryReturnPoint;

	// Token: 0x0400147C RID: 5244
	public float inventoryReturnSpeed;

	// Token: 0x0400147D RID: 5245
	public bool checkBounds;

	// Token: 0x0400147E RID: 5246
	private Vector2 boundReturnPosition;

	// Token: 0x0400147F RID: 5247
	private float boundReturnSpeed = 10f;
}
