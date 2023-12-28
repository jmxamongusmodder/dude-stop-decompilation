using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class PuzzleFriendsPen_Paper : MonoBehaviour
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00066D64 File Offset: 0x00065164
	private bool firstQuestionFilled
	{
		get
		{
			return this.firstQuestionOne.fill + this.firstQuestionTwo.fill > this.firstQuestionFill;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00066D85 File Offset: 0x00065185
	private bool secondQuestionFilled
	{
		get
		{
			return this.secondQuestion.fill > this.secondQuestionFill;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00066D9A File Offset: 0x0006519A
	private bool thirdQuestionFilled
	{
		get
		{
			return this.thirdQuestionOne.fill + this.thirdQuestionTwo.fill + this.thirdQuestionThree.fill > this.thirdQuestionFill;
		}
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x00066DC8 File Offset: 0x000651C8
	private void Update()
	{
		if (this.finished)
		{
			this.GetComponentInPuzzleStats<PuzzleFriendsPen_Hand>().MoveOut();
			base.enabled = false;
			return;
		}
		if (!this.firstAnimatorEnabled && this.firstQuestionFilled)
		{
			Audio.self.playOneShot("32d0754c-b363-439d-8786-f87e9739eb8b", 1f);
			this.firstAnimatorEnabled = true;
			this.firstQuestionMark.enabled = true;
			this.firstQuestionMark.gameObject.SetActive(true);
			this.firstQuestionMark.SetTrigger("play");
			this.firstQuestionOne.EndDrawing();
			this.firstQuestionTwo.EndDrawing();
		}
		if (!this.secondAnimatorEnabled && this.secondQuestionFilled)
		{
			Audio.self.playOneShot("32d0754c-b363-439d-8786-f87e9739eb8b", 1f);
			this.secondAnimatorEnabled = true;
			this.secondQuestionMark.enabled = true;
			this.secondQuestionMark.gameObject.SetActive(true);
			this.secondQuestionMark.SetTrigger("play");
			this.secondQuestion.EndDrawing();
		}
		if (!this.thirdAnimatorEnabled && this.thirdQuestionFilled)
		{
			Audio.self.playOneShot("32d0754c-b363-439d-8786-f87e9739eb8b", 1f);
			this.thirdAnimatorEnabled = true;
			this.thirdQuestionMark.enabled = true;
			this.thirdQuestionMark.gameObject.SetActive(true);
			this.thirdQuestionMark.SetTrigger("play");
			this.thirdQuestionOne.EndDrawing();
			this.thirdQuestionTwo.EndDrawing();
			this.thirdQuestionThree.EndDrawing();
		}
		if (!this.answerAnimatorEnabled && this.checkbox5 == null)
		{
			Audio.self.playOneShot("32d0754c-b363-439d-8786-f87e9739eb8b", 1f);
			this.answerAnimatorEnabled = true;
			this.check4Mark.enabled = true;
			this.check4Mark.gameObject.SetActive(true);
			this.check4Mark.SetTrigger("play");
		}
		bool flag = false;
		if (!this.answerAnimatorEnabled && this.checkbox4 == null)
		{
			Audio.self.playOneShot("32d0754c-b363-439d-8786-f87e9739eb8b", 1f);
			this.answerAnimatorEnabled = true;
			this.check5Mark.enabled = true;
			this.check5Mark.gameObject.SetActive(true);
			this.check5Mark.SetTrigger("play");
			Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().answerWrong();
			flag = true;
		}
		if (!this.markAnimatorEnabled && this.firstQuestionFilled && this.secondQuestionFilled && this.thirdQuestionFilled)
		{
			if (!(this.checkbox4 != null) || !(this.checkbox5 != null))
			{
				if (this.checkbox4 != null)
				{
					base.StartCoroutine(this.MarkingCoroutine(this.AMark));
					if (!flag)
					{
						Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().endExam();
					}
				}
				else
				{
					base.StartCoroutine(this.MarkingCoroutine(this.FMark));
					if (!flag)
					{
						Global.self.currPuzzle.GetComponent<AudioVoice_FriendPen>().endExam();
					}
				}
			}
		}
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000670F8 File Offset: 0x000654F8
	private IEnumerator MarkingCoroutine(Animator target)
	{
		this.finished = true;
		Global.self.canBePaused = false;
		Global.PauseArrows(this.timeBeforeLastMark);
		this.markAnimatorEnabled = true;
		yield return new WaitForSeconds(this.timeBeforeLastMark);
		target.enabled = true;
		target.gameObject.SetActive(true);
		target.SetTrigger("play");
		Global.self.canBePaused = true;
		Audio.self.playOneShot("ce46e4f0-8161-462d-bfa3-d7449ec2973f", 1f);
		yield break;
	}

	// Token: 0x04001859 RID: 6233
	public float timeBeforeLastMark = 1f;

	// Token: 0x0400185A RID: 6234
	[Header("First question")]
	public DrawingCanvas firstQuestionOne;

	// Token: 0x0400185B RID: 6235
	public DrawingCanvas firstQuestionTwo;

	// Token: 0x0400185C RID: 6236
	public Animator firstQuestionMark;

	// Token: 0x0400185D RID: 6237
	private bool firstAnimatorEnabled;

	// Token: 0x0400185E RID: 6238
	public float firstQuestionFill;

	// Token: 0x0400185F RID: 6239
	[Header("Second question")]
	public DrawingCanvas secondQuestion;

	// Token: 0x04001860 RID: 6240
	public Animator secondQuestionMark;

	// Token: 0x04001861 RID: 6241
	private bool secondAnimatorEnabled;

	// Token: 0x04001862 RID: 6242
	public float secondQuestionFill;

	// Token: 0x04001863 RID: 6243
	[Header("Third question")]
	public DrawingCanvas thirdQuestionOne;

	// Token: 0x04001864 RID: 6244
	public DrawingCanvas thirdQuestionTwo;

	// Token: 0x04001865 RID: 6245
	public DrawingCanvas thirdQuestionThree;

	// Token: 0x04001866 RID: 6246
	public Animator thirdQuestionMark;

	// Token: 0x04001867 RID: 6247
	private bool thirdAnimatorEnabled;

	// Token: 0x04001868 RID: 6248
	public float thirdQuestionFill;

	// Token: 0x04001869 RID: 6249
	[Header("Checkboxes")]
	public Transform checkbox4;

	// Token: 0x0400186A RID: 6250
	public Transform checkbox5;

	// Token: 0x0400186B RID: 6251
	public Animator check4Mark;

	// Token: 0x0400186C RID: 6252
	public Animator check5Mark;

	// Token: 0x0400186D RID: 6253
	public Animator AMark;

	// Token: 0x0400186E RID: 6254
	public Animator FMark;

	// Token: 0x0400186F RID: 6255
	private bool answerAnimatorEnabled;

	// Token: 0x04001870 RID: 6256
	private bool markAnimatorEnabled;

	// Token: 0x04001871 RID: 6257
	private bool finished;
}
