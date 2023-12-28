using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000523 RID: 1315
public class Console : MonoBehaviour
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0008868F File Offset: 0x00086A8F
	public static global::Console self
	{
		get
		{
			if (global::Console._self == null)
			{
				global::Console._self = UIControl.self.console;
			}
			return global::Console._self;
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000886D5 File Offset: 0x00086AD5
	// (set) Token: 0x06001E2A RID: 7722 RVA: 0x000886B5 File Offset: 0x00086AB5
	public bool canOpen
	{
		get
		{
			return this._canOpen;
		}
		set
		{
			if (this._canOpen && !value)
			{
				this.TryToHideOptionsConsole();
			}
			this._canOpen = value;
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000886E0 File Offset: 0x00086AE0
	private void Start()
	{
		this.container.gameObject.SetActive(false);
		this.defaultInputField.SetActive(false);
		this.colliderToBlockMouse.SetActive(false);
		this.mouseOverImg = this.mouseOverObj.GetComponent<Image>();
		this.mouseOverStartColor = this.mouseOverImg.color;
		Color color = this.mouseOverImg.color;
		color.a = 0f;
		this.mouseOverImg.color = color;
		this.inputField.gameObject.SetActive(false);
		this.inputFieldStats = LineTranslator.GetTextWithStats(this.inputField);
		LineTranslator.SetFontAndSize(this.inputFieldStats, new Text[]
		{
			this.inputField
		});
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x00088798 File Offset: 0x00086B98
	private void Update()
	{
		this.moveMouseOver();
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000887A0 File Offset: 0x00086BA0
	private void moveMouseOver()
	{
		if (!this.active || this.buttonUnderMouse == null)
		{
			return;
		}
		Vector2 v = this.mouseOverObj.position;
		Vector2 vector = this.buttonUnderMouse.position;
		v.y = Mathf.Lerp(v.y, vector.y, Time.deltaTime * 18f);
		this.mouseOverObj.position = v;
		if (this.mouseOverImg.color.a <= 0f)
		{
			v.y = vector.y;
			this.mouseOverObj.position = v;
		}
		if (this.mouseOverClicked)
		{
			return;
		}
		if (this.mouseOverImg.color.a != this.mouseOverStartColor.a)
		{
			Color color = this.mouseOverImg.color;
			color.a = Mathf.MoveTowards(color.a, this.mouseOverStartColor.a, Time.deltaTime * 5f);
			this.mouseOverImg.color = color;
		}
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x000888D0 File Offset: 0x00086CD0
	private IEnumerator hideMouseOver()
	{
		while (this.mouseOverClicked)
		{
			yield return null;
		}
		this.buttonUnderMouse = null;
		Color c = this.mouseOverImg.color;
		while (c.a > 0f)
		{
			c.a = Mathf.MoveTowards(c.a, 0f, Time.deltaTime * 10f);
			this.mouseOverImg.color = c;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x000888EB File Offset: 0x00086CEB
	public void mouseOverClick()
	{
		if (!this.active)
		{
			return;
		}
		this.mouseOverClicked = true;
		base.StartCoroutine(this.animateMouseOverClick());
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x00088910 File Offset: 0x00086D10
	private IEnumerator animateMouseOverClick()
	{
		float time = 0f;
		float max = this.mouseOverOnClickCurve.keys[this.mouseOverOnClickCurve.length - 1].time;
		Color c = this.mouseOverImg.color;
		while (time < max)
		{
			time = Mathf.MoveTowards(time, max, Time.deltaTime * (max / this.mouseOverOnClickTime));
			float prog = this.mouseOverOnClickCurve.Evaluate(time);
			c.a = this.mouseOverStartColor.a + prog * this.mouveOverOnClickAlpha;
			this.mouseOverImg.color = c;
			yield return null;
		}
		this.mouseOverClicked = false;
		yield break;
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x0008892B File Offset: 0x00086D2B
	public void mouseOver(RectTransform button)
	{
		if (!this.active)
		{
			return;
		}
		if (button != this.buttonUnderMouse)
		{
			this.buttonUnderMouse = button;
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x00088954 File Offset: 0x00086D54
	public IEnumerator typeCommand(string text, float minTime = 0f, float maxTime = 0.03f)
	{
		this.defaultInputField.SetActive(false);
		this.inputField.gameObject.SetActive(true);
		LineTranslator.SetFontAndSize(this.inputFieldStats, new Text[]
		{
			this.inputField
		});
		this.inputField.text = string.Empty;
		string newTxt = string.Empty;
		while (text.Length > 0)
		{
			newTxt += text[0];
			text = text.Remove(0, 1);
			this.inputField.text = newTxt;
			if (text.Length != 0)
			{
				Text text2 = this.inputField;
				text2.text += "_";
			}
			float waitTime = UnityEngine.Random.Range(minTime, maxTime);
			if (UnityEngine.Random.value > 0.9f)
			{
				waitTime *= 3f;
			}
			Audio.self.playOneShot("7a36dc28-2acf-4156-8750-2915188ac530", 1f);
			yield return new WaitForSeconds(waitTime);
		}
		yield return new WaitForSeconds(0.1f);
		Audio.self.playOneShot("1c5e3114-61ae-4ec3-805f-b4a6f013e1e3", 1f);
		yield break;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x00088984 File Offset: 0x00086D84
	public void resetInputField(bool showDefault = true)
	{
		this.inputField.gameObject.SetActive(false);
		this.defaultInputField.SetActive(showDefault);
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000889A4 File Offset: 0x00086DA4
	private ConsoleSubMenu createSubMenu(RectTransform screen)
	{
		RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(screen);
		rectTransform.name = rectTransform.name.Replace("(Clone)", string.Empty);
		rectTransform.SetParent(this.container);
		rectTransform.localScale = Vector3.one;
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.right;
		rectTransform.offsetMin = new Vector2(14f, 38f);
		rectTransform.offsetMax = Vector2.left * 14f;
		ConsoleSubMenu component = rectTransform.GetComponent<ConsoleSubMenu>();
		component.setMenu();
		return component;
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x00088A38 File Offset: 0x00086E38
	public void resetConsole()
	{
		this.onScreen = false;
		this.active = false;
		this.moving = false;
		this.colliderToBlockMouse.SetActive(false);
		this.container.gameObject.SetActive(false);
		Audio.self.SetSnapshot(MusicTypes.Console, false);
		UnityEngine.Object.Destroy(this.currentMenu.transform.gameObject);
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x00088A9C File Offset: 0x00086E9C
	public void TryToHideOptionsConsole()
	{
		if (this.moving)
		{
			return;
		}
		if (!this.onScreen)
		{
			return;
		}
		if (this.currentMenu == null)
		{
			return;
		}
		if (this.currentMenu.transform.name != this.pauseMenu.name && this.currentMenu.transform.name != this.audioMenu.name && this.currentMenu.transform.name != this.confirmExitMenu.name)
		{
			return;
		}
		this.hideConsole();
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x00088B4C File Offset: 0x00086F4C
	public void hideConsole()
	{
		if (this.moving)
		{
			return;
		}
		Audio.self.SetSnapshot(MusicTypes.Console, false);
		Audio.self.playOneShot("bbd3376a-2a83-4a41-9052-4c1f7fd0c779", 1f);
		this.moving = true;
		base.StartCoroutine(this.animateConsoleShow(false));
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x00088B9C File Offset: 0x00086F9C
	public void showConsole(RectTransform newMenu = null)
	{
		if (this.moving)
		{
			return;
		}
		this.moving = true;
		if (this.currentMenu != null)
		{
			UnityEngine.Object.Destroy(this.currentMenu.transform.gameObject);
		}
		if (newMenu == null)
		{
			this.currentMenu = this.createSubMenu(this.pauseMenu);
		}
		else
		{
			this.currentMenu = this.createSubMenu(newMenu);
		}
		Audio.self.SetSnapshot(MusicTypes.Console, true);
		Audio.self.playOneShot("6600d56f-17f6-4aac-9835-9f3f5e13d6fc", 1f);
		base.StartCoroutine(this.animateConsoleShow(true));
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x00088C44 File Offset: 0x00087044
	public void openCloseConsole()
	{
		if (this.moving || !this.canOpen)
		{
			return;
		}
		if (!this.onScreen)
		{
			this.showConsole(null);
		}
		else if (this.active)
		{
			this.hideConsole();
		}
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x00088C90 File Offset: 0x00087090
	public void switchMenu(RectTransform nextMenu)
	{
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.switchMenus(nextMenu));
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x00088CAC File Offset: 0x000870AC
	private IEnumerator switchMenus(RectTransform newMenu)
	{
		this.active = false;
		this.nextMenu = this.createSubMenu(newMenu);
		yield return base.StartCoroutine(this.nextMenu.showMenu());
		this.active = true;
		yield break;
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x00088CD0 File Offset: 0x000870D0
	public void hideOldMenu()
	{
		if (this.nextMenu == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.currentMenu.transform.gameObject);
		this.currentMenu = this.nextMenu;
		this.nextMenu = null;
		this.buttonUnderMouse = null;
		Color color = this.mouseOverImg.color;
		color.a = 0f;
		this.mouseOverImg.color = color;
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x00088D44 File Offset: 0x00087144
	private IEnumerator animateConsoleShow(bool show)
	{
		if (show)
		{
			this.container.gameObject.SetActive(true);
		}
		else
		{
			this.onScreen = false;
			this.active = false;
			base.StartCoroutine(this.hideMouseOver());
			this.colliderToBlockMouse.SetActive(false);
			yield return base.StartCoroutine(this.currentMenu.hideConsole());
		}
		float height = this.container.sizeDelta.y;
		AnimationCurve curve = (!show) ? this.hideContainerCurve : this.showContainerCurve;
		float time = 0f;
		float max = curve.keys[curve.length - 1].time;
		while (time < max)
		{
			time = Mathf.MoveTowards(time, max, Time.deltaTime);
			float prog = curve.Evaluate(time);
			Vector2 pos = this.container.anchoredPosition;
			pos.y = height * prog;
			this.container.anchoredPosition = pos;
			yield return null;
		}
		if (show)
		{
			this.currentMenu.gameObject.SetActive(true);
			yield return base.StartCoroutine(this.currentMenu.showMenu());
			this.colliderToBlockMouse.SetActive(true);
			this.onScreen = true;
			this.active = true;
		}
		else
		{
			this.container.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(this.currentMenu.transform.gameObject);
		}
		this.moving = false;
		yield break;
	}

	// Token: 0x04002179 RID: 8569
	private static global::Console _self;

	// Token: 0x0400217A RID: 8570
	public const float DELAY_TO_SHOW_LINE = 0.03f;

	// Token: 0x0400217B RID: 8571
	public const float DELAY_LONG = 0.1f;

	// Token: 0x0400217C RID: 8572
	public bool active;

	// Token: 0x0400217D RID: 8573
	private bool onScreen;

	// Token: 0x0400217E RID: 8574
	private bool moving;

	// Token: 0x0400217F RID: 8575
	private bool _canOpen = true;

	// Token: 0x04002180 RID: 8576
	[HideInInspector]
	public ConsoleSubMenu currentMenu;

	// Token: 0x04002181 RID: 8577
	private ConsoleSubMenu nextMenu;

	// Token: 0x04002182 RID: 8578
	[Header("Container")]
	public RectTransform container;

	// Token: 0x04002183 RID: 8579
	public AnimationCurve showContainerCurve;

	// Token: 0x04002184 RID: 8580
	public AnimationCurve hideContainerCurve;

	// Token: 0x04002185 RID: 8581
	public GameObject colliderToBlockMouse;

	// Token: 0x04002186 RID: 8582
	[Header("Graphics")]
	public RectTransform mouseOverObj;

	// Token: 0x04002187 RID: 8583
	private Image mouseOverImg;

	// Token: 0x04002188 RID: 8584
	private Color mouseOverStartColor;

	// Token: 0x04002189 RID: 8585
	private RectTransform buttonUnderMouse;

	// Token: 0x0400218A RID: 8586
	private bool mouseOverClicked;

	// Token: 0x0400218B RID: 8587
	public AnimationCurve mouseOverOnClickCurve;

	// Token: 0x0400218C RID: 8588
	public float mouveOverOnClickAlpha;

	// Token: 0x0400218D RID: 8589
	public float mouseOverOnClickTime;

	// Token: 0x0400218E RID: 8590
	[Space(10f)]
	public Text inputField;

	// Token: 0x0400218F RID: 8591
	private TextWithStats inputFieldStats;

	// Token: 0x04002190 RID: 8592
	public GameObject defaultInputField;

	// Token: 0x04002191 RID: 8593
	[Header("Menus")]
	public RectTransform pauseMenu;

	// Token: 0x04002192 RID: 8594
	public RectTransform audioMenu;

	// Token: 0x04002193 RID: 8595
	[Space(10f)]
	public RectTransform gameLoading;

	// Token: 0x04002194 RID: 8596
	public RectTransform confirmExitMenu;

	// Token: 0x04002195 RID: 8597
	public RectTransform contactingDeveloper;

	// Token: 0x04002196 RID: 8598
	public RectTransform hundredPhotosDuck;

	// Token: 0x04002197 RID: 8599
	public RectTransform queueDuckCkeck;

	// Token: 0x04002198 RID: 8600
	public RectTransform cupDuck;

	// Token: 0x04002199 RID: 8601
	public RectTransform cupDuck_DuckOptions;

	// Token: 0x0400219A RID: 8602
	public RectTransform cupDuck_DestroyDuck;

	// Token: 0x0400219B RID: 8603
	public RectTransform cupDuck_BlueButton;

	// Token: 0x0400219C RID: 8604
	public RectTransform cupDuck_LastScreen;

	// Token: 0x0400219D RID: 8605
	[Space(10f)]
	public RectTransform pack11_Port;

	// Token: 0x0400219E RID: 8606
	public RectTransform pack11_Info;

	// Token: 0x0400219F RID: 8607
	public RectTransform pack11_File;

	// Token: 0x040021A0 RID: 8608
	[Space(10f)]
	public RectTransform pack12_MailList;
}
