using System;

// Token: 0x02000434 RID: 1076
public class PuzzleMotherCall_PhoneOnHomework : InventoryDraggable
{
	// Token: 0x06001B65 RID: 7013 RVA: 0x0007150B File Offset: 0x0006F90B
	public override void ExitInventory()
	{
		base.ExitInventory();
		this.returnToInventory = true;
		Global.self.currPuzzle.GetComponent<AudioVoice_HomeWork>().pullOutPhone();
	}
}
