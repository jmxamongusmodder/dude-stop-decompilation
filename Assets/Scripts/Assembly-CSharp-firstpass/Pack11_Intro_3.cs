using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x02000569 RID: 1385
public class Pack11_Intro_3 : AbstractUIScreen
{
	// Token: 0x06001FEB RID: 8171 RVA: 0x0009A769 File Offset: 0x00098B69
	public override void setScreen(Transform item)
	{
		this.buttonContinue.SetActive(false);
	}

	// Token: 0x06001FEC RID: 8172 RVA: 0x0009A777 File Offset: 0x00098B77
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x0009A779 File Offset: 0x00098B79
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.main());
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x0009A798 File Offset: 0x00098B98
	private IEnumerator main()
	{
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		this.text.setTextToTranslate("PACK11_DUCK3_CONNECTING", WordTranslationContainer.Theme.PUZZLE);
		string wholeText = this.text.currentText;
		string msg = LineTranslator.translateText("DONE", WordTranslationContainer.Theme.MENU, false, string.Empty).ToUpper();
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 6));
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		global::Console.self.showConsole(global::Console.self.pack11_File);
		while (this.chosenFile == -1)
		{
			yield return null;
		}
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		wholeText = wholeText + "\n" + LineTranslator.translateText("PACK11_DUCK3_UPLOADING", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 6));
		msg = LineTranslator.translateText("DONE", WordTranslationContainer.Theme.MENU, false, string.Empty).ToUpper();
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.successColor), msg, "\n\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		yield return new WaitForSeconds(0.5f);
		wholeText += LineTranslator.translateText("PACK11_DUCK3_UPLOADED", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		string text = wholeText;
		wholeText = string.Concat(new string[]
		{
			text,
			" ",
			Global.self.duckSolutionDatabase,
			"/",
			this.fileLinks[this.chosenFile]
		});
		this.text.setTextNoTranslation(wholeText);
		yield return new WaitForSeconds(1f);
		this.buttonContinue.SetActive(true);
		yield break;
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x0009A7B3 File Offset: 0x00098BB3
	public void onFile(int index)
	{
		this.chosenFile = index;
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x0009A7BC File Offset: 0x00098BBC
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

	// Token: 0x0400230D RID: 8973
	public GameObject buttonContinue;

	// Token: 0x0400230E RID: 8974
	public LineTranslator text;

	// Token: 0x0400230F RID: 8975
	public Color successColor;

	// Token: 0x04002310 RID: 8976
	private int chosenFile = -1;

	// Token: 0x04002311 RID: 8977
	[Space(10f)]
	public string[] fileLinks;
}
