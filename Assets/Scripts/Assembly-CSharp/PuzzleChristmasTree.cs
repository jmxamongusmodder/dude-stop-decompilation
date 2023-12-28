using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class PuzzleChristmasTree : PivotDraggable
{
	// Token: 0x0600191A RID: 6426 RVA: 0x0005BE14 File Offset: 0x0005A214
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.up * 2.2f);
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x0005BE60 File Offset: 0x0005A260
	private void Update()
	{
		this.Raycast();
		this.CheckVictoryConditions();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x0005BE6E File Offset: 0x0005A26E
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (!this.dragEnabled || !base.enabled)
		{
			return;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_ChristmasTree>().onDrag();
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x0005BEA1 File Offset: 0x0005A2A1
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		Global.self.currPuzzle.GetComponent<AudioVoice_ChristmasTree>().onDrop();
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x0005BEBD File Offset: 0x0005A2BD
	public void TimePassed()
	{
		this.timePassed = true;
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x0005BEC6 File Offset: 0x0005A2C6
	private void CheckVictoryConditions()
	{
		if (this.dragged || !this.inTheBox)
		{
			return;
		}
		if (this.timePassed)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x0005BF08 File Offset: 0x0005A308
	private void Raycast()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		this.inTheBox = (Physics2D.Raycast(base.transform.position, base.transform.up, 2.2f, mask).collider != null);
	}

	// Token: 0x0400171A RID: 5914
	private bool inTheBox;

	// Token: 0x0400171B RID: 5915
	private bool timePassed;
}
