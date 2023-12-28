using System;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public class UIButton_RatingButton : ButtonTemplate
{
	// Token: 0x060020B3 RID: 8371 RVA: 0x000A0C18 File Offset: 0x0009F018
	public override void soundEnter()
	{
		base.soundEnter();
		foreach (GameObject gameObject in this.showList)
		{
			gameObject.SetActive(true);
		}
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x000A0C54 File Offset: 0x0009F054
	public override void soundExit()
	{
		base.soundExit();
		foreach (GameObject gameObject in this.showList)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x04002404 RID: 9220
	public GameObject[] showList;
}
