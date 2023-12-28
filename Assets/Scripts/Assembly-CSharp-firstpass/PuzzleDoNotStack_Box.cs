using System;

// Token: 0x020003FB RID: 1019
public class PuzzleDoNotStack_Box : EnhancedDraggable
{
	// Token: 0x060019DE RID: 6622 RVA: 0x00062AC8 File Offset: 0x00060EC8
	private void Start()
	{
		this.GetComponentInPuzzleStats<PuzzleDoNotStack_Controller>().boxes.Add(this);
	}
}
