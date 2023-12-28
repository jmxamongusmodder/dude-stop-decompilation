using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class PuzzleCup_SpoilerCup : PuzzleCup
{
	// Token: 0x0600143A RID: 5178 RVA: 0x000342F4 File Offset: 0x000326F4
	public override void SetCup(Banner banner)
	{
		base.SetCup(banner);
		this.textCanvas.sortingLayerName = "Front";
		this.textCanvas.sortingOrder += banner.cupLayer;
	}

	// Token: 0x04001168 RID: 4456
	public Canvas textCanvas;
}
