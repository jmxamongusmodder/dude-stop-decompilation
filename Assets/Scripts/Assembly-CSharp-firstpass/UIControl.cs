using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200058F RID: 1423
public class UIControl : MonoBehaviour
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000A0D77 File Offset: 0x0009F177
	// (set) Token: 0x060020B7 RID: 8375 RVA: 0x000A0DA2 File Offset: 0x0009F1A2
	public static UIControl self
	{
		get
		{
			if (UIControl._self == null)
			{
				UIControl._self = GameObject.FindGameObjectWithTag("UI Control").GetComponent<UIControl>();
			}
			return UIControl._self;
		}
		set
		{
			UIControl._self = value;
		}
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000A0DAA File Offset: 0x0009F1AA
	public void OnValidate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.useBackupFont = this.useBackupFontEditor;
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000A0DC3 File Offset: 0x0009F1C3
	// (set) Token: 0x060020BA RID: 8378 RVA: 0x000A0DCB File Offset: 0x0009F1CB
	public bool useBackupFont
	{
		get
		{
			return this._useBackupFont;
		}
		set
		{
			this._useBackupFont = value;
			this.CheckWhatFontToUse();
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060020BB RID: 8379 RVA: 0x000A0DDA File Offset: 0x0009F1DA
	public float FontSubtitlesBGShiftY
	{
		get
		{
			return (!this.useBackupFont) ? this.subtitlesBgShiftY : this.backupFontSubtitlesBGShiftY;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060020BC RID: 8380 RVA: 0x000A0DF8 File Offset: 0x0009F1F8
	public float BigFontScale
	{
		get
		{
			return (!this.useBackupFont) ? 1f : this.backupBigFontScale;
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060020BD RID: 8381 RVA: 0x000A0E15 File Offset: 0x0009F215
	public float SmallFontScale
	{
		get
		{
			return (!this.useBackupFont) ? 1f : this.backupSmallFontScale;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060020BE RID: 8382 RVA: 0x000A0E32 File Offset: 0x0009F232
	public float OutlineScale
	{
		get
		{
			return (!this.useBackupFont) ? 1f : this.backupOutlineScale;
		}
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000A0E50 File Offset: 0x0009F250
	private void Start()
	{
		this.subtitlesParent.gameObject.SetActive(false);
		this.chatParent.gameObject.SetActive(this.showChat);
		this.completionLine.gameObject.SetActive(false);
		this.timeLineUI.gameObject.SetActive(false);
		this.packsGraph.SetActive(false);
		this.jigsawBotPos = this.jigsawContainer.anchoredPosition.y;
		if (this.subtitlesSizeListName.Length != this.subtitlesSizeList.Length)
		{
			Debug.LogError("Subtitle list with sizes doesn't match size of the subtitle list with names");
		}
		this.subtitlesSizeInd = SaveLoad.getInt("SubtitlesSize", this.subtitlesSizeInd);
		this.setSubtitlesFontSize(this.subtitlesSizeInd);
		this.showSubtitles = (SaveLoad.getInt("Subtitles", 1) == 1);
		this.CheckWhatFontToUse();
		this.subtitlesTextAndBg.x = this.subtitles.transform.position.y;
		this.subtitlesTextAndBg.y = this.subtitlesBG.transform.position.y;
		if (this.showFPS)
		{
			this.fpsCounter.gameObject.SetActive(true);
			base.StartCoroutine(this.countFPS());
		}
		else
		{
			this.fpsCounter.gameObject.SetActive(false);
		}
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000A0FAC File Offset: 0x0009F3AC
	private void CheckWhatFontToUse()
	{
		float fontSubtitlesBGShiftY = this.FontSubtitlesBGShiftY;
		this.subtitles.font = ((!this.useBackupFont) ? this.defaultFontSubtitles : this.backupFont);
		this.SetSubtitlesFontSize();
		RectTransform component = this.subtitlesBG.GetComponent<RectTransform>();
		Vector2 anchoredPosition = component.anchoredPosition;
		anchoredPosition.y = this.subtitles.rectTransform.anchoredPosition.y;
		component.anchoredPosition = anchoredPosition + Vector2.down * (this.subtitlesBgBorder.y + fontSubtitlesBGShiftY);
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x000A1044 File Offset: 0x0009F444
	private void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.C))
		{
			this.showChat = !this.showChat;
			this.chatParent.gameObject.SetActive(this.showChat);
			if (this.showChat)
			{
				UIControl.addNewChatText("Chat is ON now");
			}
		}
		this.updateChat();
		this.updateSubtites();
		if (this.showFPS)
		{
			this.fpsTime += Time.timeScale / Time.deltaTime;
			this.fpsFrames++;
		}
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x000A10E4 File Offset: 0x0009F4E4
	public DuckPopup makePopupDuck(bool secondaryCanvas = false)
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.popupDuck);
		if (secondaryCanvas)
		{
			transform.SetParent(this.secondCanvas.transform, false);
		}
		else
		{
			transform.SetParent(base.transform, false);
		}
		return transform.GetComponent<DuckPopup>();
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x000A1130 File Offset: 0x0009F530
	public IEnumerator killDuckOnTransition(DuckPopup duck)
	{
		while (Global.self.NoCurrentTransition)
		{
			yield return null;
		}
		duck.hideDuck();
		yield break;
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x000A114C File Offset: 0x0009F54C
	private IEnumerator countFPS()
	{
		for (; ; )
		{
			if (this.fpsFrames != 0)
			{
				UIControl.FPS = Mathf.RoundToInt(this.fpsTime / (float)this.fpsFrames);
				this.fpsCounter.text = UIControl.FPS.ToString("F0");
			}
			else
			{
				this.fpsCounter.text = "00";
			}
			this.fpsTime = 0f;
			this.fpsFrames = 0;
			yield return new WaitForSeconds(this.fpsFrequency);
		}
		yield break;
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x000A1167 File Offset: 0x0009F567
	public void stopTimeLine()
	{
		if (!Global.self.packHasTimeLine)
		{
			return;
		}
		this.timeLineUI.stopTime();
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x000A1184 File Offset: 0x0009F584
	public void startTimeLine()
	{
		Global.self.packHasTimeLine = true;
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x000A1191 File Offset: 0x0009F591
	public void endTimeLine()
	{
		if (!Global.self.packHasTimeLine)
		{
			return;
		}
		this.timeLineUI.hideTimeLine();
		Global.self.packHasTimeLine = false;
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000A11B9 File Offset: 0x0009F5B9
	public void startTime(float time)
	{
		if (!Global.self.packHasTimeLine)
		{
			return;
		}
		this.timeLineUI.startTime(time);
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000A11D7 File Offset: 0x0009F5D7
	public void resetTimeLine()
	{
		if (!Global.self.packHasTimeLine)
		{
			return;
		}
		this.timeLineUI.resetLine();
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x000A11F4 File Offset: 0x0009F5F4
	public void startCompletionPack(List<Transform> list)
	{
		this.completionLine.setCompletionUI(list);
		Global.self.packHasCompletionLine = true;
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x000A120D File Offset: 0x0009F60D
	public void setCompletionPackPuzzleState(string name, CompletionState state)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.setIconState(name, state);
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x000A122C File Offset: 0x0009F62C
	public CompletionState getCompletionPackPuzzleState(string name)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return CompletionState.None;
		}
		return this.completionLine.getIconState(name);
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x000A124B File Offset: 0x0009F64B
	public void calculateCompletionProgress()
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.calculateProgress();
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x000A1268 File Offset: 0x0009F668
	public int getCompletionProgress()
	{
		if (!Global.self.packHasCompletionLine)
		{
			return 0;
		}
		return this.completionLine.getProgress();
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x000A1286 File Offset: 0x0009F686
	public List<CompletionState> getCompletionList()
	{
		if (!Global.self.packHasCompletionLine)
		{
			return null;
		}
		return this.completionLine.getProgressList();
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x000A12A4 File Offset: 0x0009F6A4
	public void endCompletionPack()
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.resetAll();
		Global.self.packHasCompletionLine = false;
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x000A12CC File Offset: 0x0009F6CC
	public void showCompletionLine()
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.showCompletionUI();
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x000A12E9 File Offset: 0x0009F6E9
	public void hideCompletionLine(bool remove = false)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.hideCompletionUI(remove);
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000A1307 File Offset: 0x0009F707
	public void moveCompletionIndex(int value = -1)
	{
		if (!Global.self.packHasCompletionLine)
		{
			return;
		}
		this.completionLine.changeIndex(value);
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x000A1328 File Offset: 0x0009F728
	private void updateSubtites()
	{
		if (this.subtitlesTimer > 0f)
		{
			this.subtitlesTimer -= Time.deltaTime;
			this.moveSubtitlesWithInventory();
		}
		else if (this.subtitlesTimer != -1f)
		{
			this.moveSubtitlesWithInventory();
			CanvasGroup component = this.subtitlesParent.GetComponent<CanvasGroup>();
			if (component.alpha > 0f)
			{
				component.alpha -= Time.deltaTime * 4f;
			}
			else
			{
				component.alpha = 0f;
				this.subtitlesTimer = -1f;
			}
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000A13C8 File Offset: 0x0009F7C8
	private void moveSubtitlesWithInventory()
	{
		if (this.subtitlesParent.anchorMin != Vector2.zero || this.subtitilesIgnoreInventory)
		{
			return;
		}
		float num = InventoryControl.self.inventoryHighestSlot();
		if (num == -1f)
		{
			UIControl.self.subtitlesParent.anchoredPosition = this.subtitlesPos;
			return;
		}
		Vector2 anchoredPosition = this.subtitlesPos;
		anchoredPosition.y = Mathf.Clamp(num, 0f, 1f) * 60f;
		UIControl.self.subtitlesParent.anchoredPosition = anchoredPosition;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000A145B File Offset: 0x0009F85B
	public void hideSubtitles()
	{
		if (this.subtitlesTimer != -1f)
		{
			this.subtitlesTimer = 0f;
		}
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x000A1478 File Offset: 0x0009F878
	public static void setSubtitles(bool show = true)
	{
		UIControl.self.showSubtitles = show;
		UIControl.self.subtitlesParent.gameObject.SetActive(show);
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x000A149C File Offset: 0x0009F89C
	public static void setSubtitles(string text)
	{
		if (!UIControl.self.showSubtitles)
		{
			UIControl.self.subtitlesParent.gameObject.SetActive(false);
			return;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "No subtitles found.";
		}
		text = text.Replace("(n)", "\n");
		int num = 0;
		do
		{
			int num2 = num;
			num = text.IndexOf('\n', num + 1);
			if ((num == -1 && text.Length - num2 > 70) || num == -1 || num - num2 > 70)
			{
			}
		}
		while (num != -1);
		UIControl.self.subtitlesParent.gameObject.SetActive(true);
		UIControl.self.subtitlesParent.GetComponent<CanvasGroup>().alpha = 1f;
		UIControl.self.subtitlesTimer = (float)text.Length / 5.1f * UIControl.self.waitPerWord * UIControl.self.subtitlesExtraWaitTime;
		UIControl.self.subtitlesTimer = Mathf.Max(UIControl.self.minSubtitlesTime, UIControl.self.subtitlesTimer);
		if (UIControl.self.subtitlesAreTyping != null)
		{
			UIControl.self.StopCoroutine(UIControl.self.subtitlesAreTyping);
		}
		if (UIControl.self.SetCustomSubtitleText(text))
		{
			return;
		}
		Color? color = UIControl.self.subtitlesNextColor;
		if (color != null)
		{
			Graphic graphic = UIControl.self.subtitles;
			Color? color2 = UIControl.self.subtitlesNextColor;
			graphic.color = color2.Value;
			UIControl.self.subtitlesNextColor = null;
		}
		UIControl.self.subtitles.text = text;
		UIControl.self.sizeSubtitlesBG();
		UIControl.self.moveSubtitlesWithInventory();
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x000A1652 File Offset: 0x0009FA52
	public void SetSubtitlesYellow(bool on)
	{
		if (on)
		{
			this.subtitlesNextColor = new Color?(Color.yellow);
		}
		else
		{
			this.subtitlesNextColor = new Color?(Color.white);
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x000A1680 File Offset: 0x0009FA80
	private bool SetCustomSubtitleText(string text)
	{
		if (text.StartsWith("(type)"))
		{
			text = text.Replace("(type)", string.Empty);
			UIControl.self.subtitles.text = string.Empty;
			UIControl.self.subtitlesAreTyping = UIControl.self.StartCoroutine(UIControl.self.typeSubtitles(text));
			return true;
		}
		if (text.StartsWith("(type:"))
		{
			string text2 = text.Substring(0, text.IndexOf(')', 1) + 1);
			text = text.Replace(text2, string.Empty);
			UIControl.self.subtitles.text = string.Empty;
			text2 = text2.Replace("(type:", string.Empty);
			text2 = text2.Replace(")", string.Empty);
			UIControl.self.subtitlesAreTyping = UIControl.self.StartCoroutine(UIControl.self.typeScreamSubtitles(text, float.Parse(text2)));
			UIControl.self.subtitlesTimer *= 2f;
			return true;
		}
		return false;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x000A178C File Offset: 0x0009FB8C
	private void sizeSubtitlesBG()
	{
		TextGenerationSettings generationSettings = this.subtitles.GetGenerationSettings(new Vector2(1000f, 100f));
		generationSettings.lineSpacing = this.subtitles.lineSpacing;
		float d = 1f / this.secondCanvas.GetComponent<RectTransform>().localScale.x;
		TextGenerator textGenerator = new TextGenerator();
		Vector2 a = new Vector2(textGenerator.GetPreferredWidth(this.subtitles.text, generationSettings), textGenerator.GetPreferredHeight(this.subtitles.text, generationSettings));
		this.subtitlesBG.GetComponent<RectTransform>().sizeDelta = a * d + this.subtitlesBgBorder * 2f;
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x000A1844 File Offset: 0x0009FC44
	private IEnumerator typeSubtitles(string str)
	{
		string txt = string.Empty;
		int index = 0;
		int length = str.Length;
		while (index < length)
		{
			object arg = txt;
			int index2;
			index = (index2 = index) + 1;
			txt = arg + str[index2];
			this.subtitles.text = txt;
			this.sizeSubtitlesBG();
			yield return new WaitForSeconds(0.055f);
		}
		yield break;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x000A1868 File Offset: 0x0009FC68
	private IEnumerator typeScreamSubtitles(string str, float time)
	{
		string txt = string.Empty;
		int index = 0;
		int length = str.Length;
		int size = Mathf.FloorToInt((float)this.subtitles.fontSize - (float)length * 0.5f);
		float delay = time / (float)length;
		while (index < length)
		{
			string str2 = txt;
			string format = "<size={0}>{1}</size>";
			object arg = size;
			int index2;
			index = (index2 = index) + 1;
			txt = str2 + string.Format(format, arg, str[index2]);
			size += 2;
			this.subtitles.text = txt;
			this.sizeSubtitlesBG();
			yield return new WaitForSeconds(delay);
		}
		yield break;
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x000A1894 File Offset: 0x0009FC94
	public void setSubtitlesFontSize(bool previous)
	{
		if (previous)
		{
			this.subtitlesSizeInd--;
		}
		else
		{
			this.subtitlesSizeInd++;
		}
		if (this.subtitlesSizeInd < 0)
		{
			this.subtitlesSizeInd = this.subtitlesSizeList.Length - 1;
		}
		if (this.subtitlesSizeInd >= this.subtitlesSizeList.Length)
		{
			this.subtitlesSizeInd = 0;
		}
		this.SetSubtitlesFontSize();
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x000A1904 File Offset: 0x0009FD04
	public void setSubtitlesFontSize(int fontIndex)
	{
		this.subtitlesSizeInd = fontIndex;
		this.subtitlesSizeInd = Mathf.Clamp(this.subtitlesSizeInd, 0, this.subtitlesSizeList.Length - 1);
		this.SetSubtitlesFontSize();
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x000A1930 File Offset: 0x0009FD30
	private void SetSubtitlesFontSize()
	{
		this.subtitles.fontSize = Mathf.FloorToInt(((float)this.subtitlesDefaultSize + this.subtitlesSizeList[this.subtitlesSizeInd]) * this.SmallFontScale);
		this.subtitles.lineSpacing = ((!UIControl.self.useBackupFont) ? this.subtitlesDefaultLineSpacing : 1f);
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x000A1994 File Offset: 0x0009FD94
	public static void positionSubtitles(Transform puzzle = null)
	{
		if (Global.self.currPuzzleSubtitlesAtBottom(puzzle))
		{
			UIControl.self.subtitlesParent.anchorMin = Vector2.zero;
			UIControl.self.subtitlesParent.anchorMax = Vector2.right;
			UIControl.self.subtitlesParent.pivot = Vector2.right * 0.5f;
			Vector2 anchoredPosition = UIControl.self.subtitlesParent.anchoredPosition;
			anchoredPosition.y = Global.self.currPuzzleSubtitlesYShift(puzzle);
			UIControl.self.subtitlesParent.anchoredPosition = anchoredPosition;
			UIControl.self.subtitlesPos = anchoredPosition;
		}
		else
		{
			UIControl.self.subtitlesParent.anchorMin = Vector2.up;
			UIControl.self.subtitlesParent.anchorMax = Vector2.one;
			UIControl.self.subtitlesParent.pivot = new Vector2(0.5f, 1f);
			Vector2 anchoredPosition2 = UIControl.self.subtitlesParent.anchoredPosition;
			anchoredPosition2.y = -Global.self.currPuzzleSubtitlesYShift(puzzle);
			UIControl.self.subtitlesParent.anchoredPosition = anchoredPosition2;
			UIControl.self.subtitlesPos = anchoredPosition2;
		}
		if (puzzle == null)
		{
			puzzle = Global.self.currPuzzle;
		}
		if (puzzle != null)
		{
			Color color = UIControl.self.subtitlesBG.color;
			color.a = UIControl.self.subtitleBGalpha + puzzle.GetComponent<PuzzleStats>().subtitlesBGalpha;
			UIControl.self.subtitlesBG.color = color;
		}
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x000A1B20 File Offset: 0x0009FF20
	public static void setSubtitlesAlpha(float alpha)
	{
		Color color = UIControl.self.subtitlesBG.color;
		color.a = UIControl.self.subtitleBGalpha + alpha;
		UIControl.self.subtitlesBG.color = color;
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x000A1B60 File Offset: 0x0009FF60
	public static void positionSubtitles(float Y)
	{
		Vector2 anchoredPosition = UIControl.self.subtitlesParent.anchoredPosition;
		anchoredPosition.y = Y;
		UIControl.self.subtitlesParent.anchoredPosition = anchoredPosition;
		UIControl.self.subtitlesPos = anchoredPosition;
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x000A1BA0 File Offset: 0x0009FFA0
	public void showJigSawCounter()
	{
		if (this.jigsawShowingCounter || this.jigsawHiddingCounter)
		{
			return;
		}
		this.jigsawText.text = Global.self.unlockedJigsawPieces - 1 + "/" + 20;
		this.jigsawContainer.gameObject.SetActive(true);
		Vector3 v = this.jigsawContainer.anchoredPosition;
		v.y = this.jigsawTopPos;
		this.jigsawContainer.anchoredPosition = v;
		this.jigsawContainer.localScale = Vector3.one;
		base.StartCoroutine(this.moveJigSaw(true));
		this.jigsawShowingCounter = true;
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x000A1C58 File Offset: 0x000A0058
	private IEnumerator moveJigSaw(bool show = true)
	{
		float timer = 0f;
		Vector3 startPos = this.jigsawContainer.anchoredPosition;
		while (timer < this.jigsawMoveCurve.GetAnimationLength())
		{
			timer = Mathf.MoveTowards(timer, this.jigsawMoveCurve.GetAnimationLength(), Time.deltaTime * this.jigsawMoveSpeed);
			float prog = this.jigsawMoveCurve.Evaluate(timer);
			Vector3 pos = this.jigsawContainer.anchoredPosition;
			if (show)
			{
				pos.y = (startPos.y - this.jigsawBotPos) * prog + this.jigsawBotPos;
			}
			else
			{
				pos.y = (startPos.y - this.jigsawTopPos) * prog + this.jigsawTopPos;
			}
			this.jigsawContainer.anchoredPosition = pos;
			yield return null;
		}
		if (!show)
		{
			this.jigsawContainer.gameObject.SetActive(false);
			this.jigsawShowingCounter = false;
			this.jigsawHiddingCounter = false;
		}
		yield break;
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x000A1C7A File Offset: 0x000A007A
	public void jigSawCollision()
	{
		base.StartCoroutine(this.hitJigSaw());
		this.jigsawHiddingCounter = true;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x000A1C90 File Offset: 0x000A0090
	private IEnumerator hitJigSaw()
	{
		this.jigsawText.text = Global.self.unlockedJigsawPieces + "/" + 20;
		if (this.jigsawHiddingCounter)
		{
			yield break;
		}
		this.jigsawContainer.localScale = this.jigsawMaxScale * Vector3.one;
		while (this.jigsawContainer.localScale.x > 1.01f)
		{
			this.jigsawContainer.localScale = Vector3.MoveTowards(this.jigsawContainer.localScale, Vector3.one, Time.deltaTime * this.jigsawScaleDownSpeed);
			yield return null;
		}
		yield return new WaitForSeconds(this.jigsawShowTime);
		base.StartCoroutine(this.moveJigSaw(false));
		yield break;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x000A1CAC File Offset: 0x000A00AC
	private void updateChat()
	{
		IEnumerator enumerator = this.chatParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				float num = 1f;
				if (this.chatParent.childCount > 15 && transform.GetSiblingIndex() < this.chatParent.childCount - 15)
				{
					num = 100f;
				}
				Text component = transform.GetComponent<Text>();
				Color color = component.color;
				if (color.a > 0.95f)
				{
					color.a -= Time.deltaTime * 0.005f * num;
					component.color = color;
				}
				else
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
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
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x000A1D9C File Offset: 0x000A019C
	public static void addNewChatText(string text, string guid)
	{
		if (!UIControl.self.showChat)
		{
			return;
		}
		guid = guid.Replace("event:", string.Empty);
		guid = guid.Replace("/Sounds", "s");
		if (UIControl.self.lastGuid != string.Empty && UIControl.self.chatParent.childCount > 0 && guid == UIControl.self.lastGuid && UIControl.self.lastMessage.Substring(0, 5) == text.Substring(0, 5))
		{
			UIControl.self.lastGuidCount++;
			Text component = UIControl.self.chatParent.GetChild(UIControl.self.chatParent.childCount - 1).GetComponent<Text>();
			string text2 = " (" + UIControl.self.lastGuidCount.ToString() + ")";
			component.text = string.Concat(new string[]
			{
				text,
				"<color=yellow>",
				guid,
				"</color>",
				text2
			});
			Color color = component.color;
			color.a = 1f;
			component.color = color;
		}
		else
		{
			UIControl.self.lastGuidCount = 1;
			UIControl.addNewChatText(text + "<color=yellow>" + guid + "</color>");
		}
		UIControl.self.lastGuid = guid;
		UIControl.self.lastMessage = text;
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x000A1F24 File Offset: 0x000A0324
	public static void addNewChatText(string text)
	{
		text = text.Replace("event:", string.Empty);
		text = text.Replace("/Sounds", "s");
		Transform original = UIControl.self.chatLine;
		Transform transform = UnityEngine.Object.Instantiate<Transform>(original);
		transform.SetParent(UIControl.self.chatParent);
		transform.localScale = Vector3.one;
		transform.name = "Item";
		Text component = transform.GetComponent<Text>();
		component.text = text;
		Color color = component.color;
		color.a = 1f;
		component.color = color;
		UIControl.self.lastGuid = string.Empty;
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x000A1FC4 File Offset: 0x000A03C4
	public GameObject makeBlackScreen()
	{
		GameObject gameObject = new GameObject();
		Image image = gameObject.AddComponent<Image>();
		image.sprite = this.whiteSprite;
		image.color = Color.black;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(this.secondCanvas.transform);
		component.localScale = Vector3.one * 1.1f;
		component.anchorMax = Vector2.one;
		component.anchorMin = Vector2.zero;
		component.anchoredPosition = Vector2.zero;
		gameObject.transform.SetAsFirstSibling();
		return gameObject;
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x000A2050 File Offset: 0x000A0450
	public void makeNoConnectionScreen(float shiftPos = 0f)
	{
		if (this.noConnectionScreen != null)
		{
			return;
		}
		this.noConnectionScreen = UnityEngine.Object.Instantiate<Transform>(this.noConnectionScreen_Pregfab);
		this.noConnectionScreen.SetParent(base.transform, false);
		this.noConnectionScreen.GetComponent<RectTransform>().anchoredPosition = Vector2.one * shiftPos;
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x000A20AD File Offset: 0x000A04AD
	public void hideNoConnectionScreen()
	{
		if (this.noConnectionScreen != null)
		{
			UnityEngine.Object.Destroy(this.noConnectionScreen.gameObject);
		}
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x000A20D0 File Offset: 0x000A04D0
	public static Transform makeUI(Transform ui, Transform puzzle = null)
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(ui);
		transform.SetParent(UIControl.self.endScreenParent, false);
		transform.GetComponent<AbstractUIScreen>().setScreen(puzzle);
		return transform;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000A2102 File Offset: 0x000A0502
	public static void makeUIScreen(Transform puzzle)
	{
		UIControl.self.makeUIScreenSelf(puzzle);
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000A2110 File Offset: 0x000A0510
	private void makeUIScreenSelf(Transform puzzle)
	{
		PuzzleStats component = puzzle.GetComponent<PuzzleStats>();
		if (component.UIScreenCurr != null)
		{
			return;
		}
		Transform original = this.endScreenList[0];
		if (component.UIScreen)
		{
			original = component.UIScreen;
		}
		Transform transform = UnityEngine.Object.Instantiate<Transform>(original);
		PuzzleStats componentInChildren = transform.GetComponentInChildren<PuzzleStats>();
		if (componentInChildren)
		{
			UnityEngine.Object.Destroy(componentInChildren.gameObject);
		}
		transform.SetParent(this.endScreenParent, false);
		transform.GetComponent<AbstractUIScreen>().setScreen(puzzle);
		component.UIScreenCurr = transform;
		component.positionUIScreen(transform);
	}

	// Token: 0x04002405 RID: 9221
	private static UIControl _self;

	// Token: 0x04002406 RID: 9222
	public Sprite whiteSprite;

	// Token: 0x04002407 RID: 9223
	public GameObject secondCanvas;

	// Token: 0x04002408 RID: 9224
	[Header("Debug Chat")]
	public Transform chatParent;

	// Token: 0x04002409 RID: 9225
	public Transform chatLine;

	// Token: 0x0400240A RID: 9226
	[Tooltip("Show/Hide chat with messages on the screen")]
	public bool showChat = true;

	// Token: 0x0400240B RID: 9227
	private string lastGuid = string.Empty;

	// Token: 0x0400240C RID: 9228
	private string lastMessage = string.Empty;

	// Token: 0x0400240D RID: 9229
	private int lastGuidCount;

	// Token: 0x0400240E RID: 9230
	[Header("Localization fonts")]
	public bool useBackupFontEditor;

	// Token: 0x0400240F RID: 9231
	private bool _useBackupFont;

	// Token: 0x04002410 RID: 9232
	public Font backupFont;

	// Token: 0x04002411 RID: 9233
	public Font defaultFontSubtitles;

	// Token: 0x04002412 RID: 9234
	[SerializeField]
	private float backupFontSubtitlesBGShiftY = -2f;

	// Token: 0x04002413 RID: 9235
	[SerializeField]
	private float backupBigFontScale = 0.75f;

	// Token: 0x04002414 RID: 9236
	[SerializeField]
	private float backupSmallFontScale = 0.65f;

	// Token: 0x04002415 RID: 9237
	[SerializeField]
	private float backupOutlineScale = 0.5f;

	// Token: 0x04002416 RID: 9238
	public float subtitlesExtraWaitTime = 1f;

	// Token: 0x04002417 RID: 9239
	[Space(10f)]
	public GameObject packsGraph;

	// Token: 0x04002418 RID: 9240
	[Header("Subtitles")]
	public bool showSubtitles = true;

	// Token: 0x04002419 RID: 9241
	[Tooltip("List of sizes for the subtitiles")]
	public float[] subtitlesSizeList;

	// Token: 0x0400241A RID: 9242
	[Tooltip("List of names for each size in the previous list")]
	public string[] subtitlesSizeListName;

	// Token: 0x0400241B RID: 9243
	[Tooltip("Index for the default size of the subtitles on the games loading")]
	public int subtitlesSizeInd = 1;

	// Token: 0x0400241C RID: 9244
	public int subtitlesDefaultSize = 28;

	// Token: 0x0400241D RID: 9245
	public float subtitlesDefaultLineSpacing = 0.8f;

	// Token: 0x0400241E RID: 9246
	[Tooltip("Box with subtitles text")]
	public RectTransform subtitlesParent;

	// Token: 0x0400241F RID: 9247
	private Vector2 subtitlesPos;

	// Token: 0x04002420 RID: 9248
	private Vector2 subtitlesTextAndBg;

	// Token: 0x04002421 RID: 9249
	[Tooltip("Subtitles text object")]
	public Text subtitles;

	// Token: 0x04002422 RID: 9250
	[Tooltip("Subtitles BG image")]
	public Image subtitlesBG;

	// Token: 0x04002423 RID: 9251
	[Tooltip("Subtitles BG border size aroung the text")]
	public Vector2 subtitlesBgBorder;

	// Token: 0x04002424 RID: 9252
	[Tooltip("Subtitles BG position shift on Y axes")]
	public float subtitlesBgShiftY = -3f;

	// Token: 0x04002425 RID: 9253
	[Tooltip("How transparent is BG behind subtitles")]
	public float subtitleBGalpha = 0.13f;

	// Token: 0x04002426 RID: 9254
	[Tooltip("How long show each word")]
	public float waitPerWord;

	// Token: 0x04002427 RID: 9255
	[Tooltip("Minimal time to show smallest text")]
	public float minSubtitlesTime;

	// Token: 0x04002428 RID: 9256
	private float subtitlesTimer;

	// Token: 0x04002429 RID: 9257
	private Coroutine subtitlesAreTyping;

	// Token: 0x0400242A RID: 9258
	[HideInInspector]
	public bool subtitilesIgnoreInventory;

	// Token: 0x0400242B RID: 9259
	private Color? subtitlesNextColor;

	// Token: 0x0400242C RID: 9260
	[Header("EndScreen")]
	[Tooltip("Where end screen will be putted")]
	public Transform endScreenParent;

	// Token: 0x0400242D RID: 9261
	public Transform[] endScreenList;

	// Token: 0x0400242E RID: 9262
	[Header("Completion Line")]
	[Tooltip("Line with a progress of the pack")]
	public CompletionStateControl completionLine;

	// Token: 0x0400242F RID: 9263
	[Header("Time Line")]
	[Tooltip("Line with time, in which you need to finish puzzle")]
	public timeLineControl timeLineUI;

	// Token: 0x04002430 RID: 9264
	[Header("JigSaw counter")]
	public RectTransform jigsawContainer;

	// Token: 0x04002431 RID: 9265
	public RectTransform jigsawFlyPoint;

	// Token: 0x04002432 RID: 9266
	public Text jigsawText;

	// Token: 0x04002433 RID: 9267
	public float jigsawMaxScale = 1.2f;

	// Token: 0x04002434 RID: 9268
	public float jigsawScaleDownSpeed = 1f;

	// Token: 0x04002435 RID: 9269
	public AnimationCurve jigsawMoveCurve;

	// Token: 0x04002436 RID: 9270
	public float jigsawMoveSpeed = 1.5f;

	// Token: 0x04002437 RID: 9271
	public float jigsawShowTime = 2f;

	// Token: 0x04002438 RID: 9272
	public float jigsawTopPos = 30f;

	// Token: 0x04002439 RID: 9273
	private float jigsawBotPos;

	// Token: 0x0400243A RID: 9274
	private bool jigsawShowingCounter;

	// Token: 0x0400243B RID: 9275
	private bool jigsawHiddingCounter;

	// Token: 0x0400243C RID: 9276
	[Header("Console")]
	public global::Console console;

	// Token: 0x0400243D RID: 9277
	[Header("Sound Timer")]
	public bool timerEnabled;

	// Token: 0x0400243E RID: 9278
	public UITimer timer;

	// Token: 0x0400243F RID: 9279
	[Header("FPS counter")]
	public bool showFPS = true;

	// Token: 0x04002440 RID: 9280
	public Text fpsCounter;

	// Token: 0x04002441 RID: 9281
	public static int FPS;

	// Token: 0x04002442 RID: 9282
	public float fpsFrequency = 0.5f;

	// Token: 0x04002443 RID: 9283
	private float fpsTime;

	// Token: 0x04002444 RID: 9284
	private int fpsFrames;

	// Token: 0x04002445 RID: 9285
	[Header("Other")]
	public Transform noConnectionScreen_Pregfab;

	// Token: 0x04002446 RID: 9286
	private Transform noConnectionScreen;

	// Token: 0x04002447 RID: 9287
	[Header("Achievement")]
	public AchievementPopup achievementPopup;

	// Token: 0x04002448 RID: 9288
	[Header("DUCK popup")]
	public Transform popupDuck;

	// Token: 0x04002449 RID: 9289
	[Header("NY Coin")]
	public NYCoinUI nyCoin;
}
