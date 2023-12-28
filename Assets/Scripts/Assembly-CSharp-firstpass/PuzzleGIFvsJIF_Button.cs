using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000410 RID: 1040
[EnabledManually]
public class PuzzleGIFvsJIF_Button : MonoBehaviour
{
	// Token: 0x06001A64 RID: 6756 RVA: 0x00067592 File Offset: 0x00065992
	private void Update()
	{
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x00067594 File Offset: 0x00065994
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.transform.GetChild(0).gameObject.SetActive(false);
		base.transform.GetChild(1).gameObject.SetActive(true);
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000675D0 File Offset: 0x000659D0
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.hover)
		{
			Global.self.canBePaused = false;
			base.StartCoroutine(this.ExplosionCoroutine());
			Audio.self.playOneShot("d88e1199-4f1b-49c5-b315-11c5d2238bd1", 1f);
		}
		else
		{
			base.transform.GetChild(0).gameObject.SetActive(true);
			base.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x00067654 File Offset: 0x00065A54
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		this.hover = true;
		base.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
		base.transform.GetChild(1).GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000676C0 File Offset: 0x00065AC0
	private void OnMouseExit()
	{
		if (!base.enabled)
		{
			return;
		}
		this.hover = false;
		base.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		base.transform.GetChild(1).GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x0006772C File Offset: 0x00065B2C
	private IEnumerator ExplosionCoroutine()
	{
		base.enabled = false;
		this.offButton.enabled = false;
		this.secondButton.enabled = false;
		Audio.self.playOneShot("96fe8652-4858-4fdd-87b0-ae4cf29f3421", 1f);
		this.screwdriver.GetComponent<PuzzleGIFvsJIF_Screwdriver>().blinkFaster = true;
		yield return new WaitForSeconds(4.5f);
		base.transform.SetParent(this.controller.parent);
		foreach (SpriteRenderer spriteRenderer in base.transform.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.enabled = false;
		}
		base.transform.GetComponent<Collider2D>().enabled = false;
		this.blackScreen.gameObject.SetActive(true);
		this.screwdriver.gameObject.SetActive(false);
		this.controller.gameObject.SetActive(false);
		this.bomb.gameObject.SetActive(false);
		this.dust.gameObject.SetActive(true);
		JigSaw_piece[] ppList = this.GetComponentsInPuzzleStats(false);
		foreach (JigSaw_piece jigSaw_piece in ppList)
		{
			jigSaw_piece.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(this.fadeWait);
		float timer = 0f;
		SpriteRenderer rend = this.blackScreen.GetComponent<SpriteRenderer>();
		Color color = rend.color;
		Color newColor = color;
		newColor.a = 0f;
		while (timer != this.fadeOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.fadeOutTime, Time.deltaTime);
			rend.color = Color.Lerp(color, newColor, timer / this.fadeOutTime);
			yield return null;
		}
		Global.self.canBePaused = true;
		Global.LevelCompleted(0f, true);
		yield break;
	}

	// Token: 0x04001877 RID: 6263
	public Transform blackScreen;

	// Token: 0x04001878 RID: 6264
	public Transform dust;

	// Token: 0x04001879 RID: 6265
	public Transform screwdriver;

	// Token: 0x0400187A RID: 6266
	public Transform controller;

	// Token: 0x0400187B RID: 6267
	public Transform bomb;

	// Token: 0x0400187C RID: 6268
	public PuzzleGIFvsJIF_OnOff offButton;

	// Token: 0x0400187D RID: 6269
	public PuzzleGIFvsJIF_Button secondButton;

	// Token: 0x0400187E RID: 6270
	public float fadeOutTime = 2f;

	// Token: 0x0400187F RID: 6271
	public float fadeWait = 3f;

	// Token: 0x04001880 RID: 6272
	[Header("Hover")]
	public float whiten = 0.25f;

	// Token: 0x04001881 RID: 6273
	private bool hover;
}
