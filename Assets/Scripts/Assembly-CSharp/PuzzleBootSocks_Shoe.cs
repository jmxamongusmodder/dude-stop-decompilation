using System;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class PuzzleBootSocks_Shoe : EnhancedDraggable
{
	// Token: 0x06001836 RID: 6198 RVA: 0x0005478C File Offset: 0x00052B8C
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, this.foot.position.x, this.snapDistance), false);
		this.socks = this.GetComponentInPuzzleStats<PuzzleBootSocks_Sock>();
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000547CB File Offset: 0x00052BCB
	private void Update()
	{
		this.CheckReturn();
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000547D4 File Offset: 0x00052BD4
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			if (this.socks.snapped)
			{
				Global.LevelCompleted(0f, true);
			}
			else
			{
				Global.LevelFailed(0f, true);
			}
		}
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x0005482C File Offset: 0x00052C2C
	private void CheckReturn()
	{
		if (this.dragged || base.Snapped() || !base.WasMoved())
		{
			return;
		}
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.startingPosition, this.returnSpeed * Time.deltaTime);
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x00054888 File Offset: 0x00052C88
	protected override void OnSnap(SnapPoint point)
	{
		Audio.self.playOneShot("c0d4a2f8-9a07-4ff2-a22b-c52bfd720cf3", 1f);
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x0005489F File Offset: 0x00052C9F
	protected override void OnUnsnap(SnapPoint point)
	{
	}

	// Token: 0x04001625 RID: 5669
	public Transform foot;

	// Token: 0x04001626 RID: 5670
	public float returnSpeed = 2f;

	// Token: 0x04001627 RID: 5671
	public float snapDistance;

	// Token: 0x04001628 RID: 5672
	private PuzzleBootSocks_Sock socks;
}
