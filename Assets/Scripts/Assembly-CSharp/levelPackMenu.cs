using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000558 RID: 1368
public class levelPackMenu : AbstractUIScreen
{
	// Token: 0x06001F60 RID: 8032 RVA: 0x00096100 File Offset: 0x00094500
	public override void Update()
	{
		base.Update();
		if (this.changeAlpha)
		{
			this.awardGroup.alpha = Mathf.Lerp(this.awardGroup.alpha, 1.1f, Time.deltaTime * 5f);
			if (this.awardGroup.alpha >= 1f)
			{
				this.changeAlpha = false;
				this.awardGroup.alpha = 1f;
			}
		}
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x00096178 File Offset: 0x00094578
	public void mouseOverJigSawUI()
	{
		if (!this.active)
		{
			return;
		}
		if (this.awardText.text == string.Empty || this.awardSelected != this.jigSawIconParent.GetInstanceID())
		{
			this.setAwardTextAlpha();
		}
		string text = LineTranslator.translateText("JIGSAW_INFO", WordTranslationContainer.Theme.PACK_MENU, false, string.Empty);
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		text = TextFormating.formatAcquiredAward(array[0], string.Join("\n", array, 1, array.Length - 1), false, false, false, false);
		this.awardText.text = TextFormating.format(text);
		this.awardSelected = this.jigSawIconParent.GetInstanceID();
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0009622B File Offset: 0x0009462B
	public void breakStartButton(bool on)
	{
		this.startButtonIsBroken = on;
		this.startButton.GetComponent<ButtonTemplate>().setActive(!on);
		if (on)
		{
			base.StartCoroutine(this.animateStartButton());
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x0009625C File Offset: 0x0009465C
	private IEnumerator animateStartButton()
	{
		LineTranslator lt = this.startButton.GetComponent<LineTranslator>();
		string correctText = lt.components[0].text;
		this.startButton.GetComponent<Button>().interactable = false;
		while (this.startButtonIsBroken)
		{
			string from = correctText;
			string to = string.Empty;
			while (from.Length > 0)
			{
				to = to.Insert(UnityEngine.Random.Range(0, to.Length + 1), from[0].ToString());
				if (UnityEngine.Random.value > 0.9f)
				{
					to = to.Insert(UnityEngine.Random.Range(0, to.Length), " ");
				}
				from = from.Remove(0, 1);
			}
			if (UnityEngine.Random.value > 0.5f)
			{
				this.startButton.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(-5, 5));
			}
			else
			{
				this.startButton.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
			lt.setTextNoTranslation(to);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
		}
		lt.translateText(false);
		this.startButton.GetComponent<Button>().interactable = true;
		yield break;
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x00096277 File Offset: 0x00094677
	public void setAwardTextAlpha()
	{
		this.changeAlpha = true;
		this.awardGroup.alpha = 0f;
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x00096290 File Offset: 0x00094690
	private void makePackScreenUI()
	{
		if (Global.self.currentPackSelectUI != null)
		{
			return;
		}
		Global.self.currentPackSelectUI = UIControl.makeUI(Global.self.packSelectUI, this.puzzle);
		this.ps.UIScreenSecondary = Global.self.currentPackSelectUI;
		this.ps.positionUIScreen(this.ps.UIScreenSecondary);
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x000962FD File Offset: 0x000946FD
	private void attachPackScreenUI()
	{
		if (Global.self.currentPackSelectUI == null)
		{
			return;
		}
		this.ps.UIScreenSecondary = Global.self.currentPackSelectUI;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x0009632A File Offset: 0x0009472A
	protected override void cancelPressed()
	{
		if (Global.self.currentAwardAnimCount > 0)
		{
			return;
		}
		this.bBack();
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x00096344 File Offset: 0x00094744
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
		this.ps = this.puzzle.GetComponent<PuzzleStats>();
		if (UIControl.self.useBackupFont)
		{
			this.awardText.font = UIControl.self.backupFont;
		}
		this.levelControl = item.GetComponent<levelPackControl>();
		this.header.text = this.levelControl.translateHeader(false);
		this.header.color = this.levelControl.headerColor;
		this.makePackScreenUI();
		this.setJigSawIcons();
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x000963D2 File Offset: 0x000947D2
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (Global.self.currentPackSelectUI != null)
		{
			Global.self.currentPackSelectUI.GetComponent<AbstractUIScreen>().setActive(active);
		}
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x00096408 File Offset: 0x00094808
	private void setJigSawIcons()
	{
		levelPackControl component = this.puzzle.GetComponent<levelPackControl>();
		int num = component.awardControllerScript.jigsawPerPackCount;
		int i = this.jigSawIconParent.childCount;
		int jigSawPiecesFound = SerializablePackSavedStats.Get(this.puzzle.name).jigSawPiecesFound;
		int num2 = 20 - Global.self.unlockedJigsawPieces;
		if (num > num2)
		{
			num = Mathf.Min(num, jigSawPiecesFound + num2);
		}
		if (num == 0)
		{
			this.jigSawIconParent.gameObject.SetActive(false);
			return;
		}
		while (i > num)
		{
			i--;
			UnityEngine.Object.Destroy(this.jigSawIconParent.GetChild(i).gameObject);
		}
		while (i < num)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.jigSawIconParent.GetChild(0));
			transform.SetParent(this.jigSawIconParent);
			i++;
		}
		Vector2 sizeDelta = this.jigSawIconParent.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.x = (float)i * this.jigSawIconWidth;
		this.jigSawIconParent.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		for (int j = 0; j < i; j++)
		{
			Transform child = this.jigSawIconParent.GetChild(j);
			if (j < jigSawPiecesFound)
			{
				child.GetChild(0).GetComponent<Image>().color = component.headerColor;
			}
			else
			{
				child.GetChild(0).gameObject.SetActive(false);
				Color color = child.GetComponent<Image>().color;
				color.a = this.jigSawEmptyAlpha;
				child.GetComponent<Image>().color = color;
			}
		}
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x00096599 File Offset: 0x00094999
	public void bReplay()
	{
		if (!this.active)
		{
			return;
		}
		this.attachPackScreenUI();
		Global.self.resetLevelList();
		this.replayCallback();
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x000965C2 File Offset: 0x000949C2
	public void bStart()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.ps.isClickAllowed(ClickWhileVoice.start))
		{
			return;
		}
		if (Global.self.unlockNextPack)
		{
			return;
		}
		this.startPack();
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x000965F8 File Offset: 0x000949F8
	public void startPack()
	{
		this.attachPackScreenUI();
		this.levelControl.startPack();
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x0009660C File Offset: 0x00094A0C
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.ps.isClickAllowed(ClickWhileVoice.back))
		{
			return;
		}
		this.attachPackScreenUI();
		Global.self.lastPackCompletionState = CompletionState.None;
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x00096662 File Offset: 0x00094A62
	public void GoBackFromPack13()
	{
		this.attachPackScreenUI();
		Global.self.lastPackCompletionState = CompletionState.None;
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
	}

	// Token: 0x04002297 RID: 8855
	private Transform puzzle;

	// Token: 0x04002298 RID: 8856
	private PuzzleStats ps;

	// Token: 0x04002299 RID: 8857
	[Header("Buttons")]
	public Transform startButton;

	// Token: 0x0400229A RID: 8858
	public Text header;

	// Token: 0x0400229B RID: 8859
	[Tooltip("TextBox to show award text")]
	public Text awardText;

	// Token: 0x0400229C RID: 8860
	[Tooltip("Canvas Group to chagne texts alpha")]
	public CanvasGroup awardGroup;

	// Token: 0x0400229D RID: 8861
	private bool changeAlpha;

	// Token: 0x0400229E RID: 8862
	[HideInInspector]
	public int awardSelected;

	// Token: 0x0400229F RID: 8863
	private levelPackControl levelControl;

	// Token: 0x040022A0 RID: 8864
	[Header("Replay buttons")]
	public RectTransform legoReplayButton;

	// Token: 0x040022A1 RID: 8865
	public RectTransform diplomReplayButton;

	// Token: 0x040022A2 RID: 8866
	[HideInInspector]
	public Action replayCallback;

	// Token: 0x040022A3 RID: 8867
	[Header("JigSaw Icon UI")]
	public Transform jigSawIconParent;

	// Token: 0x040022A4 RID: 8868
	public float jigSawEmptyAlpha = 0.8f;

	// Token: 0x040022A5 RID: 8869
	public float jigSawIconWidth = 30f;

	// Token: 0x040022A6 RID: 8870
	private bool startButtonIsBroken;
}
