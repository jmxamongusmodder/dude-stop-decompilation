using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200052C RID: 1324
public class ConsoleSubMenu_CupDuckBlueButton : ConsoleSubMenu
{
	// Token: 0x06001E69 RID: 7785 RVA: 0x0008B3C8 File Offset: 0x000897C8
	public override void setMenu()
	{
		ButtonTemplate[] componentsInChildren = base.GetComponentsInChildren<ButtonTemplate>(true);
		foreach (ButtonTemplate buttonTemplate in componentsInChildren)
		{
			buttonTemplate.callbackMouseOn = new Action<RectTransform>(global::Console.self.mouseOver);
		}
		base.setMenu();
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0008B414 File Offset: 0x00089814
	public override IEnumerator showMenu()
	{
		global::Console.self.hideOldMenu();
		foreach (GameObject obj in this.firstList)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.03f));
			obj.SetActive(true);
		}
		AudioVoice_CupDuck voice = Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>();
		while (!voice.showBlueButton)
		{
			yield return null;
		}
		foreach (GameObject obj2 in this.secondList)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.03f));
			obj2.SetActive(true);
		}
		foreach (LineTranslator line in this.buttons)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.03f));
			line.translateText(false);
			line.scriptControlled = true;
			line.gameObject.SetActive(true);
			string text = line.currentText;
			for (int i = 0; i < text.Length + 1; i++)
			{
				line.setTextNoTranslation(text.Substring(0, i));
				yield return new WaitForSeconds(0.5f / (float)(i + 1));
			}
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(0.5f);
		this.canClick = true;
		yield break;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0008B42F File Offset: 0x0008982F
	public void bRedBlue(bool red)
	{
		if (!this.canClick)
		{
			return;
		}
		AnalyticsComponent.DuckRebBlueButtonClick(red);
		Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>().clickColorButton();
	}

	// Token: 0x040021BB RID: 8635
	public GameObject[] firstList;

	// Token: 0x040021BC RID: 8636
	public GameObject[] secondList;

	// Token: 0x040021BD RID: 8637
	public LineTranslator[] buttons;

	// Token: 0x040021BE RID: 8638
	private bool canClick;
}
