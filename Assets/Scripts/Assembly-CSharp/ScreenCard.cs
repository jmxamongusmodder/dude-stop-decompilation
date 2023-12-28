using System;
using UnityEngine;

// Token: 0x0200057D RID: 1405
public class ScreenCard : AbstractUIScreen
{
	// Token: 0x06002054 RID: 8276 RVA: 0x0009EDD4 File Offset: 0x0009D1D4
	public bool checkIfAllowed()
	{
		return (!this.ifNoBadPuzzles && !this.ifNoGoodPuzzles) || (this.ifNoBadPuzzles && AwardController.self.getProgress(false) == 0) || (this.ifNoGoodPuzzles && AwardController.self.getProgress(true) == 0);
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x0009EE34 File Offset: 0x0009D234
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x0009EE38 File Offset: 0x0009D238
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x0009EE65 File Offset: 0x0009D265
	public void bExit()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.left, true);
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x0009EE98 File Offset: 0x0009D298
	protected override void cancelPressed()
	{
		if (!this.active)
		{
			return;
		}
	}

	// Token: 0x040023A4 RID: 9124
	[Header("Conditions")]
	[Tooltip("Only show this card if NO BAD puzzles was solved")]
	public bool ifNoBadPuzzles;

	// Token: 0x040023A5 RID: 9125
	[Tooltip("Only show this card if NO GOOD puzzles was solved")]
	public bool ifNoGoodPuzzles;
}
