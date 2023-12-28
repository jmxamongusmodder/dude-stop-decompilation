using System;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class CupLastCup_Link : MonoBehaviour
{
	// Token: 0x060014EE RID: 5358 RVA: 0x0003B4B9 File Offset: 0x000398B9
	private void OnMouseDown()
	{
		this.sign.ClickLink();
	}

	// Token: 0x0400127E RID: 4734
	public CupLastCup_Sign sign;
}
