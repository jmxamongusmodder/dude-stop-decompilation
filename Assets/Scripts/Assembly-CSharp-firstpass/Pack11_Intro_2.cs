using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x02000568 RID: 1384
public class Pack11_Intro_2 : AbstractUIScreen
{
	// Token: 0x06001FE3 RID: 8163 RVA: 0x0009A2F3 File Offset: 0x000986F3
	public override void setScreen(Transform item)
	{
		this.buttonContinue.SetActive(false);
		this.buttonViewInfo.SetActive(false);
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x0009A30D File Offset: 0x0009870D
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x0009A30F File Offset: 0x0009870F
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.main());
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x0009A32C File Offset: 0x0009872C
	private IEnumerator main()
	{
		Audio.self.playLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb");
		this.text.setTextToTranslate("PACK11_DUCK1_CONNECTING", WordTranslationContainer.Theme.PUZZLE);
		string wholeText = this.text.currentText;
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 6));
		Audio.self.stopLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb", true);
		global::Console.self.showConsole(global::Console.self.pack11_Port);
		this.consoleClosed = false;
		while (!this.consoleClosed)
		{
			yield return null;
		}
		Audio.self.playLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb");
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 5));
		string msg = LineTranslator.translateText("SCROLLABLE_ERROR_MESSAGE_SUCCESS", WordTranslationContainer.Theme.MENU, false, string.Empty);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.successColor), msg, "\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.stopLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb", true);
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		yield return new WaitForSeconds(1f);
		Audio.self.playLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb");
		wholeText += LineTranslator.translateText("PACK11_DUCK2_DOMAIN", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 9));
		msg = LineTranslator.translateText("DONE", WordTranslationContainer.Theme.MENU, false, string.Empty).ToUpper();
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.successColor), msg, "\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.stopLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb", true);
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		this.buttonViewInfo.SetActive(true);
		this.consoleClosed = false;
		while (!this.consoleClosed)
		{
			yield return null;
		}
		this.buttonContinue.SetActive(true);
		yield break;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x0009A347 File Offset: 0x00098747
	public void onConsoleClose()
	{
		this.consoleClosed = true;
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x0009A350 File Offset: 0x00098750
	public void bViewInfo()
	{
		this.buttonViewInfo.SetActive(false);
		global::Console.self.showConsole(global::Console.self.pack11_Info);
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x0009A374 File Offset: 0x00098774
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Transform currPuzzle = Global.self.currPuzzle;
		Global.self.gotoNextLevel(false, null);
		currPuzzle.GetComponent<AudioVoice_Pack11_Intro_1>().onContinue();
	}

	// Token: 0x04002308 RID: 8968
	public GameObject buttonContinue;

	// Token: 0x04002309 RID: 8969
	public GameObject buttonViewInfo;

	// Token: 0x0400230A RID: 8970
	public LineTranslator text;

	// Token: 0x0400230B RID: 8971
	public Color successColor;

	// Token: 0x0400230C RID: 8972
	private bool consoleClosed;
}
