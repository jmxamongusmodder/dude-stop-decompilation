using System;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public class ScreenEndCard : AbstractUIScreen
{
	// Token: 0x0600205A RID: 8282 RVA: 0x0009EEAE File Offset: 0x0009D2AE
	public override void Update()
	{
		base.Update();
		if (Input.GetButtonDown("Submit") && this.active)
		{
			this.bContinue();
		}
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x0009EED6 File Offset: 0x0009D2D6
	public override void setActive(bool active)
	{
		base.setActive(active);
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x0009EEDF File Offset: 0x0009D2DF
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x0009EEE8 File Offset: 0x0009D2E8
	public virtual void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		if (this.puzzle.GetComponent<PuzzleStats>().active)
		{
			Global.self.gotoNextLevel(false, null);
		}
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x0009EF2A File Offset: 0x0009D32A
	protected override void cancelPressed()
	{
		this.bContinue();
	}

	// Token: 0x040023A6 RID: 9126
	protected Transform puzzle;
}
