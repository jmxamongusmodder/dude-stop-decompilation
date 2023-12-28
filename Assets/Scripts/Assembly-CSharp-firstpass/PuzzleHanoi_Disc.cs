using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class PuzzleHanoi_Disc : Draggable
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00068360 File Offset: 0x00066760
	// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00068368 File Offset: 0x00066768
	public int moves { get; private set; }

	// Token: 0x06001A82 RID: 6786 RVA: 0x00068374 File Offset: 0x00066774
	private void Start()
	{
		List<SnapPoint> list = new List<SnapPoint>();
		float num = 999f;
		Transform transform = null;
		IEnumerator enumerator = base.transform.parent.Find("Base").GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				if (transform2.name.StartsWith("Rod"))
				{
					if (Vector2.Distance(base.transform.position, transform2.position) < num)
					{
						num = Vector2.Distance(base.transform.position, transform2.position);
						transform = transform2;
					}
					list.Add(new SnapPoint(Draggable.Snap.X, transform2.position.x, this.snapDistance, transform2));
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
		base.SetSnapPoints(list);
		this.currentSnap = transform;
		this.lastSnap = transform;
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x00068490 File Offset: 0x00066890
	protected override void OnSnap(SnapPoint point)
	{
		if (base.transform.position.y >= this.minY)
		{
			this.canLockSnapPoint = true;
		}
		else
		{
			this.canLockSnapPoint = false;
		}
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x000684CE File Offset: 0x000668CE
	protected override void OnUnsnap(SnapPoint point)
	{
		this.canLockSnapPoint = false;
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000684D7 File Offset: 0x000668D7
	protected override void OnSnapChange(SnapPoint oldPoint, SnapPoint newPoint)
	{
		this.canLockSnapPoint = true;
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x000684E0 File Offset: 0x000668E0
	private void Update()
	{
		this.mov = this.moves;
		this.lockSnapPoint = (base.transform.position.y < this.minY && base.Snapped() && this.canLockSnapPoint);
		SnapPoint snapPoint = base.GetSnapPoint();
		this.currentSnap = ((snapPoint != null) ? snapPoint.transform : null);
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x00068550 File Offset: 0x00066950
	public override void OnMouseDown()
	{
		if (this.CanMove())
		{
			base.OnMouseDown();
		}
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x00068564 File Offset: 0x00066964
	public override void OnMouseUp()
	{
		if (!this.dragged)
		{
			return;
		}
		base.OnMouseUp();
		foreach (SnapPoint snapPoint in this.snapPoints)
		{
			if (base.transform.position.y < 0.2f && Mathf.Abs(base.transform.position.x - snapPoint.transform.position.x) < this.snapDistance)
			{
				this.currentSnap = snapPoint.transform;
			}
		}
		if (this.currentSnap == null)
		{
			this.onlyOnRods = false;
		}
		if (this.GetLowerDiscValue() < this.value)
		{
			this.onlyOrdered = false;
		}
		if (this.currentSnap == null || this.currentSnap != this.lastSnap)
		{
			this.lastSnap = this.currentSnap;
			this.moves++;
		}
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000686A0 File Offset: 0x00066AA0
	public bool IsCorrect()
	{
		return this.onlyOnRods && this.onlyOrdered;
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x000686B8 File Offset: 0x00066AB8
	private int GetLowerDiscValue()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, -base.transform.up, 10f, mask);
		base.gameObject.layer = LayerMask.NameToLayer("Front");
		return (!(raycastHit2D.collider != null)) ? 999 : raycastHit2D.collider.GetComponent<PuzzleHanoi_Disc>().value;
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x00068768 File Offset: 0x00066B68
	private bool CanMove()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		bool flag = true;
		flag &= (Physics2D.Raycast(base.transform.position, base.transform.up, 10f, mask).collider == null);
		flag &= (Physics2D.Raycast(base.transform.position + new Vector3(0.5f, 0f), base.transform.up, 10f, mask).collider == null);
		flag &= (Physics2D.Raycast(base.transform.position - new Vector3(0.5f, 0f), base.transform.up, 10f, mask).collider == null);
		Debug.DrawRay(base.transform.position - new Vector3(0.5f, 0f), base.transform.up, Color.red, 0.5f);
		Debug.DrawRay(base.transform.position + new Vector3(0.5f, 0f), base.transform.up, Color.red, 0.5f);
		Debug.DrawRay(base.transform.position, base.transform.up, Color.red, 0.5f);
		base.gameObject.layer = LayerMask.NameToLayer("Front");
		return flag;
	}

	// Token: 0x040018A2 RID: 6306
	public float snapDistance = 0.3f;

	// Token: 0x040018A3 RID: 6307
	public int value;

	// Token: 0x040018A5 RID: 6309
	public float minY = 0.26f;

	// Token: 0x040018A6 RID: 6310
	private Transform lastSnap;

	// Token: 0x040018A7 RID: 6311
	private Transform currentSnap;

	// Token: 0x040018A8 RID: 6312
	private bool canLockSnapPoint;

	// Token: 0x040018A9 RID: 6313
	private bool onlyOnRods = true;

	// Token: 0x040018AA RID: 6314
	private bool onlyOrdered = true;

	// Token: 0x040018AB RID: 6315
	public int mov;
}
