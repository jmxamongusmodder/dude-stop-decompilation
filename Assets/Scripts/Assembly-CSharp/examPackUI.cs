using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class examPackUI : AbstractUIScreen
{
	// Token: 0x06001EF3 RID: 7923 RVA: 0x00092F40 File Offset: 0x00091340
	public override void Update()
	{
		base.Update();
		if (!this.active)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			if (Input.GetKeyDown(KeyCode.Keypad1) && Global.self.NoCurrentTransition)
			{
				AwardController.self.addBadEndingCount();
				AwardController.self.addGoodEndingCount();
				AwardController.self.solveAsBad(null);
				this.bNext();
			}
			if (Input.GetKeyDown(KeyCode.Keypad2) && Global.self.NoCurrentTransition)
			{
				AwardController.self.addBadEndingCount();
				AwardController.self.addGoodEndingCount();
				AwardController.self.solveAsGood(null);
				this.bNext();
			}
			if (Input.GetKeyDown(KeyCode.Keypad3) && Global.self.NoCurrentTransition)
			{
				AwardController.self.addBadEndingCount();
				AwardController.self.addBadEndingCount();
				AwardController.self.addGoodEndingCount();
				AwardController.self.addGoodEndingCount();
				AwardController.self.solveAsGood(null);
				AwardController.self.solveAsBad(null);
				this.bNext();
			}
		}
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00093056 File Offset: 0x00091456
	public void EndAnimation()
	{
		base.enabled = true;
		base.GetComponent<Animator>().enabled = false;
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x0009306B File Offset: 0x0009146B
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x0009306D File Offset: 0x0009146D
	public override void setScreen(Transform item)
	{
		this.nextButton.gameObject.SetActive(false);
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x00093080 File Offset: 0x00091480
	public void showNextButton()
	{
		this.nextButton.gameObject.SetActive(true);
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00093093 File Offset: 0x00091493
	public void bNext()
	{
		if (!base.enabled)
		{
			return;
		}
		base.StartCoroutine(this.gotoNext());
		base.enabled = false;
		this.nextButton.gameObject.SetActive(false);
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x000930C8 File Offset: 0x000914C8
	private IEnumerator gotoNext()
	{
		yield return base.StartCoroutine(Global.self.currPuzzle.GetComponent<AudioVoice_Exam>().levelFinished());
		Global.self.gotoNextLevel(false, null);
		Global.self.justFinishedExamGoodScore = (float)AwardController.self.getProgress(true);
		yield break;
	}

	// Token: 0x04002241 RID: 8769
	public Transform nextButton;
}
