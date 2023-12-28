using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000459 RID: 1113
public class PuzzleStuckBalloon_StringPart : PivotDraggable
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00079489 File Offset: 0x00077889
	public bool snapped
	{
		get
		{
			return this._snapped;
		}
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x00079491 File Offset: 0x00077891
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.snapPoint.position, Color.cyan, 0.5f);
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000794B2 File Offset: 0x000778B2
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		this.CheckSnap();
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x000794C0 File Offset: 0x000778C0
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		foreach (Collider2D collider2D in this.tree.GetComponentsInChildren<Collider2D>())
		{
			collider2D.enabled = false;
		}
		this.GetComponentInPuzzleStats<AudioVoice_StuckBalloon>().cancelCatchLine();
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x00079518 File Offset: 0x00077918
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (this._snapped)
		{
			Global.LevelFailed(0f, true);
			this.GetComponentInPuzzleStats<PuzzleStuckBalloon_Lines>().enabled = true;
			this.GetComponentInPuzzleStats<PuzzleStuckBalloon_StringLines>().enabled = true;
			this.snapPoint.GetComponent<DistanceJoint2D>().enabled = true;
			this.GetComponentInPuzzleStats<AudioVoice_StuckBalloon>().cancelCatchLine();
		}
		else if (base.transform.position.x < this.tree.position.x - 2f)
		{
			this.GetComponentInPuzzleStats<AudioVoice_StuckBalloon>().sayCatchItLine();
		}
		foreach (Collider2D collider2D in this.tree.GetComponentsInChildren<Collider2D>())
		{
			collider2D.enabled = true;
		}
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x000795F4 File Offset: 0x000779F4
	protected override Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		if (!this._snapped)
		{
			return mouse - delta;
		}
		mouse -= delta;
		float sqrMagnitude = (mouse - this.snapPoint.position).sqrMagnitude;
		if (sqrMagnitude < this.unsnapDist * this.unsnapDist)
		{
			mouse = Vector2.ClampMagnitude(mouse - this.snapPoint.position, this.unsnapDist - this.unsnapThreshold);
			mouse += this.snapPoint.position;
		}
		else
		{
			base.StartCoroutine(this.TemporarySnapDisableCoroutine());
			base.StartCoroutine(this.SnapCountCoroutine());
			if (this.snaps > 5)
			{
				this.GetComponentInPuzzleStats<AudioVoice_StuckBalloon>().onSnap();
			}
		}
		return mouse;
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x000796C4 File Offset: 0x00077AC4
	private void CheckSnap()
	{
		if (!this.dragged || !this.canBeSnapped)
		{
			return;
		}
		this._snapped = false;
		float d = 0.32f;
		Vector2 vector = this.snapPoint.position + this.lastStringPart.up * d / 2f;
		if (Vector2.Distance(this.lastStringPart.position, vector) < this.snapDist)
		{
			this.lastStringPart.GetComponent<Rigidbody2D>().MovePosition(vector);
			this._snapped = true;
		}
		else if (Vector2.Distance(this.lastStringPart.transform.position, vector) < this.pullDistance)
		{
			Vector2 position = Vector2.MoveTowards(this.lastStringPart.position, vector, this.pullSpeed * Time.deltaTime);
			this.lastStringPart.GetComponent<Rigidbody2D>().MovePosition(position);
		}
	}

	// Token: 0x06001C93 RID: 7315 RVA: 0x000797C0 File Offset: 0x00077BC0
	private IEnumerator TemporarySnapDisableCoroutine()
	{
		this.canBeSnapped = false;
		this._snapped = false;
		yield return new WaitForSeconds(this.waitAfterUnsnap);
		this.canBeSnapped = true;
		yield break;
	}

	// Token: 0x06001C94 RID: 7316 RVA: 0x000797DC File Offset: 0x00077BDC
	private IEnumerator SnapCountCoroutine()
	{
		this.snaps++;
		yield return new WaitForSeconds(60f);
		this.snaps--;
		yield break;
	}

	// Token: 0x04001AFE RID: 6910
	[Header("String stuff")]
	public Transform lastStringPart;

	// Token: 0x04001AFF RID: 6911
	public Transform tree;

	// Token: 0x04001B00 RID: 6912
	[Header("Snap")]
	public new Transform snapPoint;

	// Token: 0x04001B01 RID: 6913
	public float snapDist;

	// Token: 0x04001B02 RID: 6914
	public float unsnapDist;

	// Token: 0x04001B03 RID: 6915
	public float unsnapThreshold = 0.5f;

	// Token: 0x04001B04 RID: 6916
	public float waitAfterUnsnap = 0.5f;

	// Token: 0x04001B05 RID: 6917
	[Header("Move towards")]
	public float pullDistance;

	// Token: 0x04001B06 RID: 6918
	public float pullSpeed;

	// Token: 0x04001B07 RID: 6919
	private bool _snapped;

	// Token: 0x04001B08 RID: 6920
	private bool canBeSnapped = true;

	// Token: 0x04001B09 RID: 6921
	private int snaps;
}
