using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class PuzzleMarcoPolo_Controller : MonoBehaviour
{
	// Token: 0x0600011B RID: 283 RVA: 0x0000ACDA File Offset: 0x00008EDA
	private void Start()
	{
		base.StartCoroutine(this.ControllerCoroutine());
		Audio.self.playOneShot("83e815c7-7355-4a4c-a51d-bf610bb5b825", 1f);
		PuzzleMarcoPolo_Controller.person = null;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000AD04 File Offset: 0x00008F04
	private void Update()
	{
		if (this.arrow != null)
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.arrow.position;
			float z = 270f - Mathf.Atan2(vector.x, vector.y) * 57.29578f;
			this.arrow.rotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000AD84 File Offset: 0x00008F84
	public void ChoiceClicked(bool monster)
	{
		if (!base.enabled)
		{
			return;
		}
		this.answers.gameObject.SetActive(false);
		if (monster)
		{
			Global.LevelCompleted(this.waitOnSilence, true);
			this.silence.gameObject.SetActive(true);
			this.silence.position = this.arrow.position + this.silenceOffset;
		}
		else
		{
			Audio.self.playOneShot("b1b9f187-5afd-40af-bc90-fcf2873dba01", 1f);
			Global.LevelFailed(this.waitOnText, true);
			this.answerBubble.gameObject.SetActive(true);
			this.answerBubble.position = this.arrow.position + this.answerBubbleOffset;
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000AE54 File Offset: 0x00009054
	private IEnumerator ControllerCoroutine()
	{
		yield return new WaitForSeconds(this.waitOnStart);
		this.marco.gameObject.SetActive(true);
		foreach (Transform transform in this.people)
		{
			transform.GetComponent<Collider2D>().enabled = true;
		}
		while (PuzzleMarcoPolo_Controller.person == null)
		{
			yield return null;
		}
		this.answers.gameObject.SetActive(true);
		this.answers.position = PuzzleMarcoPolo_Controller.person.position + this.answersOffset;
		this.arrow = this.answers.GetChild(1);
		yield break;
	}

	// Token: 0x040001AA RID: 426
	[Header("People")]
	public Transform[] people;

	// Token: 0x040001AB RID: 427
	[Header("Bubbles")]
	public Transform silence;

	// Token: 0x040001AC RID: 428
	public Vector2 silenceOffset;

	// Token: 0x040001AD RID: 429
	public Transform answers;

	// Token: 0x040001AE RID: 430
	public Vector2 answersOffset;

	// Token: 0x040001AF RID: 431
	public Transform answerBubble;

	// Token: 0x040001B0 RID: 432
	public Vector2 answerBubbleOffset;

	// Token: 0x040001B1 RID: 433
	public Transform marco;

	// Token: 0x040001B2 RID: 434
	[Header("Waits")]
	public float waitOnStart;

	// Token: 0x040001B3 RID: 435
	public float waitOnText = 1f;

	// Token: 0x040001B4 RID: 436
	public float waitOnSilence = 3f;

	// Token: 0x040001B5 RID: 437
	public static Transform person;

	// Token: 0x040001B6 RID: 438
	private Transform arrow;
}
