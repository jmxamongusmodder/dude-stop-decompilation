using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000566 RID: 1382
public class Pack09_Intro_1 : AbstractUIScreen
{
	// Token: 0x06001FD5 RID: 8149 RVA: 0x00099A4A File Offset: 0x00097E4A
	public override void setScreen(Transform item)
	{
		this.loadingBar.value = 0f;
		this.ps = item.GetComponent<PuzzleStats>();
		this.continueButton.SetActive(false);
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x00099A74 File Offset: 0x00097E74
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.loadBar());
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x00099A91 File Offset: 0x00097E91
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x00099A94 File Offset: 0x00097E94
	private IEnumerator loadBar()
	{
		yield return null;
		float time = 0f;
		while (time < this.loadingTime)
		{
			time += Time.deltaTime;
			if (this.loadingBar.value < time / this.loadingTime)
			{
				this.loadingBar.value += UnityEngine.Random.Range(0f, 0.08f);
			}
			yield return null;
		}
		this.continueButton.SetActive(true);
		this.ps.subtitlesYShift = 60f;
		UIControl.positionSubtitles(null);
		yield break;
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x00099AB0 File Offset: 0x00097EB0
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		if (Global.self.currPuzzle.GetComponent<AudioVoice_Pack09_Intro_1>())
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_Pack09_Intro_1>().onContinue();
		}
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x040022FF RID: 8959
	public GameObject continueButton;

	// Token: 0x04002300 RID: 8960
	public float loadingTime = 16f;

	// Token: 0x04002301 RID: 8961
	public Slider loadingBar;

	// Token: 0x04002302 RID: 8962
	private PuzzleStats ps;
}
