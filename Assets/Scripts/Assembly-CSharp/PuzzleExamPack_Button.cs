using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002E RID: 46
public class PuzzleExamPack_Button : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600010C RID: 268 RVA: 0x0000A717 File Offset: 0x00008917
	private Transform book
	{
		get
		{
			if (PuzzleExamPack_Button._book == null)
			{
				PuzzleExamPack_Button._book = Global.self.currPuzzle.GetComponent<PuzzleStats>().UIScreenCurr;
			}
			return PuzzleExamPack_Button._book;
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000A748 File Offset: 0x00008948
	public void AnswerSelected(bool correct)
	{
		if (!base.enabled)
		{
			return;
		}
		Animator animator;
		Image image;
		if (correct)
		{
			animator = this.correctAnimator;
			image = this.correctCheckbox;
			Audio.self.playOneShot("83670fe1-1df7-41fb-a3ed-43e9d7935c83", 1f);
			Global.self.currPuzzle.GetComponent<AudioVoice_Exam>().playAnswer(false);
			AnalyticsComponent.PuzzleFinishedAsGoodExamPack(base.name);
			if (!this.bothCorrect)
			{
				AwardController.self.solveAsGood(null);
			}
		}
		else
		{
			animator = this.wrongAnimator;
			image = this.wrongCheckbox;
			if (this.bothCorrect)
			{
				Audio.self.playOneShot("83670fe1-1df7-41fb-a3ed-43e9d7935c83", 1f);
				Global.self.currPuzzle.GetComponent<AudioVoice_Exam>().playAnswer(false);
				AnalyticsComponent.PuzzleFinishedAsGoodExamPack(base.name);
			}
			else
			{
				Audio.self.playOneShot("83161f8e-bbf1-4f39-9840-069a3db39513", 1f);
				Global.self.currPuzzle.GetComponent<AudioVoice_Exam>().playAnswer(true);
				AwardController.self.solveAsBad(null);
				AnalyticsComponent.PuzzleFinishedAsMonsterExamPack(base.name);
			}
		}
		animator.gameObject.SetActive(true);
		animator.enabled = true;
		animator.Play("checkMarkAppear");
		image.color = this.checkedColor;
		base.GetComponentsInChildren<Button>().ToList<Button>().ForEach(delegate(Button x)
		{
			x.enabled = false;
			x.GetComponent<ButtonTemplate>().enabled = false;
		});
		if (!this.bothCorrect)
		{
			AwardController.self.addBadEndingCount();
			AwardController.self.addGoodEndingCount();
		}
		this.answered = true;
		base.enabled = false;
		this.wrongCheckbox.transform.parent.GetComponent<ButtonTemplate>().disableSounds(1);
		this.correctCheckbox.transform.parent.GetComponent<ButtonTemplate>().disableSounds(1);
		if ((from x in this.book.GetComponent<Book>().bookPages
		where x.GetComponent<PuzzleExamPack_Button>() != null && !x.GetComponent<PuzzleExamPack_Button>().answered
		select x).Count<Transform>() == 0)
		{
			this.book.GetComponent<examPackUI>().showNextButton();
		}
	}

	// Token: 0x04000190 RID: 400
	private static Transform _book;

	// Token: 0x04000191 RID: 401
	public Animator wrongAnimator;

	// Token: 0x04000192 RID: 402
	public Image wrongCheckbox;

	// Token: 0x04000193 RID: 403
	public Animator correctAnimator;

	// Token: 0x04000194 RID: 404
	public Image correctCheckbox;

	// Token: 0x04000195 RID: 405
	private Color checkedColor = new Color(0.39f, 0.39f, 0.39f);

	// Token: 0x04000196 RID: 406
	private bool answered;

	// Token: 0x04000197 RID: 407
	public bool bothCorrect;
}
