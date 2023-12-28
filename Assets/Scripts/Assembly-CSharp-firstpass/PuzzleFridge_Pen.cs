using System;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class PuzzleFridge_Pen : InventoryDraggable
{
	// Token: 0x06001A1A RID: 6682 RVA: 0x00064D50 File Offset: 0x00063150
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.pics == null || this.pics.Length == 0 || this.alreadyDrew)
		{
			return;
		}
		foreach (PuzzleFridgePaintings puzzleFridgePaintings in this.pics)
		{
			Bounds bounds = puzzleFridgePaintings.GetComponent<Collider2D>().bounds;
			bounds.center = bounds.center.XY();
			if (bounds.Contains(this.tip.position.XY()))
			{
				this.alreadyDrew = true;
				Global.self.currPuzzle.GetComponent<AudioVoice_Fridge>().tryToDraw();
			}
		}
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x00064DFC File Offset: 0x000631FC
	public override void ExitInventory()
	{
		this.pics = this.GetComponentsInPuzzleStats(true);
		this.returnToInventory = true;
		base.ExitInventory();
	}

	// Token: 0x04001825 RID: 6181
	public Transform tip;

	// Token: 0x04001826 RID: 6182
	private PuzzleFridgePaintings[] pics;

	// Token: 0x04001827 RID: 6183
	private bool alreadyDrew;
}
