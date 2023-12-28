using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class AudioVoice_MamaCall : AudioVoiceScrollable
{
	// Token: 0x060011C8 RID: 4552 RVA: 0x00023030 File Offset: 0x00021430
	public override bool onTransitionOut()
	{
		if (!this.active)
		{
			return true;
		}
		if (this.phoneAnswered && !InventoryControl.self.isItemInInventory<PuzzleMotherCall_Phone>() && this.GetComponentInPuzzleStats<PuzzleMotherCall_Phone>())
		{
			base.playVoice(this.leave, true, true, false, true);
		}
		return true;
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x00023086 File Offset: 0x00021486
	public override void onTransitionIn()
	{
		if (!this.active)
		{
			return;
		}
		if (InventoryControl.self.isItemInInventory<PuzzleMotherCall_BlackPen>())
		{
			base.playVoice(this.brindPen, true, true, false, true);
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x000230B4 File Offset: 0x000214B4
	public void answerPhone()
	{
		this.phoneAnswered = true;
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000230BD File Offset: 0x000214BD
	public void dropCall()
	{
		base.playVoice(this.drop, true, false, true, true);
		this.phoneAnswered = true;
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000230D7 File Offset: 0x000214D7
	public void putPenIn()
	{
		if (this.penInPlayed)
		{
			return;
		}
		this.penInPlayed = base.playVoice(this.penIn, true, false, true, true);
	}

	// Token: 0x04000ED8 RID: 3800
	[Space(10f)]
	public StandaloneLevelVoice[] drop;

	// Token: 0x04000ED9 RID: 3801
	public StandaloneLevelVoice[] leave;

	// Token: 0x04000EDA RID: 3802
	public StandaloneLevelVoice[] brindPen;

	// Token: 0x04000EDB RID: 3803
	public StandaloneLevelVoice[] penIn;

	// Token: 0x04000EDC RID: 3804
	private bool penInPlayed;

	// Token: 0x04000EDD RID: 3805
	private bool phoneAnswered;
}
