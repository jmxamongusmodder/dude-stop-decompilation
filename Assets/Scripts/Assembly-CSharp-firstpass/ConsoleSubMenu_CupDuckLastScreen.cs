using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class ConsoleSubMenu_CupDuckLastScreen : ConsoleSubMenuMultiple
{
	// Token: 0x06001E6D RID: 7789 RVA: 0x0008B7D0 File Offset: 0x00089BD0
	protected override IEnumerator showText()
	{
		global::Console.self.hideOldMenu();
		yield return base.StartCoroutine(base.showResponse(this.list[0]));
		yield return new WaitForSeconds(2f);
		yield return base.StartCoroutine(base.showResponse(this.list[1]));
		yield return new WaitForSeconds(0.5f);
		yield return base.StartCoroutine(base.showResponse(this.list[2]));
		yield break;
	}
}
