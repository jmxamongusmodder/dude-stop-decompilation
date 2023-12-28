using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class CupInventoryBad_Trophy : InventoryDraggable
{
	// Token: 0x06000094 RID: 148 RVA: 0x00007EC8 File Offset: 0x000060C8
	private void Start()
	{
		base.GetComponent<CupInventory_InventoryItem>().lockMouse = true;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00007ED6 File Offset: 0x000060D6
	public void Discarded()
	{
		base.EmulateMouseUp();
		this.OnMouseUp();
		this.dragEnabled = false;
		this.ignoreMouseUp = true;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00007EF2 File Offset: 0x000060F2
	protected override void ChangeLooks()
	{
		if (this.grabbed)
		{
			return;
		}
		this.controller.TrophyGrabbed(this);
		this.returnToInventory = false;
		this.grabbed = true;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00007F1A File Offset: 0x0000611A
	public override void OnMouseUp()
	{
		if (!this.ignoreMouseUp)
		{
			base.OnMouseUp();
		}
	}

	// Token: 0x04000120 RID: 288
	public CupInventoryBad_Controller controller;

	// Token: 0x04000121 RID: 289
	[HideInInspector]
	public bool ignoreMouseUp;

	// Token: 0x04000122 RID: 290
	private bool grabbed;
}
