using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class PuzzleHomework_FirstPaper : PuzzleHomework_Paper
{
	// Token: 0x06001AA0 RID: 6816 RVA: 0x0006998C File Offset: 0x00067D8C
	public override void ChangeSprites()
	{
		base.ChangeSprites();
		this.textCanvas.GetComponent<Canvas>().sortingLayerName = this.layerAfterMove;
	}

	// Token: 0x040018C0 RID: 6336
	public Transform textCanvas;

	// Token: 0x040018C1 RID: 6337
	public GameObject mark;
}
