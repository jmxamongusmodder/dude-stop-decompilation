using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class CupCakeIsTrue_Cake : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x00004CAF File Offset: 0x00002EAF
	private void OnMouseDown()
	{
		if (!this.enabled)
		{
			return;
		}
		this.GetComponentInPuzzleStats<CupCakeIsTrue_Candle>().SwitchLights();
		Global.CupAcquired(base.transform);
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x040000B9 RID: 185
	public new bool enabled;
}
