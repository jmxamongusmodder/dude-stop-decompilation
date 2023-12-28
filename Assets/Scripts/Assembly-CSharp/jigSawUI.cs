using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000554 RID: 1364
public class jigSawUI : AbstractUIScreen
{
	// Token: 0x06001F3E RID: 7998 RVA: 0x000951C8 File Offset: 0x000935C8
	private void Start()
	{
		PuzzleStats component = this.puzzle.GetComponent<PuzzleStats>();
		component.UIScreen = null;
		component.UIScreenSecondary = component.UIScreenCurr;
		component.UIScreenCurr = null;
		this.noPiecesText.SetActive(false);
		this.shaffleButton.SetActive(true);
		if (Global.self.unlockedJigsawPieces == 0)
		{
			this.noPiecesText.SetActive(true);
		}
		if (Global.self.unlockedJigsawPieces < 2)
		{
			this.shaffleButton.SetActive(false);
			this.backButton.anchoredPosition = this.backButton.anchoredPosition.setX(0f);
		}
		this.jigsawCount.text = Global.self.unlockedJigsawPieces + "/" + 20;
		base.StartCoroutine(this.hideJigSawCounter());
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x000952A4 File Offset: 0x000936A4
	private IEnumerator hideJigSawCounter()
	{
		yield return new WaitForSeconds(8f);
		if (!base.enabled || !this.active)
		{
			yield break;
		}
		float startY = this.jigsawParent.anchoredPosition.y;
		Vector2 pos = this.jigsawParent.anchoredPosition;
		float prog = 0f;
		while (prog < 1f && this.active)
		{
			prog += Time.deltaTime * 3f;
			pos.y = startY + this.jigsawCurve.Evaluate(prog) * 34f;
			this.jigsawParent.anchoredPosition = pos;
			yield return null;
		}
		this.jigsawParent.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000952BF File Offset: 0x000936BF
	public override void removeScreen()
	{
		base.enabled = false;
		this.active = false;
		this.puzzle.GetComponent<PuzzleStats>().UIScreenSecondary = null;
		base.StartCoroutine(this.moveButtons());
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x00095300 File Offset: 0x00093700
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

	// Token: 0x06001F42 RID: 8002 RVA: 0x0009531B File Offset: 0x0009371B
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x00095324 File Offset: 0x00093724
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x00095326 File Offset: 0x00093726
	public void bShaffle()
	{
		if (!this.active)
		{
			return;
		}
		this.puzzle.GetComponentInChildren<PuzzleJigsaw_Controller>().Reset();
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x00095344 File Offset: 0x00093744
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		if (!this.puzzle.GetComponent<AudioVoice_JigSawPuzzle>().isClickAllowed(ClickWhileVoice.back))
		{
			return;
		}
		AnalyticsComponent.PuzzleAborted(this.puzzle.name, Global.self.currentLevelPack);
		Global.TellAnalyticsLevelAborted();
		this.puzzle.GetComponentInChildren<PuzzleJigsaw_Controller>().SaveJigsaw(false);
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.left, true);
	}

	// Token: 0x0400227B RID: 8827
	private Transform puzzle;

	// Token: 0x0400227C RID: 8828
	public RectTransform[] buttons;

	// Token: 0x0400227D RID: 8829
	public GameObject noPiecesText;

	// Token: 0x0400227E RID: 8830
	public GameObject shaffleButton;

	// Token: 0x0400227F RID: 8831
	public RectTransform backButton;

	// Token: 0x04002280 RID: 8832
	public Text jigsawCount;

	// Token: 0x04002281 RID: 8833
	public RectTransform jigsawParent;

	// Token: 0x04002282 RID: 8834
	public AnimationCurve jigsawCurve;
}
