using System;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class CupGetWithClick : MonoBehaviour
{
	// Token: 0x060014B9 RID: 5305 RVA: 0x00039F87 File Offset: 0x00038387
	private void OnMouseDown()
	{
		if (!this.enabled || !this.active)
		{
			return;
		}
		Global.CupAcquired(base.transform);
		this.enabled = false;
	}

	// Token: 0x04001244 RID: 4676
	private new bool enabled = true;

	// Token: 0x04001245 RID: 4677
	public bool active = true;
}
