using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000536 RID: 1334
public class ConsoleSubMenuDefault : ConsoleSubMenu
{
	// Token: 0x06001E8F RID: 7823 RVA: 0x0008A114 File Offset: 0x00088514
	public override void setMenu()
	{
		ButtonTemplate[] componentsInChildren = base.GetComponentsInChildren<ButtonTemplate>(true);
		foreach (ButtonTemplate buttonTemplate in componentsInChildren)
		{
			buttonTemplate.callbackMouseOn = new Action<RectTransform>(global::Console.self.mouseOver);
		}
		base.setMenu();
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x0008A160 File Offset: 0x00088560
	public override IEnumerator showMenu()
	{
		yield return base.StartCoroutine(global::Console.self.typeCommand(this.startCommand.text, 0f, 0.03f));
		global::Console.self.hideOldMenu();
		this.startCommand.gameObject.SetActive(true);
		global::Console.self.resetInputField(true);
		yield return new WaitForSeconds(0.1f);
		foreach (GameObject obj in this.menuItems)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.03f));
			obj.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x0008A17C File Offset: 0x0008857C
	public override IEnumerator hideConsole()
	{
		string closeConsole = LineTranslator.translateText("CONSOLE_CLOSE", WordTranslationContainer.Theme.MENU, false, string.Empty);
		yield return base.StartCoroutine(global::Console.self.typeCommand(closeConsole, 0f, 0.03f));
		this.endCommand.GetComponent<Text>().text = closeConsole;
		this.endCommand.SetActive(true);
		global::Console.self.resetInputField(false);
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	// Token: 0x040021CA RID: 8650
	public Text startCommand;

	// Token: 0x040021CB RID: 8651
	public GameObject[] menuItems;

	// Token: 0x040021CC RID: 8652
	public GameObject endCommand;
}
