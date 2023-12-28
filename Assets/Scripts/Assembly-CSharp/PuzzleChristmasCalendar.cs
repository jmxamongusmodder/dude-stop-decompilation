using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E1 RID: 993
public class PuzzleChristmasCalendar : MonoBehaviour
{
	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600190C RID: 6412 RVA: 0x0005B296 File Offset: 0x00059696
	// (set) Token: 0x0600190D RID: 6413 RVA: 0x0005B2A0 File Offset: 0x000596A0
	protected int currentMonth
	{
		get
		{
			return this._currentMonth;
		}
		set
		{
			this._currentMonth = value % 12;
			if (this._currentMonth >= 11)
			{
				AudioVoice_ChristmasTree2 componentInPuzzleStats = this.GetComponentInPuzzleStats<AudioVoice_ChristmasTree2>();
				if (componentInPuzzleStats != null)
				{
					componentInPuzzleStats.setDecember();
				}
			}
		}
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x0005B2E0 File Offset: 0x000596E0
	private void Awake()
	{
		this.currentMonth = (this.startMonth = 11);
		this.thisMonth = base.GetComponentInChildren<LineTranslator>();
		this.UpdateMonthText();
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x0005B310 File Offset: 0x00059710
	private void Update()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		a.z = 0f;
		if (this.dragged && Vector3.Distance(a, this.startPosition) > this.tearDistance)
		{
			this.TearPage();
		}
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x0005B364 File Offset: 0x00059764
	private void TearPage()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position.z = 0f;
		if (!this.box.gameObject.activeInHierarchy)
		{
			Audio.self.playLoopSound("ed9cbe51-9e9d-4e3d-8dc1-47715109feab");
			this.box.gameObject.SetActive(true);
		}
		if (!this.boxShown)
		{
			base.StartCoroutine(this.BoxShowingCoroutine());
		}
		this.dragged = false;
		LineTranslator componentInChildren = base.GetComponentInChildren<LineTranslator>();
		GameObject gameObject = new GameObject(string.Format("Torn page ({0})", componentInChildren.components[0].text));
		gameObject.gameObject.AddComponent<PuzzleChristmasTornPage>().paper = this.crumpledPaper;
		gameObject.gameObject.GetComponent<PuzzleChristmasTornPage>().crumpleDistance = this.crumpleDistance;
		gameObject.transform.SetParent(base.transform.parent);
		gameObject.transform.position = position;
		Audio.self.playOneShot("0ab4f22a-fb66-4c71-b46d-97ab9ed05a2b", 1f);
		if (Global.self.currPuzzle.GetComponent<AudioVoice_ChristmasTree>())
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_ChristmasTree>().tearOff();
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.tornPage.gameObject);
		gameObject2.transform.position = position;
		gameObject2.transform.SetParent(gameObject.transform);
		gameObject2.gameObject.SetActive(true);
		gameObject2.GetComponentInChildren<LineTranslator>().guid = componentInChildren.guid;
		gameObject2.GetComponentInChildren<LineTranslator>().translateText(false);
		this.UpdateTextSize(gameObject2.GetComponentInChildren<Text>());
		Vector3 b = (position.x - this.startPosition.x >= 0f) ? new Vector3(-0.75f, -0.557f, 0f) : new Vector3(0.6f, -0.557f, 0f);
		gameObject.transform.position = base.transform.position - b;
		gameObject.transform.position -= new Vector3(0f, 0.12f, 0f);
		gameObject2.transform.position += b;
		this.currentMonth++;
		this.UpdateMonthText();
		if (this.monthsPassed++ == this.monthsToWait)
		{
			this.TimePassed();
		}
		if (this.monthsPassed == this.totalMonths)
		{
			base.GetComponent<Collider2D>().enabled = false;
		}
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x0005B5F9 File Offset: 0x000599F9
	private void OnMouseDown()
	{
		this.startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.startPosition.z = 0f;
		this.dragged = true;
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x0005B627 File Offset: 0x00059A27
	private void OnMouseUp()
	{
		this.dragged = false;
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x0005B630 File Offset: 0x00059A30
	protected virtual IEnumerator BoxShowingCoroutine()
	{
		this.boxShown = true;
		this.box.gameObject.SetActive(true);
		while (this.box.position.x != this.boxPoint.position.x)
		{
			this.box.position = Vector3.MoveTowards(this.box.position, this.boxPoint.position, this.boxSpeed * Time.deltaTime);
			Audio.self.playLoopSound("ed9cbe51-9e9d-4e3d-8dc1-47715109feab");
			yield return null;
		}
		Audio.self.stopLoopSound("ed9cbe51-9e9d-4e3d-8dc1-47715109feab", true);
		yield break;
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x0005B64B File Offset: 0x00059A4B
	protected virtual void TimePassed()
	{
		this.tree.GetComponent<PuzzleChristmasTree>().TimePassed();
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x0005B660 File Offset: 0x00059A60
	protected void UpdateMonthText()
	{
		this.thisMonth.guid = "CALENDAR_SHORT_" + this.monthTexts[this.currentMonth];
		this.thisMonth.translateText(false);
		this.UpdateTextSize(this.thisMonth.GetComponent<Text>());
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x0005B6B0 File Offset: 0x00059AB0
	private void UpdateTextSize(Text text)
	{
		if (text.text.Length == 3)
		{
			text.fontSize = this.threeLetterSize;
		}
		else if (text.text.Length == 4)
		{
			text.fontSize = this.fourLetterSize;
		}
	}

	// Token: 0x04001700 RID: 5888
	public float tearDistance = 0.3f;

	// Token: 0x04001701 RID: 5889
	public float crumpleDistance = 0.1f;

	// Token: 0x04001702 RID: 5890
	public float boxSpeed = 2f;

	// Token: 0x04001703 RID: 5891
	public Transform box;

	// Token: 0x04001704 RID: 5892
	public Transform boxPoint;

	// Token: 0x04001705 RID: 5893
	public Transform tree;

	// Token: 0x04001706 RID: 5894
	public Transform tornPage;

	// Token: 0x04001707 RID: 5895
	public Transform crumpledPaper;

	// Token: 0x04001708 RID: 5896
	public int monthsToWait = 4;

	// Token: 0x04001709 RID: 5897
	public int totalMonths = 6;

	// Token: 0x0400170A RID: 5898
	public int fourLetterSize = 15;

	// Token: 0x0400170B RID: 5899
	public int threeLetterSize = 20;

	// Token: 0x0400170C RID: 5900
	protected const string MONTH_TEXT_BASE = "CALENDAR_SHORT_";

	// Token: 0x0400170D RID: 5901
	protected string[] monthTexts = new string[]
	{
		"JAN",
		"FEB",
		"MAR",
		"APR",
		"MAY",
		"JUN",
		"JUL",
		"AUG",
		"SEP",
		"OCT",
		"NOV",
		"DEC"
	};

	// Token: 0x0400170E RID: 5902
	protected int startMonth;

	// Token: 0x0400170F RID: 5903
	private int monthsPassed;

	// Token: 0x04001710 RID: 5904
	private int _currentMonth;

	// Token: 0x04001711 RID: 5905
	protected LineTranslator thisMonth;

	// Token: 0x04001712 RID: 5906
	private Vector3 startPosition;

	// Token: 0x04001713 RID: 5907
	private bool boxShown;

	// Token: 0x04001714 RID: 5908
	private bool dragged;
}
