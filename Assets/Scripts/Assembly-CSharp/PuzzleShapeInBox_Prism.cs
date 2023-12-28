using System;

// Token: 0x02000447 RID: 1095
public class PuzzleShapeInBox_Prism : PuzzleShapeInBox_SnappableObject
{
	// Token: 0x06001C06 RID: 7174 RVA: 0x00075F4F File Offset: 0x0007434F
	protected override bool CanBeSnapped()
	{
		return this.lid.prismCanBeSnapped;
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00075F5C File Offset: 0x0007435C
	protected override bool StupidPosition()
	{
		return this.lid.prismStupid;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x00075F69 File Offset: 0x00074369
	protected override void NotifyLid()
	{
		this.lid.TriangleInside();
	}
}
