using System;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class PuzzleHomework_Pen : DrawingPen
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0006A286 File Offset: 0x00068686
	private PuzzleHomework_Calendar calendar
	{
		get
		{
			if (this._calendar == null && this.GetPuzzleStats() != null)
			{
				this._calendar = this.GetComponentInPuzzleStats<PuzzleHomework_Calendar>();
			}
			return this._calendar;
		}
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x0006A2BC File Offset: 0x000686BC
	public override void EnterInventory()
	{
		base.EnterInventory();
		this.SetCalendarPenStatusTo(false);
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x0006A2CB File Offset: 0x000686CB
	protected override void ChangeLooks()
	{
		if (Global.getCompletionState(null) != CompletionState.None)
		{
			this.returnToInventory = true;
		}
		this.SetCalendarPenStatusTo(true);
		base.transform.localScale = Vector2.one * this.scale;
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x0006A306 File Offset: 0x00068706
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.SetCalendarPenStatusTo(true);
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x0006A315 File Offset: 0x00068715
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		this.SetCalendarPenStatusTo(false);
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x0006A324 File Offset: 0x00068724
	private void SetCalendarPenStatusTo(bool status)
	{
		if (this.calendar != null)
		{
			this.calendar.penHeld = status;
		}
	}

	// Token: 0x040018D8 RID: 6360
	public float scale = 0.7f;

	// Token: 0x040018D9 RID: 6361
	private PuzzleHomework_Calendar _calendar;
}
