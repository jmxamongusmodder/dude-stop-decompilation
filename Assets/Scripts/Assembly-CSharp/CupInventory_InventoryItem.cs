using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class CupInventory_InventoryItem : InventoryItem
{
	// Token: 0x06000085 RID: 133 RVA: 0x00007181 File Offset: 0x00005381
	public override bool mouseDownInInventory()
	{
		if (!this.lockMouse)
		{
			base.mouseDownInInventory();
		}
		return !this.lockMouse;
	}

	// Token: 0x04000107 RID: 263
	[HideInInspector]
	public bool lockMouse;
}
