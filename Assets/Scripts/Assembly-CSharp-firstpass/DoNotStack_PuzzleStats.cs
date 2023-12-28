using System;

// Token: 0x02000375 RID: 885
public class DoNotStack_PuzzleStats : PuzzleStats
{
	// Token: 0x060015BC RID: 5564 RVA: 0x00043E6C File Offset: 0x0004226C
	public override void TimeHasEnded()
	{
		base.GetComponentInChildren<PuzzleDoNotStack_Controller>().TimeHasEnded();
	}
}
