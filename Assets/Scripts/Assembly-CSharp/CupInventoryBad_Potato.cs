using System;

// Token: 0x02000019 RID: 25
public class CupInventoryBad_Potato : InventoryDraggable
{
	// Token: 0x06000091 RID: 145 RVA: 0x00007E9E File Offset: 0x0000609E
	private void Start()
	{
		this.returnToInventory = true;
		base.GetComponent<CupInventory_InventoryItem>().lockMouse = true;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00007EB3 File Offset: 0x000060B3
	protected override void ChangeLooks()
	{
		this.controller.PotatoGrabbed();
	}

	// Token: 0x0400011F RID: 287
	public CupInventoryBad_Controller controller;
}
