using System;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class AudioVoice_HomeWork : AudioVoiceScrollable
{
	// Token: 0x06001183 RID: 4483 RVA: 0x00020E18 File Offset: 0x0001F218
	public override void onTransitionIn()
	{
		if (!this.active)
		{
			return;
		}
		bool? flag = this.finished;
		if (flag != null)
		{
			base.playVoice(this.comeback, true, true, false, true);
		}
		else if (InventoryControl.self.isItemInInventory<PuzzleHomework_Pen>())
		{
			base.playVoice(this.onLoad, true, true, false, true);
		}
		else
		{
			base.playVoice(this.whereIsPencil, true, true, false, true);
		}
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00020E90 File Offset: 0x0001F290
	public override bool onTransitionOut()
	{
		if (!this.active)
		{
			return true;
		}
		bool? flag = this.finished;
		if (flag == null && this.startedDrawing)
		{
			base.playVoice(this.leave, true, true, false, true);
		}
		return true;
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00020EE0 File Offset: 0x0001F2E0
	public void onCalendarClick(bool last)
	{
		bool? flag = this.finished;
		if (flag != null)
		{
			return;
		}
		if (last)
		{
			base.playVoice(this.lastDay, true, false, true, true);
		}
		else if (this.clickPlayed < 2 && UnityEngine.Random.value > 0.5f)
		{
			base.playVoice(this.calendar, true, false, true, true);
			this.clickPlayed++;
		}
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00020F57 File Offset: 0x0001F357
	public void finishPuzzle(bool monster)
	{
		this.finished = new bool?(monster);
		if (!monster)
		{
			base.playVoice(this.finish, true, true, false, true);
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00020F7F File Offset: 0x0001F37F
	public void startDrawing()
	{
		this.startedDrawing = true;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00020F88 File Offset: 0x0001F388
	public void pullOutPhone()
	{
		bool? flag = this.finished;
		if (flag != null)
		{
			return;
		}
		base.playVoice(this.phone, false, true, false, true);
	}

	// Token: 0x04000E8F RID: 3727
	[Space(10f)]
	public StandaloneLevelVoice[] onLoad;

	// Token: 0x04000E90 RID: 3728
	public StandaloneLevelVoice[] whereIsPencil;

	// Token: 0x04000E91 RID: 3729
	public StandaloneLevelVoice[] calendar;

	// Token: 0x04000E92 RID: 3730
	public StandaloneLevelVoice[] lastDay;

	// Token: 0x04000E93 RID: 3731
	public StandaloneLevelVoice[] finish;

	// Token: 0x04000E94 RID: 3732
	public StandaloneLevelVoice[] leave;

	// Token: 0x04000E95 RID: 3733
	public StandaloneLevelVoice[] comeback;

	// Token: 0x04000E96 RID: 3734
	public StandaloneLevelVoice[] phone;

	// Token: 0x04000E97 RID: 3735
	private bool? finished;

	// Token: 0x04000E98 RID: 3736
	private int clickPlayed;

	// Token: 0x04000E99 RID: 3737
	private bool startedDrawing;
}
