using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class PuzzleCrayons_Crayon : PivotDraggable
{
	// Token: 0x06000109 RID: 265 RVA: 0x0000A69B File Offset: 0x0000889B
	private void Update()
	{
		this.Rotate();
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000A6A4 File Offset: 0x000088A4
	private void Rotate()
	{
		if (!this.dragged)
		{
			return;
		}
		float num = base.transform.eulerAngles.z;
		num = Mathf.MoveTowardsAngle(num, 0f, this.rotationSpeed * Time.deltaTime);
		base.body.MoveRotation(num);
	}

	// Token: 0x0400018F RID: 399
	public float rotationSpeed;
}
