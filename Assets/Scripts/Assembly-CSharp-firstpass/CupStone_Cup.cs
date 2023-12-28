using System;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class CupStone_Cup : MonoBehaviour
{
	// Token: 0x0600157B RID: 5499 RVA: 0x000415A9 File Offset: 0x0003F9A9
	private void OnMouseDown()
	{
		if (!this.enabled)
		{
			return;
		}
		base.transform.SetParent(this.GetPuzzleStats().transform, true);
		Global.CupAcquired(base.transform);
	}

	// Token: 0x04001334 RID: 4916
	public new bool enabled;
}
