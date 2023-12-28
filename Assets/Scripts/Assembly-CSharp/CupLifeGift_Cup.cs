using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class CupLifeGift_Cup : MonoBehaviour
{
	// Token: 0x060000BB RID: 187 RVA: 0x00008BDB File Offset: 0x00006DDB
	private void OnMouseDown()
	{
		if (this.canBeAcquired)
		{
			Global.CupAcquired(base.transform);
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0400013A RID: 314
	public bool canBeAcquired;
}
