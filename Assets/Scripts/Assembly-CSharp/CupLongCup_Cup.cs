using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class CupLongCup_Cup : CupLongCup_Draggable
{
	// Token: 0x0600152F RID: 5423 RVA: 0x0003DCC4 File Offset: 0x0003C0C4
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (this.removedFromLetter)
		{
			Global.CupAcquired(base.transform);
		}
		base.GetComponent<Rigidbody2D>().isKinematic = true;
	}
}
