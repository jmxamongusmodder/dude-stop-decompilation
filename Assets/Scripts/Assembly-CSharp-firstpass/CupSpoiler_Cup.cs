using System;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class CupSpoiler_Cup : MonoBehaviour
{
	// Token: 0x06001741 RID: 5953 RVA: 0x0004C930 File Offset: 0x0004AD30
	private void OnMouseDown()
	{
		base.GetComponent<Collider2D>().enabled = false;
		Global.self.canBePaused = true;
		Global.CupAcquired(base.transform);
	}
}
