using System;

// Token: 0x0200040D RID: 1037
public class PuzzleFriendsPen_Phone : InventoryDraggable
{
	// Token: 0x06001A58 RID: 6744 RVA: 0x00067235 File Offset: 0x00065635
	public override void ExitInventory()
	{
		this.returnToInventory = true;
		Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().phoneOut();
		base.ExitInventory();
	}
}
