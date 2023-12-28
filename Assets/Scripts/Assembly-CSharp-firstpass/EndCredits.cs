using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000543 RID: 1347
public class EndCredits : AbstractUIScreen
{
	// Token: 0x06001ECC RID: 7884 RVA: 0x00090110 File Offset: 0x0008E510
	public override void setScreen(Transform item)
	{
		if (UIControl.self.useBackupFont)
		{
			this.duckText.font = UIControl.self.backupFont;
			this.duckText.fontSize = Mathf.RoundToInt((float)this.duckText.fontSize * UIControl.self.BigFontScale);
		}
		this.hintText.gameObject.SetActive(false);
		this.scroll.gameObject.SetActive(false);
		this.buttonYes.SetActive(false);
		this.buttonContinue.SetActive(false);
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x000901A2 File Offset: 0x0008E5A2
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x000901A4 File Offset: 0x0008E5A4
	public void showCredits()
	{
		this.scroll.gameObject.SetActive(true);
		base.StartCoroutine(this.scrollCredits());
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x000901C4 File Offset: 0x0008E5C4
	private IEnumerator scrollCredits()
	{
		yield return new WaitForSeconds(1f);
		this.saveEndState();
		yield return new WaitForSeconds(4f);
		Audio.self.playLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa");
		float posY = this.scroll.anchoredPosition.y;
		float dist = this.scroll.sizeDelta.y - UIControl.self.GetComponent<RectTransform>().sizeDelta.y;
		UIControl.self.secondCanvas.GetComponent<GraphicRaycaster>().enabled = true;
		DuckPopup duck = null;
		bool showDuck = false;
		if (!this.DebbugSkipDuck)
		{
			while (posY < dist * (this.startSlow + this.slowLength))
			{
				float sMod = 1f;
				if (posY > dist * this.startSlow)
				{
					sMod = 1f - (posY - dist * this.startSlow) / (dist * this.slowLength);
					sMod = Mathf.Clamp(sMod, 0f, 1f);
					if (this.creditMusicPlaying)
					{
						Audio.self.playLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa", "Pitch", 1f - sMod);
					}
					sMod = Mathf.Clamp(sMod, 0.1f, 1f);
					if (!showDuck)
					{
						showDuck = true;
						GlitchEffectController.self.startGlitch();
						duck = UIControl.self.makePopupDuck(true);
						duck.setDuck(false);
						Audio.self.playLoopSound("84bee965-03d2-4f5f-9807-72f11b41644a");
						this.duckCoroutine = base.StartCoroutine(this.showPopupDuck(duck));
					}
				}
				posY = Mathf.MoveTowards(posY, dist, this.scrollSpeed * sMod * Time.deltaTime);
				this.scroll.anchoredPosition = this.scroll.anchoredPosition.setY(posY);
				yield return null;
			}
			if (this.creditMusicPlaying)
			{
				Audio.self.stopLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa", true);
				this.creditMusicPlaying = false;
			}
			while (this.duckCoroutine != null)
			{
				yield return null;
			}
			Audio.self.playLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa", "Pitch", 0f);
		}
		while (posY < dist)
		{
			posY = Mathf.MoveTowards(posY, dist, this.scrollSpeed * Time.deltaTime);
			this.scroll.anchoredPosition = this.scroll.anchoredPosition.setY(posY);
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		yield return base.StartCoroutine(base.showHintIcon(this.hintText, null));
		while (!Input.GetMouseButtonDown(0) && !Input.GetButtonDown("Cancel") && !Input.GetButtonDown("Submit"))
		{
			yield return null;
		}
		UIControl.self.secondCanvas.GetComponent<GraphicRaycaster>().enabled = false;
		Audio.self.stopLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa", true);
		this.endCredits();
		yield break;
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x000901E0 File Offset: 0x0008E5E0
	private IEnumerator showPopupDuck(DuckPopup duck)
	{
		string txt = LineTranslator.translateText("DUCK_CREDITS_MIGRATION", WordTranslationContainer.Theme.CONSOLE, false, string.Empty);
		yield return base.StartCoroutine(duck.setTextSize(txt + "... 100%"));
		duck.viewLastMsgButton.GetComponent<ButtonTemplate>().setActive(false);
		yield return base.StartCoroutine(duck.setOneLine(txt, new DuckPopupSettings[]
		{
			DuckPopupSettings.ViewLastMsg
		}));
		int prog = 0;
		while (prog < 100)
		{
			prog++;
			duck.setLine(txt + "... " + prog.ToString() + "%");
			yield return new WaitForSeconds(Extensions.Random(this.duckLoadSpeed));
		}
		bool bClick = false;
		duck.subscribeToViewLastMsg(delegate
		{
			bClick = true;
		});
		duck.viewLastMsgButton.GetComponent<ButtonTemplate>().setActive(true);
		while (!bClick)
		{
			yield return null;
		}
		if (this.creditMusicPlaying)
		{
			Audio.self.stopLoopSound("9e805129-3223-4f01-9164-d77fdfef84aa", true);
			this.creditMusicPlaying = false;
		}
		Audio.self.stopLoopSound("84bee965-03d2-4f5f-9807-72f11b41644a", true);
		Audio.self.playOneShot("1e48931c-1869-402c-bfc9-a7b9a3fc2c27", 1f);
		GlitchEffectController.self.stopGlitch();
		duck.speechBubble.gameObject.SetActive(false);
		yield return base.StartCoroutine(this.duckMessages(duck));
		UnityEngine.Object.Destroy(duck.gameObject);
		this.duckCoroutine = null;
		yield break;
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00090204 File Offset: 0x0008E604
	private IEnumerator duckMessages(DuckPopup duck)
	{
		this.duckScreen.SetActive(true);
		Coroutine blink = null;
		yield return base.StartCoroutine(this.typeHelloMsg());
		blink = base.StartCoroutine(this.blinkUnderscore());
		int count = 0;
		while (WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "DUCK_CREDITS_MSG" + count, Global.self.currLanguage) != null)
		{
			count++;
			yield return null;
		}
		yield return base.StartCoroutine(this.waitForButton());
		base.StartCoroutine(this.ChangeSadMusic(0f));
		base.StopCoroutine(blink);
		for (int ind = 0; ind < count; ind++)
		{
			yield return base.StartCoroutine(this.typeText("DUCK_CREDITS_MSG" + ind));
			blink = base.StartCoroutine(this.blinkUnderscore());
			if (ind < count - 1)
			{
				yield return base.StartCoroutine(this.waitForButton());
				base.StartCoroutine(this.ChangeSadMusic((float)ind * 0.1f + 0.1f));
				base.StopCoroutine(blink);
			}
		}
		yield return base.StartCoroutine(this.duckPopupRemove(duck));
		if (blink != null)
		{
			base.StopCoroutine(blink);
		}
		this.duckScreen.SetActive(false);
		yield break;
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00090228 File Offset: 0x0008E628
	private IEnumerator ChangeSadMusic(float from)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			Audio.self.playLoopSound("d93655ee-5ef5-4c52-9d4b-3ca89111a3c9", "CreditsIndex", from + t * 0.1f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00090244 File Offset: 0x0008E644
	private IEnumerator duckPopupRemove(DuckPopup duck)
	{
		duck.transform.SetParent(UIControl.self.transform, false);
		string txt = LineTranslator.translateText("DUCK_CREDITS_UPLOADING", WordTranslationContainer.Theme.CONSOLE, false, string.Empty);
		string txt2 = LineTranslator.translateText("DUCK_CREDITS_REMOVE", WordTranslationContainer.Theme.CONSOLE, false, string.Empty);
		yield return base.StartCoroutine(duck.setTextSize(txt + "... 100%\n" + txt2));
		yield return base.StartCoroutine(duck.setOneLine(txt, new DuckPopupSettings[0]));
		int prog = 0;
		while (prog < 100)
		{
			prog++;
			duck.setLine(txt + "... " + prog.ToString() + "%");
			yield return new WaitForSeconds(Extensions.Random(this.duckLoadSpeed));
		}
		yield return new WaitForSeconds(0.2f);
		yield return base.StartCoroutine(duck.addNewLine(txt2, new DuckPopupSettings[]
		{
			DuckPopupSettings.Yes
		}));
		bool bClick = false;
		duck.subscribeToYes(delegate
		{
			bClick = true;
		});
		while (!bClick)
		{
			yield return null;
		}
		Audio.self.stopLoopSound("d93655ee-5ef5-4c52-9d4b-3ca89111a3c9", true);
		Audio.self.playOneShot("72f585c7-62da-4e3d-901e-7d074a432704", 1f);
		duck.yesButton.GetComponent<ButtonTemplate>().setActive(false);
		GlitchEffectController.self.startGlitch(4f, 2f);
		yield return new WaitForSeconds(4f);
		GameObject blackScr = UIControl.self.makeBlackScreen();
		yield return new WaitForSeconds(4f);
		GlitchEffectController.self.startGlitch(0.5f);
		UnityEngine.Object.Destroy(blackScr);
		yield break;
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x00090268 File Offset: 0x0008E668
	private IEnumerator typeHelloMsg()
	{
		yield return base.StartCoroutine(this.typeText("DUCK_CREDITS_HELLO1"));
		string hello = this.duckText.text;
		Coroutine blink = base.StartCoroutine(this.blinkUnderscore());
		yield return new WaitForSeconds(0.3f);
		base.StopCoroutine(blink);
		string hello2 = LineTranslator.translateText("DUCK_CREDITS_HELLO2", WordTranslationContainer.Theme.CONSOLE, false, string.Empty);
		int removeTill = 0;
		for (int i = 0; i < hello.Length; i++)
		{
			if (i > hello.Length || i > hello2.Length)
			{
				break;
			}
			if (hello[i] != hello2[i])
			{
				break;
			}
			removeTill = i;
		}
		int ind = hello.Length;
		while (ind > removeTill)
		{
			Text text = this.duckText;
			string text2 = hello;
			int startIndex = 0;
			int length;
			ind = (length = ind) - 1;
			text.text = text2.Substring(startIndex, length) + "_";
			Audio.self.playOneShot("a49cc8b0-8271-44ad-9e2b-13d1a0502404", 1f);
			yield return new WaitForSeconds(Extensions.Random(this.typeSpeed));
		}
		if (removeTill == 0)
		{
			this.duckText.text = string.Empty;
			yield return new WaitForSeconds(Extensions.Random(this.typeSpeed));
		}
		while (ind < hello2.Length)
		{
			this.duckText.text = hello2.Substring(0, ++ind) + "_";
			Audio.self.playOneShot("163d61e6-625f-4385-a93e-f43cd5f157ba", 1f);
			yield return new WaitForSeconds(Extensions.Random(this.typeSpeed));
		}
		this.duckText.text = hello2;
		yield break;
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x00090284 File Offset: 0x0008E684
	private IEnumerator typeText(string text)
	{
		text = LineTranslator.translateText(text, WordTranslationContainer.Theme.CONSOLE, false, string.Empty);
		Queue<string> wait = new Queue<string>();
		Regex reg = new Regex("\\(w(\\d+(?:\\.\\d+)?)\\)");
		MatchCollection matches = reg.Matches(text);
		IEnumerator enumerator = matches.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Match match = (Match)obj;
				int num = text.IndexOf(match.Value);
				wait.Enqueue(num + "," + match.Groups[1].Value);
				text = text.Remove(num, match.Value.Length);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.duckText.text = string.Format("_<color=#00000000>{0}</color>", text);
		yield return new WaitForSeconds(this.blinkSpeed.y);
		this.duckText.text = string.Format("<color=#00000000>_{0}</color>", text);
		yield return new WaitForSeconds(this.blinkSpeed.x * 0.5f);
		int ind = 0;
		int wPos = -1;
		float wTime = -1f;
		int totalLenShift = 0;
		while (ind < text.Length)
		{
			if (text[ind] == '\n')
			{
				string text2 = "<color=#00000000>_</color>";
				text = text.Insert(ind, text2);
				ind += text2.Length;
				totalLenShift += text2.Length;
			}
			ind++;
			this.duckText.text = string.Format("{0}_<color=#00000000>{1}</color>", text.Substring(0, ind), text.Substring(ind));
			Audio.self.playOneShot("163d61e6-625f-4385-a93e-f43cd5f157ba", 1f);
			if (wPos == -1 && wait.Count != 0)
			{
				string[] array = wait.Dequeue().Split(new char[]
				{
					','
				});
				wPos = int.Parse(array[0]);
				wTime = float.Parse(array[1]);
			}
			if (ind == wPos + totalLenShift)
			{
				yield return new WaitForSeconds(wTime);
				wPos = -1;
			}
			else
			{
				yield return new WaitForSeconds(Extensions.Random(this.typeSpeed));
			}
		}
		this.duckText.text = text;
		yield break;
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x000902A8 File Offset: 0x0008E6A8
	private IEnumerator blinkUnderscore()
	{
		string txt = this.duckText.text;
		this.duckText.text = txt + "_";
		yield return new WaitForSeconds(this.blinkSpeed.x);
		for (;;)
		{
			this.duckText.text = txt + "<color=#00000000>_</color>";
			yield return new WaitForSeconds(this.blinkSpeed.x);
			this.duckText.text = txt + "_";
			yield return new WaitForSeconds(this.blinkSpeed.y);
		}
		yield break;
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x000902C4 File Offset: 0x0008E6C4
	private IEnumerator waitForButton()
	{
		yield return new WaitForSeconds(1f);
		this.buttonContinue.SetActive(true);
		this.duckButtonClicked = false;
		while (!this.duckButtonClicked)
		{
			yield return null;
		}
		this.buttonContinue.SetActive(false);
		yield break;
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x000902DF File Offset: 0x0008E6DF
	public void bContinue()
	{
		this.duckButtonClicked = true;
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x000902E8 File Offset: 0x0008E6E8
	public void bYes()
	{
		this.duckButtonClicked = true;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x000902F1 File Offset: 0x0008E6F1
	private void saveEndState()
	{
		SerializableGameStats.self.isGameFinished = true;
		SerializableGameStats.self.isGameFinishedJustNow = true;
		Global.self.Save();
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x00090313 File Offset: 0x0008E713
	private void endCredits()
	{
		Global.self.canExitEndScreen = true;
		global::Console.self.canOpen = true;
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
	}

	// Token: 0x040021FB RID: 8699
	public RectTransform hintText;

	// Token: 0x040021FC RID: 8700
	public RectTransform scroll;

	// Token: 0x040021FD RID: 8701
	public float scrollSpeed = 1f;

	// Token: 0x040021FE RID: 8702
	public Vector2 duckLoadSpeed;

	// Token: 0x040021FF RID: 8703
	[Header("Duck")]
	public bool DebbugSkipDuck;

	// Token: 0x04002200 RID: 8704
	public GameObject duckScreen;

	// Token: 0x04002201 RID: 8705
	public GameObject buttonContinue;

	// Token: 0x04002202 RID: 8706
	public GameObject buttonYes;

	// Token: 0x04002203 RID: 8707
	public Text duckText;

	// Token: 0x04002204 RID: 8708
	public Vector2 typeSpeed;

	// Token: 0x04002205 RID: 8709
	public Vector2 blinkSpeed;

	// Token: 0x04002206 RID: 8710
	public float startSlow = 0.41f;

	// Token: 0x04002207 RID: 8711
	public float slowLength = 0.05f;

	// Token: 0x04002208 RID: 8712
	private bool duckButtonClicked;

	// Token: 0x04002209 RID: 8713
	private Coroutine duckCoroutine;

	// Token: 0x0400220A RID: 8714
	private bool creditMusicPlaying = true;
}
