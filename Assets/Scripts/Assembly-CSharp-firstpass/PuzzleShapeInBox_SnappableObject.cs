using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x02000449 RID: 1097
public class PuzzleShapeInBox_SnappableObject : PuzzleShapeInBox_Object
{
	// Token: 0x06001C13 RID: 7187 RVA: 0x00075370 File Offset: 0x00073770
	public override void Start()
	{
		base.Start();
		this.point = new SnapPoint(Draggable.Snap.XY, this.lidSnapPoint.position, this.snapDistance, this.lidSnapPoint);
		base.AddSnapPoint(this.point, false);
		this.insidePoint = new SnapPoint(Draggable.Snap.XY, this.lidSnapPoint.position + Vector2.down * this.snapDistance, this.snapDistance, this.lidSnapPoint)
		{
			enabled = false
		};
		base.AddSnapPoint(this.insidePoint, false);
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x0007540B File Offset: 0x0007380B
	private void Update()
	{
		this.UpdateSnapPointData();
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x00075413 File Offset: 0x00073813
	private void OnDisable()
	{
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x00075424 File Offset: 0x00073824
	public override void OnMouseUp()
	{
		if (!this.dragged && !this.forcedUnsnap)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped() || this.forcedUnsnap)
		{
			this.forcedUnsnap = false;
			if (this.StupidPosition())
			{
				base.StartCoroutine(this.FallingThroughCoroutine());
			}
			else
			{
				this.dragEnabled = false;
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, base.GetComponent<Rigidbody2D>().velocity.y);
				base.GetComponent<BoxCollider2D>().isTrigger = true;
				base.gameObject.layer = LayerMask.NameToLayer("No touching");
				this.NotifyLid();
				Global.self.currPuzzle.GetComponent<AudioVoice_ShapesInBox>().inHole();
			}
		}
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000754F4 File Offset: 0x000738F4
	protected void UpdateSnapPointData()
	{
		if (!this.dragged)
		{
			return;
		}
		this.point.enabled = this.CanBeSnapped();
		this.point.coord2D = this.point.transform.position;
		this.insidePoint.coord2D = this.point.coord2D + Vector2.down * this.snapDistance;
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x00075569 File Offset: 0x00073969
	protected override void OnSnap(SnapPoint point)
	{
		if (point != this.insidePoint && !this.StupidPosition())
		{
			this.insidePoint.enabled = true;
		}
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x0007558E File Offset: 0x0007398E
	protected override void OnUnsnap(SnapPoint point)
	{
		if (point != this.insidePoint)
		{
			this.insidePoint.enabled = false;
		}
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x000755A8 File Offset: 0x000739A8
	protected override void OnSnapChange(SnapPoint oldPoint, SnapPoint newPoint)
	{
		if (newPoint == this.insidePoint)
		{
			this.forcedUnsnap = true;
			this.dragged = false;
			this.dragEnabled = false;
			base.snapEnabled = false;
			base.RemoveAllSnapPoints();
			base.snapPoint = oldPoint;
			oldPoint.enabled = false;
			newPoint.enabled = false;
			this.point.enabled = false;
			this.insidePoint.enabled = false;
			newPoint.coord2D = oldPoint.coord2D;
			this.OnMouseUp();
			base.transform.position = oldPoint.coord2D;
		}
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x00075638 File Offset: 0x00073A38
	protected virtual bool CanBeSnapped()
	{
		return false;
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x0007563B File Offset: 0x00073A3B
	protected virtual bool StupidPosition()
	{
		return false;
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x0007563E File Offset: 0x00073A3E
	protected virtual void NotifyLid()
	{
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x00075640 File Offset: 0x00073A40
	private IEnumerator FallingThroughCoroutine()
	{
		this.dragEnabled = false;
		Collider2D lowerLid = (from x in this.lid.GetComponentsInChildren<Collider2D>()
		where !x.isTrigger
		select x).First<Collider2D>();
		lowerLid.gameObject.layer = LayerMask.NameToLayer("Front");
		base.gameObject.layer = LayerMask.NameToLayer("Back");
		base.GetComponent<Rigidbody2D>().constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
		this.lid.dragEnabled = false;
		yield return new WaitForSeconds(0.3f);
		Global.self.currPuzzle.GetComponent<AudioVoice_ShapesInBox>().inWrongHole();
		Bounds b;
		do
		{
			b = lowerLid.bounds;
			b.center = new Vector2(b.center.x, b.center.y);
			yield return null;
		}
		while (base.GetComponent<BoxCollider2D>().bounds.Intersects(b));
		this.lid.dragEnabled = true;
		this.dragEnabled = true;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		lowerLid.gameObject.layer = LayerMask.NameToLayer("Default");
		base.gameObject.layer = LayerMask.NameToLayer("Default");
		yield break;
	}

	// Token: 0x04001A6A RID: 6762
	public float snapDistance;

	// Token: 0x04001A6B RID: 6763
	public Transform lidSnapPoint;

	// Token: 0x04001A6C RID: 6764
	private SnapPoint point;

	// Token: 0x04001A6D RID: 6765
	private SnapPoint insidePoint;

	// Token: 0x04001A6E RID: 6766
	private bool forcedUnsnap;
}
