using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x0200056A RID: 1386
public class Pack12_Duck : AbstractUIScreen
{
	// Token: 0x06001FF2 RID: 8178 RVA: 0x0009AB54 File Offset: 0x00098F54
	public override void setScreen(Transform item)
	{
		this.buttonContinue.SetActive(false);
		this.buttonYes.setActive(false);
		this.buttonNo.setActive(false);
		this.buttonYes.gameObject.SetActive(false);
		this.buttonNo.gameObject.SetActive(false);
		this.voice = item.GetComponent<AudioVoice_Pack12_Duck>();
		if (SerializablePuzzleStats.Get(item.name).loadedTimes > 0)
		{
			this.secondTime = true;
		}
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x0009ABD0 File Offset: 0x00098FD0
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x0009ABD4 File Offset: 0x00098FD4
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		Global.self.endCutscenePack12 = false;
		global::Console.self.canOpen = false;
		if (!this.secondTime)
		{
			base.StartCoroutine(this.main());
		}
		else
		{
			base.StartCoroutine(this.secondMain());
		}
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x0009AC30 File Offset: 0x00099030
	private IEnumerator main()
	{
		this.text.setTextNoTranslation(string.Empty);
		string done = LineTranslator.translateText("DONE", WordTranslationContainer.Theme.MENU, false, string.Empty);
		float time = 0f;
		int index = 0;
		string wholeText = string.Empty;
		this.voice.playFirstLine();
		yield return new WaitForSeconds(0.5f);
		this.text.setTextToTranslate("CONSOLE_LOADING", WordTranslationContainer.Theme.MENU);
		wholeText = this.text.currentText;
		Coroutine dots = base.StartCoroutine(Pack12_Duck.addDots(this.text, wholeText));
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		while (!this.voice.mailList)
		{
			yield return null;
		}
		base.StopCoroutine(dots);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.doneColor), done, "\n");
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		wholeText += LineTranslator.translateText("PACK12_DUCK_START", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		dots = base.StartCoroutine(Pack12_Duck.addDots(this.text, wholeText));
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		while (!this.voice.contacts)
		{
			yield return null;
		}
		global::Console.self.showConsole(global::Console.self.pack12_MailList);
		this.consoleClosed = false;
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		time = 5f;
		index = 0;
		while (!this.consoleClosed)
		{
			if (this.voice.isPlaying())
			{
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				time -= Time.deltaTime;
				if (time < 0f)
				{
					AudioVoice_Pack12_Duck audioVoice_Pack12_Duck = this.voice;
					int ind;
					index = (ind = index) + 1;
					audioVoice_Pack12_Duck.playWaitingLine(ind);
					time = 10f;
				}
				yield return null;
			}
		}
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		this.voice.stopVoice();
		base.StopCoroutine(dots);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		wholeText = LineTranslator.translateText("PACK12_DUCK_SHARE_DATA", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		dots = base.StartCoroutine(Pack12_Duck.addDots(this.text, wholeText));
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		yield return new WaitForSeconds(1f);
		this.voice.playShareLine();
		UIControl.positionSubtitles(80f);
		this.buttonYes.gameObject.SetActive(true);
		this.buttonNo.gameObject.SetActive(true);
		while (!this.voice.allowClick)
		{
			yield return null;
		}
		base.StopCoroutine(dots);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.doneColor), done, "\n");
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		this.text.setTextNoTranslation(wholeText);
		this.buttonYes.setActive(true);
		this.buttonNo.setActive(true);
		this.buttonYesClicked = null;
		time = 5f;
		index = 0;
		for (;;)
		{
			bool? flag = this.buttonYesClicked;
			if (flag != null)
			{
				break;
			}
			if (this.voice.isPlaying())
			{
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				time -= Time.deltaTime;
				if (time < 0f)
				{
					AudioVoice_Pack12_Duck audioVoice_Pack12_Duck2 = this.voice;
					int ind;
					index = (ind = index) + 1;
					audioVoice_Pack12_Duck2.playWaitLineYesNo(ind);
					time = 7f;
				}
				yield return null;
			}
		}
		base.StartCoroutine(this.buttonClicked());
		yield break;
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x0009AC4C File Offset: 0x0009904C
	private IEnumerator secondMain()
	{
		yield return new WaitForSeconds(0.5f);
		string done = LineTranslator.translateText("DONE", WordTranslationContainer.Theme.MENU, false, string.Empty);
		this.text.setTextToTranslate("PACK12_DUCK_LOADING", WordTranslationContainer.Theme.PUZZLE);
		string wholeText = this.text.currentText;
		Coroutine dots = base.StartCoroutine(Pack12_Duck.addDots(this.text, wholeText));
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		yield return new WaitForSeconds(1f);
		base.StopCoroutine(dots);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.doneColor), done, "\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		yield return new WaitForSeconds(0.5f);
		wholeText += LineTranslator.translateText("PACK12_DUCK_SHARE_DATA", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		dots = base.StartCoroutine(Pack12_Duck.addDots(this.text, wholeText));
		Audio.self.playLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7");
		UIControl.positionSubtitles(80f);
		this.buttonYes.gameObject.SetActive(true);
		this.buttonNo.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.voice.playSecondStart();
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		base.StopCoroutine(dots);
		Audio.self.stopLoopSound("83ba9ada-e6af-40d0-a589-58bd7a8253a7", true);
		wholeText += string.Format("... <color=#{0}>{1}</color>{2}", ColorUtility.ToHtmlStringRGBA(this.doneColor), done, "\n");
		this.text.setTextNoTranslation(wholeText);
		Audio.self.playOneShot("2576ebc5-df0f-4931-94c0-47ac5a053d57", 1f);
		this.buttonYes.setActive(true);
		this.buttonNo.setActive(true);
		this.buttonYesClicked = null;
		for (;;)
		{
			bool? flag = this.buttonYesClicked;
			if (flag != null)
			{
				break;
			}
			yield return null;
		}
		base.StartCoroutine(this.buttonClicked());
		yield break;
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x0009AC68 File Offset: 0x00099068
	private IEnumerator buttonClicked()
	{
		UIControl.positionSubtitles(30f);
		this.buttonYes.gameObject.SetActive(false);
		this.buttonNo.gameObject.SetActive(false);
		if (this.buttonYesClicked == true)
		{
			base.StartCoroutine(this.yesClicked());
		}
		else
		{
			this.voice.playLastLine(false);
			this.text.setTextToTranslate("PACK12_DUCK_REMEMBER", WordTranslationContainer.Theme.PUZZLE);
			GlitchEffectController.self.startGlitch(0.5f);
			while (!this.voice.showContinue)
			{
				yield return null;
			}
			base.StartCoroutine(this.endCoroutine());
		}
		yield break;
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x0009AC84 File Offset: 0x00099084
	private IEnumerator yesClicked()
	{
		Global.self.endCutscenePack12 = true;
		this.voice.playLastLine(true);
		string wholeText = LineTranslator.translateText("PACK12_DUCK_SEND", WordTranslationContainer.Theme.PUZZLE, false, string.Empty);
		int index = 0;
		while (index <= 7)
		{
			if (index > 0)
			{
				Audio.self.playOneShot("70752361-3b02-49bc-aef5-74138ce65a32", 1f);
			}
			LineTranslator lineTranslator = this.text;
			string format = "{3}... <color=#{0}>{1}/{2}</color>";
			object[] array = new object[4];
			array[0] = ColorUtility.ToHtmlStringRGBA(this.doneColor);
			int num = 1;
			int num2;
			index = (num2 = index) + 1;
			array[num] = num2;
			array[2] = 7;
			array[3] = wholeText;
			lineTranslator.setTextNoTranslation(string.Format(format, array));
			yield return new WaitForSeconds(2f + UnityEngine.Random.value);
		}
		while (!this.voice.showContinue)
		{
			yield return null;
		}
		base.StartCoroutine(this.endCoroutine());
		yield break;
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x0009ACA0 File Offset: 0x000990A0
	private IEnumerator endCoroutine()
	{
		yield return new WaitForSeconds(0.5f);
		UIControl.positionSubtitles(60f);
		this.buttonContinue.SetActive(true);
		yield break;
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x0009ACBC File Offset: 0x000990BC
	private IEnumerator playWaitingLines()
	{
		yield return null;
		yield break;
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x0009ACD0 File Offset: 0x000990D0
	public static IEnumerator addDots(LineTranslator text, string txt)
	{
		float wait = 0.8f;
		int dots = 0;
		for (;;)
		{
			text.setTextNoTranslation(txt + new string('.', dots));
			dots++;
			if (dots > 3)
			{
				dots = 0;
			}
			yield return new WaitForSeconds(wait);
		}
		yield break;
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x0009ACF2 File Offset: 0x000990F2
	public void onConsoleClose()
	{
		this.consoleClosed = true;
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x0009ACFC File Offset: 0x000990FC
	public void bContintue()
	{
		if (!this.active)
		{
			return;
		}
		if (this.buttonYesClicked == false)
		{
			global::Console.self.canOpen = true;
		}
		this.voice.bContinue();
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x0009AD5F File Offset: 0x0009915F
	public void bYes()
	{
		this.buttonYesClicked = new bool?(true);
		AnalyticsComponent.DuckEndButtonYes();
		Audio.self.StartMusic("f24b6c30-feac-4181-b42d-3c1f8242383d");
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x0009AD81 File Offset: 0x00099181
	public void bNo()
	{
		this.buttonYesClicked = new bool?(false);
		AnalyticsComponent.DuckEndButtonNo();
		Audio.self.playOneShot("533bb9ef-c5b2-4b63-849c-b6619af399e0", 1f);
	}

	// Token: 0x04002312 RID: 8978
	private AudioVoice_Pack12_Duck voice;

	// Token: 0x04002313 RID: 8979
	public GameObject buttonContinue;

	// Token: 0x04002314 RID: 8980
	public ButtonTemplate buttonYes;

	// Token: 0x04002315 RID: 8981
	public ButtonTemplate buttonNo;

	// Token: 0x04002316 RID: 8982
	public LineTranslator text;

	// Token: 0x04002317 RID: 8983
	public Color doneColor;

	// Token: 0x04002318 RID: 8984
	private bool consoleClosed = true;

	// Token: 0x04002319 RID: 8985
	private bool? buttonYesClicked;

	// Token: 0x0400231A RID: 8986
	private bool secondTime;
}
