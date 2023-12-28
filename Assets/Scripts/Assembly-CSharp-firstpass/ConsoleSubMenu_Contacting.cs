using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x0200052A RID: 1322
public class ConsoleSubMenu_Contacting : ConsoleSubMenuDefault
{
	// Token: 0x06001E62 RID: 7778 RVA: 0x0008A45C File Offset: 0x0008885C
	public override IEnumerator showMenu()
	{
		Global.self.currPuzzle.GetComponent<AudioVoice_loadingScreen>().startVoice(this);
		global::Console.self.hideOldMenu();
		this.contactingLine.gameObject.SetActive(true);
		LineTranslator lt = this.contactingLine.GetComponent<LineTranslator>();
		string txt = lt.currentText;
		int dotCount = 1;
		while (!this.showOptions && !this.skipButtons)
		{
			lt.setTextNoTranslation(txt + new string('.', dotCount));
			if (++dotCount > 3)
			{
				dotCount = 1;
			}
			yield return new WaitForSeconds(this.loadingDotsDelay);
		}
		lt.setTextToTranslate("CONSOLE_DEVELOPER_CONTACTED", WordTranslationContainer.Theme.MENU);
		if (Global.self.firstTimeLoadingGame)
		{
			base.StartCoroutine(this.showMenuAnimation());
		}
		yield break;
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0008A478 File Offset: 0x00088878
	private IEnumerator showMenuAnimation()
	{
		yield return base.StartCoroutine(global::Console.self.typeCommand(this.startCommand.text, 0f, 0.03f));
		this.startCommand.gameObject.SetActive(true);
		global::Console.self.resetInputField(true);
		yield return new WaitForSeconds(0.1f);
		foreach (GameObject obj in this.menuItems)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.089999996f));
			obj.SetActive(true);
		}
		while (!this.showButtons)
		{
			yield return null;
		}
		foreach (GameObject obj2 in this.buttons)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.089999996f));
			obj2.SetActive(true);
		}
		yield break;
	}

	// Token: 0x040021B2 RID: 8626
	public float loadingDotsDelay = 0.5f;

	// Token: 0x040021B3 RID: 8627
	public GameObject contactingLine;

	// Token: 0x040021B4 RID: 8628
	public GameObject[] buttons;

	// Token: 0x040021B5 RID: 8629
	[HideInInspector]
	public bool showOptions;

	// Token: 0x040021B6 RID: 8630
	[HideInInspector]
	public bool showButtons;

	// Token: 0x040021B7 RID: 8631
	[HideInInspector]
	public bool skipButtons;
}
