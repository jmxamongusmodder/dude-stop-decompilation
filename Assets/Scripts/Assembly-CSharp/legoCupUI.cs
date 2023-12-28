using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public class legoCupUI : AbstractUIScreen
{
	// Token: 0x06001F49 RID: 8009 RVA: 0x00095710 File Offset: 0x00093B10
	private void Start()
	{
		PuzzleStats component = this.puzzle.GetComponent<PuzzleStats>();
		component.loadUIOnStart = false;
		component.UIScreen = this.cupUI;
		component.UIScreenSecondary = component.UIScreenCurr;
		component.UIScreenCurr = null;
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x0009574F File Offset: 0x00093B4F
	public override void removeScreen()
	{
		base.enabled = false;
		this.active = false;
		this.puzzle.GetComponent<PuzzleStats>().UIScreenSecondary = null;
		base.StartCoroutine(this.moveButtons());
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x00095790 File Offset: 0x00093B90
	private IEnumerator moveButtons()
	{
		for (;;)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					RectTransform rectTransform = (RectTransform)obj;
					Vector2 anchoredPosition = rectTransform.anchoredPosition;
					anchoredPosition.y -= Time.deltaTime * 200f;
					rectTransform.anchoredPosition = anchoredPosition;
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
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x000957AC File Offset: 0x00093BAC
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
		if (!Global.self.replayingCupPuzzle)
		{
			this.saveButton.anchoredPosition = this.resetButton.anchoredPosition;
			this.saveButton.gameObject.SetActive(false);
			this.cancelButton.gameObject.SetActive(false);
			this.resetButton.gameObject.SetActive(false);
			global::Console.self.canOpen = false;
			this.subtShift = this.puzzle.GetComponent<PuzzleStats>().subtitlesYShift;
			this.puzzle.GetComponent<PuzzleStats>().subtitlesYShift = 0f;
		}
		else
		{
			AnalyticsComponent.PuzzleStarted(this.puzzle.name);
		}
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x00095863 File Offset: 0x00093C63
	public void showSaveButton()
	{
		this.saveButton.gameObject.SetActive(true);
		global::Console.self.canOpen = true;
		this.puzzle.GetComponent<PuzzleStats>().subtitlesYShift = this.subtShift;
		UIControl.positionSubtitles(null);
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x0009589D File Offset: 0x00093C9D
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x0009589F File Offset: 0x00093C9F
	public void bReset()
	{
		if (!this.active)
		{
			return;
		}
		this.puzzle.GetComponentInChildren<CupLego_Controller>().Reset();
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x000958C0 File Offset: 0x00093CC0
	public void bSave()
	{
		if (!this.active)
		{
			return;
		}
		if (this.puzzle.GetComponentInChildren<CupLego_Controller>().AcquireCup())
		{
			this.removeScreen();
			if (Global.self.replayingCupPuzzle)
			{
				AnalyticsComponent.PuzzleFinished(this.puzzle.name);
			}
		}
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x00095913 File Offset: 0x00093D13
	public void bCancel()
	{
		if (!this.active)
		{
			return;
		}
		this.puzzle.GetComponent<PuzzleStats>().makePauseMenu();
	}

	// Token: 0x04002284 RID: 8836
	public Transform cupUI;

	// Token: 0x04002285 RID: 8837
	private Transform puzzle;

	// Token: 0x04002286 RID: 8838
	[Space(10f)]
	public RectTransform cancelButton;

	// Token: 0x04002287 RID: 8839
	public RectTransform resetButton;

	// Token: 0x04002288 RID: 8840
	public RectTransform saveButton;

	// Token: 0x04002289 RID: 8841
	private float subtShift;
}
