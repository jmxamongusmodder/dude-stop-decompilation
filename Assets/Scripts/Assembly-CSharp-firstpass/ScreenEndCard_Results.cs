using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000580 RID: 1408
public class ScreenEndCard_Results : ScreenEndCard
{
	// Token: 0x06002063 RID: 8291 RVA: 0x0009EFC1 File Offset: 0x0009D3C1
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		if (this.solvingOrder == null || this.solvingOrder.Count < 2)
		{
			return;
		}
		base.StartCoroutine(this.animateScreen());
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x0009EFFC File Offset: 0x0009D3FC
	public override void setScreen(Transform item)
	{
		base.setScreen(item);
		this.solvingOrder = AwardController.self.getSolvedOrder();
		if (this.solvingOrder == null || this.solvingOrder.Count < 2)
		{
			Debug.LogError("Pack can't have only 2 puzzles. Chave End Screen Card!");
			return;
		}
		int count = this.solvingOrder.Count;
		Vector2 sizeDelta = this.container.sizeDelta;
		sizeDelta.x = this.iconWidth * (float)count;
		this.container.sizeDelta = sizeDelta;
		int cupPos = Mathf.CeilToInt((float)AwardController.self.getNeededProc(true, false) / 100f * (float)count);
		int cupPos2 = Mathf.CeilToInt((float)AwardController.self.getNeededProc(false, false) / 100f * (float)count);
		int cup100Pos = Mathf.CeilToInt((float)AwardController.self.getNeededProc(true, true) / 100f * (float)count);
		int cup100Pos2 = Mathf.CeilToInt((float)AwardController.self.getNeededProc(false, true) / 100f * (float)count);
		this.badLine.setActive(count, cupPos, cup100Pos, AwardController.self.previousBestBad);
		this.goodLine.setActive(count, cupPos2, cup100Pos2, AwardController.self.previousBestGood);
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x0009F120 File Offset: 0x0009D520
	private IEnumerator animateScreen()
	{
		while (this.solvingOrder.Count > 0)
		{
			bool monster = this.solvingOrder.Dequeue();
			if (monster)
			{
				yield return base.StartCoroutine(this.badLine.showIcon(this.animationSpeed));
			}
			else
			{
				yield return base.StartCoroutine(this.goodLine.showIcon(this.animationSpeed));
			}
			this.animationSpeed += this.animationAcceleration;
		}
		yield break;
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x0009F13C File Offset: 0x0009D53C
	public override void bContinue()
	{
		if (this.puzzle.GetComponent<AudioVoice_ResultScreen>() != null && !this.puzzle.GetComponent<AudioVoice_ResultScreen>().isClickAllowed(ClickWhileVoice.contin))
		{
			return;
		}
		AnalyticsComponent.PuzzleFinished(this.puzzle.name);
		base.bContinue();
	}

	// Token: 0x040023A9 RID: 9129
	[Header("Result bars")]
	public RectTransform container;

	// Token: 0x040023AA RID: 9130
	public NoRewardResultLine goodLine;

	// Token: 0x040023AB RID: 9131
	public NoRewardResultLine badLine;

	// Token: 0x040023AC RID: 9132
	public Transform buttonContinue;

	// Token: 0x040023AD RID: 9133
	public float iconWidth;

	// Token: 0x040023AE RID: 9134
	public float animationSpeed;

	// Token: 0x040023AF RID: 9135
	public float animationAcceleration;

	// Token: 0x040023B0 RID: 9136
	private Queue<bool> solvingOrder;
}
