using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class Draggable : MonoBehaviour
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060015BE RID: 5566 RVA: 0x000363F9 File Offset: 0x000347F9
	protected Rigidbody2D body
	{
		get
		{
			if (this._body == null)
			{
				this._body = base.GetComponent<Rigidbody2D>();
			}
			return this._body;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060015BF RID: 5567 RVA: 0x0003641E File Offset: 0x0003481E
	private InventoryItem inventoryItem
	{
		get
		{
			if (this._inventoryItem == null)
			{
				this._inventoryItem = base.GetComponent<InventoryItem>();
			}
			return this._inventoryItem;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00036443 File Offset: 0x00034843
	// (set) Token: 0x060015C1 RID: 5569 RVA: 0x0003644B File Offset: 0x0003484B
	public bool snapEnabled
	{
		get
		{
			return this._snapEnabled;
		}
		set
		{
			this._snapEnabled = value;
			if (!value)
			{
				this.snapPoint = null;
			}
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00036461 File Offset: 0x00034861
	// (set) Token: 0x060015C3 RID: 5571 RVA: 0x0003646C File Offset: 0x0003486C
	protected SnapPoint snapPoint
	{
		get
		{
			return this._snapPoint;
		}
		set
		{
			if (this._snapPoint != null && value == null)
			{
				this.OnUnsnap(this._snapPoint);
			}
			else if (this._snapPoint == null && value != null)
			{
				this.OnSnap(value);
			}
			else if (this._snapPoint != null && value != null && value != this._snapPoint)
			{
				this.OnSnapChange(this._snapPoint, value);
			}
			this._snapPoint = value;
		}
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000364E9 File Offset: 0x000348E9
	protected virtual void OnDrawGizmosSelected()
	{
		if (this.limit != null)
		{
			this.limit.DrawGizmos();
		}
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x00036501 File Offset: 0x00034901
	public virtual void FixedUpdate()
	{
		this.CalculateMouseMovement();
		if (this.emulateMouseUp && !Input.GetMouseButton(0))
		{
			this.OnMouseUp();
			this.emulateMouseUp = false;
		}
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x0003652C File Offset: 0x0003492C
	public virtual void OnMouseDown()
	{
		if (!this.dragEnabled)
		{
			return;
		}
		this.sqrMoveLimit = this.moveLimit * this.moveLimit;
		this.dragged = true;
		if (this.startingPosition == -Vector3.one)
		{
			this.startingPosition = base.transform.position;
		}
		if (this.inventoryItem)
		{
			this.InventoryItemMouseDown();
		}
		if (base.enabled && !string.IsNullOrEmpty(this.pickUpSound))
		{
			Audio.self.playOneShot(this.pickUpSound, 1f);
		}
		this.SetDelta(Camera.main.GetMousePosition() - base.transform.position);
		this.initialDelta = this.delta;
		this.initialScale = base.transform.localScale;
		this.MouseDowned();
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x00036620 File Offset: 0x00034A20
	public virtual void OnMouseUp()
	{
		if (!this.dragEnabled)
		{
			return;
		}
		this.BeforeMouseUpped();
		this.dragged = false;
		if (this.useRigidbody && this.allowThrow && this.body != null)
		{
			this.body.velocity = (base.transform.localPosition - this.lastPos) * 50f * this.throwQuotient;
		}
		if (this.body != null && this.body.IsSleeping())
		{
			this.body.WakeUp();
		}
		this.MouseUpped();
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x000366DA File Offset: 0x00034ADA
	public void EmulateMouseUp()
	{
		this.emulateMouseUp = true;
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x000366E4 File Offset: 0x00034AE4
	public Vector3 GetDelta()
	{
		return (!this.rotateDelta) ? this.delta : (Quaternion.Euler(0f, 0f, base.transform.eulerAngles.z) * this.delta);
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x00036734 File Offset: 0x00034B34
	public void SetDelta(Vector3 delta)
	{
		this.delta = ((!this.rotateDelta) ? delta : (Quaternion.Euler(0f, 0f, -base.transform.eulerAngles.z) * delta));
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x00036781 File Offset: 0x00034B81
	public bool IsDragged()
	{
		return this.dragged;
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x00036789 File Offset: 0x00034B89
	protected bool WasMoved()
	{
		return base.transform.position != this.startingPosition && this.startingPosition != -1f * Vector3.one;
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000367C3 File Offset: 0x00034BC3
	protected virtual Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		return mouse - delta;
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x000367CC File Offset: 0x00034BCC
	protected void AddSnapPoint(SnapPoint point, bool snap = false)
	{
		this.snapPoints.Add(point);
		if (snap)
		{
			this.snapPoint = point;
		}
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x000367E7 File Offset: 0x00034BE7
	protected void RemoveSnapPoint(SnapPoint point)
	{
		this.snapPoints.Remove(point);
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x000367F8 File Offset: 0x00034BF8
	protected void RemoveSnapPoint(Transform point)
	{
		this.snapPoints.RemoveAll((SnapPoint x) => x.transform == point);
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x0003682A File Offset: 0x00034C2A
	protected void RemoveAllSnapPoints()
	{
		this.snapPoints.Clear();
		this.snapPoint = null;
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x0003683E File Offset: 0x00034C3E
	protected void SetSnapPoints(List<SnapPoint> points)
	{
		this.snapPoints = points;
	}

	// Token: 0x060015D3 RID: 5587 RVA: 0x00036847 File Offset: 0x00034C47
	protected SnapPoint GetSnapPoint()
	{
		return this.snapPoint;
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x0003684F File Offset: 0x00034C4F
	protected bool Snapped()
	{
		return this.snapEnabled && this.GetSnapPoint() != null;
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x0003686B File Offset: 0x00034C6B
	protected virtual void AnnounceMovement(Vector3 currPos, Vector3 nextPos)
	{
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x0003686D File Offset: 0x00034C6D
	private void CalculateMouseMovement()
	{
		if (!this.dragged)
		{
			return;
		}
		if (this.useRigidbody)
		{
			this.MoveBodyToMouse();
		}
		else
		{
			this.MoveObjectToMouse();
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x00036898 File Offset: 0x00034C98
	protected virtual void MoveBodyToMouse()
	{
		if (this.body == null)
		{
			Debug.LogError("Trying to move a body without a rigidbody!");
			return;
		}
		Vector3 vector = this.GetMousePosition();
		if (!this.CheckLimits(vector))
		{
			vector = this.limit.newMousePosition;
		}
		vector = this.CheckSnapPoints(vector);
		vector -= base.transform.position;
		this.body.velocity = Vector3.zero;
		this.body.angularVelocity = 0f;
		if (vector.sqrMagnitude > this.sqrMoveLimit)
		{
			vector = vector.normalized * this.moveLimit;
		}
		if (this.lockX)
		{
			vector.x = 0f;
		}
		if (this.lockY)
		{
			vector.y = 0f;
		}
		this.AnnounceMovement(this.body.position, this.body.position + vector);
		this.lastPos = base.transform.localPosition;
		this.body.MovePosition(this.body.position + vector);
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x000369DC File Offset: 0x00034DDC
	private void MoveObjectToMouse()
	{
		Vector3 vector = this.GetMousePosition();
		if (this.scaleDelta)
		{
			this.SetDelta(new Vector3(this.initialDelta.x / (this.initialScale.x / base.transform.localScale.x), this.initialDelta.y / (this.initialScale.y / base.transform.localScale.y)));
		}
		if (!this.CheckLimits(vector))
		{
			vector = this.limit.newMousePosition;
		}
		vector = this.CheckSnapPoints(vector);
		if (this.lockX)
		{
			vector.x = base.transform.position.x;
		}
		if (this.lockY)
		{
			vector.y = base.transform.position.y;
		}
		this.AnnounceMovement(base.transform.position, vector);
		base.transform.position = vector;
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x00036AE8 File Offset: 0x00034EE8
	private Vector3 GetMousePosition()
	{
		Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouse.z = 0f;
		return this.ProcessMousePosition(mouse, this.GetDelta());
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x00036B20 File Offset: 0x00034F20
	private bool CheckLimits(Vector3 mouse)
	{
		if (!this.limit.Check(mouse))
		{
			if (this.limit.disableDragOnBorder)
			{
				this.LimitReached();
				this.OnMouseUp();
			}
			return false;
		}
		return true;
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x00036B54 File Offset: 0x00034F54
	private Vector3 CheckSnapPoints(Vector3 mouse)
	{
		if (this.snapPoints.Count == 0 || !this.snapEnabled)
		{
			return mouse;
		}
		SnapPoint snapPoint = null;
		foreach (SnapPoint snapPoint2 in this.snapPoints)
		{
			if (snapPoint2.enabled)
			{
				if (!this.lockSnapPoint || snapPoint2 == this.snapPoint)
				{
					if (snapPoint2.type == Draggable.Snap.XY && Vector2.Distance(mouse, snapPoint2.coord2D) < snapPoint2.distance)
					{
						snapPoint = snapPoint2;
						break;
					}
					if (snapPoint2.type == Draggable.Snap.X && (Mathf.Abs(mouse.x - snapPoint2.coord) < snapPoint2.distance || this.lockSnapPoint))
					{
						snapPoint = snapPoint2;
					}
					else if (snapPoint2.type == Draggable.Snap.Y && (Mathf.Abs(mouse.y - snapPoint2.coord) < snapPoint2.distance || this.lockSnapPoint))
					{
						snapPoint = snapPoint2;
					}
				}
			}
		}
		this.snapPoint = snapPoint;
		if (this.snapPoint != null)
		{
			if (this.snapPoint.type == Draggable.Snap.XY)
			{
				mouse = this.snapPoint.coord2D;
			}
			else if (this.snapPoint.type == Draggable.Snap.X)
			{
				mouse = new Vector2(this.snapPoint.coord, mouse.y);
			}
			else if (this.snapPoint.type == Draggable.Snap.Y)
			{
				mouse = new Vector2(mouse.x, this.snapPoint.coord);
			}
		}
		return mouse;
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00036D34 File Offset: 0x00035134
	protected virtual void InventoryItemMouseDown()
	{
		this.inventoryItem.mouseDown();
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00036D41 File Offset: 0x00035141
	protected virtual void LimitReached()
	{
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00036D43 File Offset: 0x00035143
	protected virtual void OnSnap(SnapPoint point)
	{
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00036D45 File Offset: 0x00035145
	protected virtual void OnUnsnap(SnapPoint point)
	{
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x00036D47 File Offset: 0x00035147
	protected virtual void OnSnapChange(SnapPoint oldPoint, SnapPoint newPoint)
	{
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x00036D49 File Offset: 0x00035149
	protected virtual void BeforeMouseUpped()
	{
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x00036D4B File Offset: 0x0003514B
	protected virtual void MouseUpped()
	{
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x00036D4D File Offset: 0x0003514D
	protected virtual void MouseDowned()
	{
	}

	// Token: 0x04001373 RID: 4979
	[Header("Sounds")]
	[EventRef]
	public string pickUpSound;

	// Token: 0x04001374 RID: 4980
	[Header("Draggable stuff")]
	public Draggable.Limit limit;

	// Token: 0x04001375 RID: 4981
	public bool dragEnabled = true;

	// Token: 0x04001376 RID: 4982
	public bool useRigidbody;

	// Token: 0x04001377 RID: 4983
	public float throwQuotient = 0.75f;

	// Token: 0x04001378 RID: 4984
	public bool allowThrow = true;

	// Token: 0x04001379 RID: 4985
	public float moveLimit = 2f;

	// Token: 0x0400137A RID: 4986
	private float sqrMoveLimit = -1f;

	// Token: 0x0400137B RID: 4987
	public bool rotateDelta;

	// Token: 0x0400137C RID: 4988
	public bool lockX;

	// Token: 0x0400137D RID: 4989
	public bool lockY;

	// Token: 0x0400137E RID: 4990
	protected Vector3 startingPosition = -1f * Vector3.one;

	// Token: 0x0400137F RID: 4991
	protected bool dragged;

	// Token: 0x04001380 RID: 4992
	protected bool scaleDelta;

	// Token: 0x04001381 RID: 4993
	private Rigidbody2D _body;

	// Token: 0x04001382 RID: 4994
	private InventoryItem _inventoryItem;

	// Token: 0x04001383 RID: 4995
	private Vector2 lastPos;

	// Token: 0x04001384 RID: 4996
	private Vector3 delta;

	// Token: 0x04001385 RID: 4997
	private Vector3 initialDelta;

	// Token: 0x04001386 RID: 4998
	private Vector3 initialScale;

	// Token: 0x04001387 RID: 4999
	private bool emulateMouseUp;

	// Token: 0x04001388 RID: 5000
	private bool _snapEnabled = true;

	// Token: 0x04001389 RID: 5001
	protected bool lockSnapPoint;

	// Token: 0x0400138A RID: 5002
	[HideInInspector]
	public List<SnapPoint> snapPoints = new List<SnapPoint>();

	// Token: 0x0400138B RID: 5003
	private SnapPoint _snapPoint;

	// Token: 0x02000377 RID: 887
	public enum Snap
	{
		// Token: 0x0400138D RID: 5005
		None,
		// Token: 0x0400138E RID: 5006
		X,
		// Token: 0x0400138F RID: 5007
		Y,
		// Token: 0x04001390 RID: 5008
		XY
	}

	// Token: 0x02000378 RID: 888
	[Serializable]
	public class Limit
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x00036DB4 File Offset: 0x000351B4
		public void DrawGizmos()
		{
			if (!this.limit)
			{
				return;
			}
			this.DrawSide(this.top, this.topVal, this.topScreen, true);
			this.DrawSide(this.bottom, this.bottomVal, this.bottomScreen, true);
			this.DrawSide(this.left, this.leftVal, this.leftScreen, false);
			this.DrawSide(this.right, this.rightVal, this.rightScreen, false);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00036E34 File Offset: 0x00035234
		public bool Check(Vector3 mouse)
		{
			if (!this.limit)
			{
				return true;
			}
			bool flag = true;
			flag &= this.CheckSide(ref mouse.y, this.top, this.topVal, this.topScreen, true, true);
			flag &= this.CheckSide(ref mouse.y, this.bottom, this.bottomVal, this.bottomScreen, false, true);
			flag &= this.CheckSide(ref mouse.x, this.left, this.leftVal, this.leftScreen, false, false);
			flag &= this.CheckSide(ref mouse.x, this.right, this.rightVal, this.rightScreen, true, false);
			if (!flag)
			{
				this.newMousePosition = mouse;
			}
			return flag;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00036EEE File Offset: 0x000352EE
		public void EnableAll()
		{
			this.top = true;
			this.left = true;
			this.right = true;
			this.bottom = true;
			this.limit = true;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00036F13 File Offset: 0x00035313
		public void EnableAll(float top, float right, float bottom, float left)
		{
			this.EnableAll();
			this.topVal = top;
			this.leftVal = left;
			this.rightVal = right;
			this.bottomVal = bottom;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00036F38 File Offset: 0x00035338
		private bool CheckSide(ref float value, bool side, float threshold, bool screen, bool sign, bool topOrBottom)
		{
			if (!side)
			{
				return true;
			}
			bool flag;
			if (screen)
			{
				if (topOrBottom)
				{
					Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(0f, threshold));
					flag = ((!sign) ? (value > vector.y) : (value < vector.y));
					if (!flag)
					{
						value = vector.y;
					}
				}
				else
				{
					Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(threshold, 0f));
					flag = ((!sign) ? (value > vector2.x) : (value < vector2.x));
					if (!flag)
					{
						value = vector2.x;
					}
				}
			}
			else
			{
				flag = ((!sign) ? (value > threshold) : (value < threshold));
				if (!flag)
				{
					value = threshold;
				}
			}
			return flag;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00037018 File Offset: 0x00035418
		private void DrawSide(bool side, float val, bool screen, bool topOrBottom)
		{
			if (!side)
			{
				return;
			}
			Gizmos.color = Color.gray;
			if (screen)
			{
				if (topOrBottom)
				{
					GizmosExtension.DrawHorizontalLine(Camera.main.ViewportToWorldPoint(new Vector3(0f, val)).y, -10f, 10f);
				}
				else
				{
					GizmosExtension.DrawVerticalLine(Camera.main.ViewportToWorldPoint(new Vector3(val, 0f)).x);
				}
			}
			else if (topOrBottom)
			{
				GizmosExtension.DrawHorizontalLine(val, -10f, 10f);
			}
			else
			{
				GizmosExtension.DrawVerticalLine(val);
			}
		}

		// Token: 0x04001391 RID: 5009
		public bool limit;

		// Token: 0x04001392 RID: 5010
		public bool disableDragOnBorder = true;

		// Token: 0x04001393 RID: 5011
		public Vector3 newMousePosition;

		// Token: 0x04001394 RID: 5012
		public bool top;

		// Token: 0x04001395 RID: 5013
		public bool bottom;

		// Token: 0x04001396 RID: 5014
		public bool left;

		// Token: 0x04001397 RID: 5015
		public bool right;

		// Token: 0x04001398 RID: 5016
		public float topVal = 0.95f;

		// Token: 0x04001399 RID: 5017
		public float rightVal = 0.95f;

		// Token: 0x0400139A RID: 5018
		public float bottomVal = 0.05f;

		// Token: 0x0400139B RID: 5019
		public float leftVal = 0.05f;

		// Token: 0x0400139C RID: 5020
		public bool topScreen = true;

		// Token: 0x0400139D RID: 5021
		public bool bottomScreen = true;

		// Token: 0x0400139E RID: 5022
		public bool leftScreen = true;

		// Token: 0x0400139F RID: 5023
		public bool rightScreen = true;

		// Token: 0x040013A0 RID: 5024
		public bool unfolded;
	}
}
