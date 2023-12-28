using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class CupJigsawGood_Cup : MonoBehaviour
{
	// Token: 0x060014C8 RID: 5320 RVA: 0x0003A88C File Offset: 0x00038C8C
	public void OnComplete()
	{
		if (this.animationPlayed)
		{
			return;
		}
		this.animationPlayed = true;
		this.enabled = true;
		Audio.self.playOneShot("a9571a83-4c73-4f4c-90ce-2f4dd903b33e", 1f);
		base.transform.GetChild(2).gameObject.SetActive(true);
		foreach (CupJigsawGood_Piece cupJigsawGood_Piece in this.GetComponentsInPuzzleStats(false))
		{
			if (cupJigsawGood_Piece.thisIsCorrectPiece)
			{
				cupJigsawGood_Piece.gameObject.SetActive(false);
			}
			else
			{
				cupJigsawGood_Piece.dragEnabled = false;
			}
		}
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x0003A924 File Offset: 0x00038D24
	private void OnMouseDown()
	{
		if (!this.enabled || !this.active)
		{
			return;
		}
		if (!base.transform.GetChild(2).GetChild(0).gameObject.activeInHierarchy)
		{
			Global.CupAcquired(base.transform);
			this.active = false;
		}
	}

	// Token: 0x0400125C RID: 4700
	private new bool enabled;

	// Token: 0x0400125D RID: 4701
	private bool active = true;

	// Token: 0x0400125E RID: 4702
	private bool animationPlayed;
}
