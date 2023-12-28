using System;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class packSelectArrowAnimation : MonoBehaviour
{
	// Token: 0x0600201D RID: 8221 RVA: 0x0009CC7C File Offset: 0x0009B07C
	public void showArrow()
	{
		if (this.active)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.active = true;
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x0009CC9D File Offset: 0x0009B09D
	public void hideArrow()
	{
		if (!this.active)
		{
			return;
		}
		base.gameObject.SetActive(false);
		this.active = false;
	}

	// Token: 0x0400235F RID: 9055
	[HideInInspector]
	public bool active = true;
}
