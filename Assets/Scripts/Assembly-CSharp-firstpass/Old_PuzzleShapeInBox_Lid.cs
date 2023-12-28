using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class Old_PuzzleShapeInBox_Lid : Draggable
{
	// Token: 0x060017AC RID: 6060 RVA: 0x0004FCE7 File Offset: 0x0004E0E7
	private void Start()
	{
		this.shapes = this.GetComponentsInPuzzleStats(false);
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x0004FCF6 File Offset: 0x0004E0F6
	private void Update()
	{
		this.CheckVictoryConditions();
	}

	// Token: 0x060017AE RID: 6062 RVA: 0x0004FD00 File Offset: 0x0004E100
	private void CheckVictoryConditions()
	{
		if (this.dragged)
		{
			return;
		}
		if (Vector2.Distance(base.transform.position, this.box.position) > this.distanceToBox)
		{
			return;
		}
		if ((from x in this.shapes
		where x.IsDragged() || !x.insideTheBox
		select x).Count<Old_PuzzleShapeInBox_Shape>() > 0)
		{
			return;
		}
		foreach (Old_PuzzleShapeInBox_Shape old_PuzzleShapeInBox_Shape in this.shapes)
		{
			old_PuzzleShapeInBox_Shape.dragEnabled = false;
		}
		bool monster = (from x in this.shapes
		where x.throughTheLid
		select x).Count<Old_PuzzleShapeInBox_Shape>() == 0;
		base.StartCoroutine(this.Countdown(monster));
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x0004FDE4 File Offset: 0x0004E1E4
	private IEnumerator Countdown(bool monster)
	{
		yield return new WaitForSeconds(this.waitBeforeEnd);
		if (!base.enabled)
		{
			yield break;
		}
		if (monster)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		yield break;
	}

	// Token: 0x04001575 RID: 5493
	public Transform box;

	// Token: 0x04001576 RID: 5494
	public float distanceToBox;

	// Token: 0x04001577 RID: 5495
	public float waitBeforeEnd;

	// Token: 0x04001578 RID: 5496
	private Old_PuzzleShapeInBox_Shape[] shapes;
}
