using System;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class AudioVoice_FriendPen : AudioVoiceScrollable
{
	// Token: 0x0600116E RID: 4462 RVA: 0x00020A6B File Offset: 0x0001EE6B
	public override void onTransitionIn()
	{
		if (!this.active)
		{
			return;
		}
		base.playVoice(this.onLoad, false, true, false, true);
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00020A8C File Offset: 0x0001EE8C
	public override bool onTransitionOut()
	{
		if (!this.active)
		{
			return true;
		}
		if (this.newPenShowed && !this.finished)
		{
			base.playVoice(this.leaveNot, true, true, false, true);
		}
		if (this.finished && !this.givenBack && !this.reminded)
		{
			base.playVoice(this.leaveWith, true, false, true, true);
			this.reminded = true;
			return false;
		}
		return true;
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x00020B09 File Offset: 0x0001EF09
	public bool isPlaying()
	{
		return this.voice != null && this.voice.isPlaying();
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x00020B24 File Offset: 0x0001EF24
	public void endPen()
	{
		base.playVoice(this.penEnds, true, true, false, true);
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x00020B37 File Offset: 0x0001EF37
	public void showNewPen()
	{
		this.newPenShowed = true;
		base.playVoice(this.newPen, true, false, true, false);
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x00020B51 File Offset: 0x0001EF51
	public void answerWrong()
	{
		this.wrongAnswered = true;
		base.playVoice(this.answer5, true, true, false, true);
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x00020B6B File Offset: 0x0001EF6B
	public void draw()
	{
		this.newPenShowed = true;
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x00020B74 File Offset: 0x0001EF74
	public void endExam()
	{
		this.finished = true;
		base.playVoice(this.end, true, false, true, true);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x00020B8E File Offset: 0x0001EF8E
	public void givePenBack()
	{
		this.givenBack = true;
		if (this.thanked)
		{
			return;
		}
		this.thanked = base.playVoice(this.giveBack, true, false, true, true);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x00020BB9 File Offset: 0x0001EFB9
	public void takePen()
	{
		this.givenBack = false;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x00020BC2 File Offset: 0x0001EFC2
	public void phoneOut()
	{
		if (!this.finished)
		{
			base.playVoice(this.phone, true, true, false, true);
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x00020BE0 File Offset: 0x0001EFE0
	private void OnApplicationFocus(bool hasFocus)
	{
		if (!this.active || hasFocus || this.wrongAnswered || this.finished)
		{
			return;
		}
		base.playVoice(this.lostFocus, true, true, false, true);
	}

	// Token: 0x04000E7C RID: 3708
	[Space(10f)]
	public StandaloneLevelVoice[] onLoad;

	// Token: 0x04000E7D RID: 3709
	public StandaloneLevelVoice[] penEnds;

	// Token: 0x04000E7E RID: 3710
	public StandaloneLevelVoice[] newPen;

	// Token: 0x04000E7F RID: 3711
	public StandaloneLevelVoice[] answer5;

	// Token: 0x04000E80 RID: 3712
	public StandaloneLevelVoice[] leaveNot;

	// Token: 0x04000E81 RID: 3713
	public StandaloneLevelVoice[] end;

	// Token: 0x04000E82 RID: 3714
	public StandaloneLevelVoice[] leaveWith;

	// Token: 0x04000E83 RID: 3715
	public StandaloneLevelVoice[] giveBack;

	// Token: 0x04000E84 RID: 3716
	public StandaloneLevelVoice[] phone;

	// Token: 0x04000E85 RID: 3717
	public StandaloneLevelVoice[] lostFocus;

	// Token: 0x04000E86 RID: 3718
	private bool newPenShowed;

	// Token: 0x04000E87 RID: 3719
	private bool finished;

	// Token: 0x04000E88 RID: 3720
	private bool givenBack;

	// Token: 0x04000E89 RID: 3721
	private bool reminded;

	// Token: 0x04000E8A RID: 3722
	private bool thanked;

	// Token: 0x04000E8B RID: 3723
	private bool wrongAnswered;
}
