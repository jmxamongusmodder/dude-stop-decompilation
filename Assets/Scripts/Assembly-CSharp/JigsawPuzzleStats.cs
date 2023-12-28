using System;

// Token: 0x02000393 RID: 915
public class JigsawPuzzleStats : PuzzleStats
{
	// Token: 0x060016E4 RID: 5860 RVA: 0x0004A542 File Offset: 0x00048942
	public override void makePauseMenu()
	{
		base.GetComponentInChildren<PuzzleJigsaw_Controller>().SaveJigsaw(false);
		base.makePauseMenu();
	}
}
