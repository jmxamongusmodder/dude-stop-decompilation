using System;

// Token: 0x02000444 RID: 1092
public class PuzzleShapeInBox_Cylinder : PuzzleShapeInBox_SnappableObject
{
	// Token: 0x06001BEB RID: 7147 RVA: 0x0007589D File Offset: 0x00073C9D
	protected override bool CanBeSnapped()
	{
		return this.lid.cylinderCanBeSnapped;
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000758AA File Offset: 0x00073CAA
	protected override bool StupidPosition()
	{
		return this.lid.cylinderStupid;
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x000758B7 File Offset: 0x00073CB7
	protected override void NotifyLid()
	{
		this.lid.CylinderInside();
	}
}
