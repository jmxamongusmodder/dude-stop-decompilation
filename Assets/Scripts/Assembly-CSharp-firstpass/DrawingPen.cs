using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AD RID: 941
public class DrawingPen : InventoryDraggable
{
	// Token: 0x06001755 RID: 5973 RVA: 0x0004D8E2 File Offset: 0x0004BCE2
	private void Update()
	{
		this.CheckRotation();
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x0004D8EA File Offset: 0x0004BCEA
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.state = DrawingPen.State.Dragged;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x0004D905 File Offset: 0x0004BD05
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponentInChildren<DrawingPenPoint>().DropPixel();
		base.OnMouseUp();
		this.state = DrawingPen.State.Released;
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x0004D92B File Offset: 0x0004BD2B
	public virtual bool IsDrawing()
	{
		return base.IsDragged() && this.state == DrawingPen.State.DraggedIdle;
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x0004D944 File Offset: 0x0004BD44
	public virtual void PixelDrawn(DrawingCanvas paper, int pixelsDrawn)
	{
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x0004D946 File Offset: 0x0004BD46
	public virtual void Depleted()
	{
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x0004D948 File Offset: 0x0004BD48
	public override void EnterInventory()
	{
		this.state = DrawingPen.State.Released;
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x0004D970 File Offset: 0x0004BD70
	protected override void MoveBackToInventory()
	{
		this.EnterInventory();
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x0004D978 File Offset: 0x0004BD78
	private void SetPivotPoint()
	{
		Vector2 b = Camera.main.GetMousePosition();
		Vector3 vector = base.transform.position - b;
		this.pivotDelta = Quaternion.Inverse(base.transform.rotation) * vector;
		this.pivotScale = base.transform.localScale.x;
		base.transform.position -= vector;
		this.pivoted.Clear();
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (!(transform.tag == "Inventory Sprite") && transform.gameObject.activeInHierarchy)
				{
					transform.position += vector;
					this.pivoted.Add(transform);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x0004DAAC File Offset: 0x0004BEAC
	private void ReleasePivotPoint()
	{
		float d = (this.pivotScale == 0f) ? 1f : (base.transform.localScale.x / this.pivotScale);
		Vector3 b = base.transform.rotation * this.pivotDelta * d;
		base.transform.position += b;
		this.pivotDelta = Vector2.zero;
		foreach (Transform transform in this.pivoted)
		{
			transform.position -= b;
		}
		this.pivoted.Clear();
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x0004DB98 File Offset: 0x0004BF98
	protected virtual void CheckRotation()
	{
		if (this.state != DrawingPen.State.Dragged && this.state != DrawingPen.State.Released)
		{
			return;
		}
		float target = (!this.dragged) ? this.defaultAngle : this.rotatedAngle;
		float num = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, target, this.rotationSpeed * Time.deltaTime);
		if (this.useRigidbody)
		{
			base.body.MoveRotation(num);
		}
		else
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, num);
		}
		float num2 = 0.1f;
		if (Mathf.Abs(Mathf.DeltaAngle(num, target)) < num2)
		{
			this.state = ((this.state != DrawingPen.State.Dragged) ? DrawingPen.State.ReleasedIdle : DrawingPen.State.DraggedIdle);
		}
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x0004DC67 File Offset: 0x0004C067
	public override void ExitInventory()
	{
		base.OnMouseDown();
		base.SetDelta(Vector3.zero);
		base.EmulateMouseUp();
		this.state = DrawingPen.State.Dragged;
		this.ChangeLooks();
	}

	// Token: 0x0400153B RID: 5435
	public float rotatedAngle;

	// Token: 0x0400153C RID: 5436
	public float defaultAngle;

	// Token: 0x0400153D RID: 5437
	public float rotationSpeed;

	// Token: 0x0400153E RID: 5438
	protected DrawingPen.State state = DrawingPen.State.ReleasedIdle;

	// Token: 0x0400153F RID: 5439
	private Vector2 pivotDelta;

	// Token: 0x04001540 RID: 5440
	private float pivotScale;

	// Token: 0x04001541 RID: 5441
	private List<Transform> pivoted = new List<Transform>();

	// Token: 0x020003AE RID: 942
	public enum State
	{
		// Token: 0x04001543 RID: 5443
		Dragged,
		// Token: 0x04001544 RID: 5444
		DraggedIdle,
		// Token: 0x04001545 RID: 5445
		Released,
		// Token: 0x04001546 RID: 5446
		ReleasedIdle
	}
}
