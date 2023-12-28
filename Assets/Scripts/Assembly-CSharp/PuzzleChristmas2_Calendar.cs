using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003DF RID: 991
public class PuzzleChristmas2_Calendar : PuzzleChristmasCalendar
{
	// Token: 0x060018FE RID: 6398 RVA: 0x0005B848 File Offset: 0x00059C48
	private void Awake()
	{
		base.currentMonth = (this.startMonth = 8);
		this.thisMonth = base.GetComponentInChildren<LineTranslator>();
		base.UpdateMonthText();
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x0005B878 File Offset: 0x00059C78
	protected override IEnumerator BoxShowingCoroutine()
	{
		yield return base.StartCoroutine(base.BoxShowingCoroutine());
		this.tree.GetComponent<Rigidbody2D>().isKinematic = false;
		this.tree.GetComponent<Draggable>().dragEnabled = true;
		this.tree.SetParent(this.GetPuzzleStats().transform);
		yield break;
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x0005B893 File Offset: 0x00059C93
	protected override void TimePassed()
	{
		this.tree.GetComponent<PuzzleChristmas2_Tree>().TimePassed();
	}
}
