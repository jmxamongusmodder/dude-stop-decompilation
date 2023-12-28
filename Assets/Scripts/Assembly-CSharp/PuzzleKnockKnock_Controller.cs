using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class PuzzleKnockKnock_Controller : MonoBehaviour
{
	// Token: 0x06000114 RID: 276 RVA: 0x0000AA8D File Offset: 0x00008C8D
	private void Start()
	{
		Audio.self.playOneShot("1c042b2b-7684-458f-b015-435cd86e60fd", 1f);
		this.rends = base.transform.GetComponentsInChildren<SpriteRenderer>();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000AAB8 File Offset: 0x00008CB8
	private void Update()
	{
		if (this.arrow.gameObject.activeSelf)
		{
			Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.arrow.position;
			float z = 270f - Mathf.Atan2(vector.x, vector.y) * 57.29578f;
			this.arrow.rotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000AB3C File Offset: 0x00008D3C
	public void ChoiceClicked(bool monster)
	{
		this.answers.gameObject.SetActive(false);
		if (monster)
		{
			Audio.self.playOneShot("2b4bcc98-458e-4abc-9b46-9fdd8576560f", 1f);
			Global.LevelCompleted(this.waitOnSilence, true);
			this.silence.gameObject.SetActive(true);
			this.normalFace.gameObject.SetActive(false);
			this.angryFace.gameObject.SetActive(true);
		}
		else
		{
			Audio.self.playOneShot("6fadf5ad-ee47-43a7-8802-ce86ee98dce9", 1f);
			Global.LevelFailed(this.waitOnText, true);
			this.answerBubble.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000ABEC File Offset: 0x00008DEC
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Alpha", this.whiten);
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000AC3C File Offset: 0x00008E3C
	private void OnMouseExit()
	{
		if (this.rends == null)
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in this.rends)
		{
			spriteRenderer.material.SetFloat("_Alpha", 0f);
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000AC89 File Offset: 0x00008E89
	private void OnMouseDown()
	{
		if (base.enabled && !this.answers.gameObject.activeSelf)
		{
			this.answers.gameObject.SetActive(true);
		}
	}

	// Token: 0x040001A0 RID: 416
	[Header("Other person's faces")]
	public Transform normalFace;

	// Token: 0x040001A1 RID: 417
	public Transform angryFace;

	// Token: 0x040001A2 RID: 418
	[Header("Answer stuff")]
	public Transform answers;

	// Token: 0x040001A3 RID: 419
	public Transform answerBubble;

	// Token: 0x040001A4 RID: 420
	public Transform silence;

	// Token: 0x040001A5 RID: 421
	public Transform arrow;

	// Token: 0x040001A6 RID: 422
	public float waitOnSilence = 3f;

	// Token: 0x040001A7 RID: 423
	public float waitOnText = 1f;

	// Token: 0x040001A8 RID: 424
	public float whiten;

	// Token: 0x040001A9 RID: 425
	private SpriteRenderer[] rends;
}
