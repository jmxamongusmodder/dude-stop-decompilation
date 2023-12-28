using System;

// Token: 0x02000408 RID: 1032
public class PuzzleFridgePaintings_Keys : InventoryDraggable
{
	// Token: 0x06001A3C RID: 6716 RVA: 0x00066050 File Offset: 0x00064450
	public override void OnReturnPositionReached()
	{
		base.OnReturnPositionReached();
		Audio.self.playOneShot("ce4af325-fdbc-4ed0-8f6a-3eefef12037a", 1f);
	}
}
