using System;
using UnityEngine;

// Token: 0x020003C8 RID: 968
public class PuzzleBootSocks_Sock : EnhancedDraggable
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600183D RID: 6205 RVA: 0x000548B4 File Offset: 0x00052CB4
	public bool snapped
	{
		get
		{
			return base.Snapped();
		}
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000548BC File Offset: 0x00052CBC
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, this.foot.position.x, this.snapDistance), false);
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000548EF File Offset: 0x00052CEF
	private void Update()
	{
		this.CheckReturn();
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x000548F8 File Offset: 0x00052CF8
	private void CheckReturn()
	{
		if (this.dragged || base.Snapped() || !base.WasMoved())
		{
			return;
		}
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.startingPosition, this.returnSpeed * Time.deltaTime);
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x00054954 File Offset: 0x00052D54
	protected override void OnSnap(SnapPoint point)
	{
		Audio.self.playOneShot("407aa12c-a1ec-4448-b32d-4b09be1c3481", 1f);
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x0005496B File Offset: 0x00052D6B
	protected override void OnUnsnap(SnapPoint point)
	{
		Audio.self.playOneShot("407aa12c-a1ec-4448-b32d-4b09be1c3481", 1f);
	}

	// Token: 0x04001629 RID: 5673
	public Transform foot;

	// Token: 0x0400162A RID: 5674
	public float returnSpeed = 2f;

	// Token: 0x0400162B RID: 5675
	public float snapDistance;
}
