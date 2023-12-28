using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x02000567 RID: 1383
public class Pack11_Intro_1 : AbstractUIScreen
{
	// Token: 0x06001FDB RID: 8155 RVA: 0x00099C66 File Offset: 0x00098066
	public override void setScreen(Transform item)
	{
		this.buttonCancel.SetActive(false);
		this.buttonRetry.SetActive(false);
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x00099C80 File Offset: 0x00098080
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x00099C82 File Offset: 0x00098082
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.main());
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x00099CA0 File Offset: 0x000980A0
	private IEnumerator main()
	{
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		this.text.setTextToTranslate("PACK11_DUCK1_SAVING", WordTranslationContainer.Theme.PUZZLE);
		string wholeText = this.text.currentText;
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 6));
		string error = LineTranslator.translateText("ERROR", WordTranslationContainer.Theme.MENU, false, string.Empty);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.errorColor), error, "\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		Audio.self.playOneShot("09946ad3-95e0-42fe-b8c5-eae12ef4b5ee", 1f);
		yield return new WaitForSeconds(1f);
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		wholeText += LineTranslator.translateText("PACK11_DUCK1_UPLOADING", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, wholeText, 6));
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.errorColor), error, "\n\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		Audio.self.playOneShot("09946ad3-95e0-42fe-b8c5-eae12ef4b5ee", 1f);
		yield return new WaitForSeconds(1f);
		wholeText += LineTranslator.translateText("PACK11_DUCK1_CONNECTING", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		string prevText = wholeText;
		for (int i = 0; i < 4; i++)
		{
			Audio.self.playLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb");
			yield return base.StartCoroutine(Pack11_Intro_1.addDots(this.text, prevText, 2));
			wholeText = string.Format("{0}... <color=#{1}>{2}</color>", prevText, ColorUtility.ToHtmlStringRGBA(this.errorColor), error);
			Audio.self.stopLoopSound("5034a287-658a-4aaa-9cfc-e6ad57e8cdfb", true);
			Audio.self.playOneShot("09946ad3-95e0-42fe-b8c5-eae12ef4b5ee", 1f);
			this.text.setTextNoTranslation(wholeText);
			if (i >= 3)
			{
				break;
			}
			yield return new WaitForSeconds(0.5f);
			this.buttonRetry.SetActive(true);
			this.retryPressed = false;
			while (!this.retryPressed)
			{
				yield return null;
			}
			this.buttonRetry.SetActive(false);
		}
		wholeText = wholeText + "\n" + LineTranslator.translateText("PACK11_DUCK1_REACHED", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		this.text.setTextNoTranslation(wholeText);
		yield return new WaitForSeconds(0.5f);
		this.buttonCancel.SetActive(true);
		yield break;
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x00099CBC File Offset: 0x000980BC
	public static IEnumerator addDots(LineTranslator text, string txt, int times)
	{
		float wait = 0.8f;
		int dots = 0;
		while ((float)times >= 0f)
		{
			if (++dots > 3)
			{
				dots = 0;
			}
			text.setTextNoTranslation(txt + new string('.', dots));
			times--;
			yield return new WaitForSeconds(wait);
		}
		yield break;
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x00099CE8 File Offset: 0x000980E8
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

	// Token: 0x06001FE1 RID: 8161 RVA: 0x00099D2B File Offset: 0x0009812B
	public void bRetry()
	{
		this.retryPressed = true;
	}

	// Token: 0x04002303 RID: 8963
	public GameObject buttonCancel;

	// Token: 0x04002304 RID: 8964
	public GameObject buttonRetry;

	// Token: 0x04002305 RID: 8965
	private bool retryPressed;

	// Token: 0x04002306 RID: 8966
	public LineTranslator text;

	// Token: 0x04002307 RID: 8967
	public Color errorColor;
}
