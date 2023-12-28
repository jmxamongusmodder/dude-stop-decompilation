using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class PivotDraggable : EnhancedDraggable
{
	// Token: 0x06001733 RID: 5939 RVA: 0x0003CB88 File Offset: 0x0003AF88
	protected override void MouseDowned()
	{
		Vector3 point = Camera.main.GetMousePosition() - base.transform.position;
		Vector2 centerOfMass = Quaternion.Euler(0f, 0f, -base.transform.eulerAngles.z) * point;
		base.body.centerOfMass = centerOfMass;
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x0003CBEB File Offset: 0x0003AFEB
	protected override void MouseUpped()
	{
		base.body.centerOfMass = Vector2.zero;
	}
}
