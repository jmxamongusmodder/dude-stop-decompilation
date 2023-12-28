using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class ConsoleSubMenu_loading : ConsoleSubMenuDefault
{
	// Token: 0x06001E7A RID: 7802 RVA: 0x0008CB58 File Offset: 0x0008AF58
	public override IEnumerator showMenu()
	{
		this.startCommand.gameObject.SetActive(true);
		LineTranslator lt = this.startCommand.GetComponent<LineTranslator>();
		string txt = lt.currentText;
		lt.setTextNoTranslation(txt + ".");
		yield return new WaitForSeconds(this.loadingDotsDelay);
		lt.setTextNoTranslation(txt + "..");
		yield return new WaitForSeconds(this.loadingDotsDelay);
		lt.setTextNoTranslation(txt + "...");
		yield return new WaitForSeconds(0.1f);
		foreach (GameObject obj in this.menuItems)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.14999999f));
			obj.SetActive(true);
		}
		yield return new WaitForSeconds(this.delayAfterLoad);
		base.StartCoroutine(this.switchMenu());
		yield break;
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x0008CB74 File Offset: 0x0008AF74
	private IEnumerator switchMenu()
	{
		yield return null;
		global::Console.self.switchMenu(global::Console.self.contactingDeveloper);
		yield break;
	}

	// Token: 0x040021C6 RID: 8646
	public float loadingDotsDelay = 0.2f;

	// Token: 0x040021C7 RID: 8647
	public float delayAfterLoad = 0.5f;
}
