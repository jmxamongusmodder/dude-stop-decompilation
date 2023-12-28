using System;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class CupLego_PuzzleStats : PuzzleStats
{
	// Token: 0x0600173F RID: 5951 RVA: 0x0004C8CC File Offset: 0x0004ACCC
	public override void makePauseMenu()
	{
		if (Global.self.replayingCupPuzzle)
		{
			AnalyticsComponent.PuzzleFinished(base.name);
			Global.TellAnalyticsLevelFinished();
			Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.left, true);
		}
		else
		{
			base.makePauseMenu();
		}
	}
}
