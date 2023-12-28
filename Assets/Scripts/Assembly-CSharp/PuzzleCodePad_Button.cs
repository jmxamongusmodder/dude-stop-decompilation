using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class PuzzleCodePad_Button : MonoBehaviour
{
	// Token: 0x060000D2 RID: 210 RVA: 0x00009638 File Offset: 0x00007838
	private void Awake()
	{
		this.pressed = base.transform.GetChild(0);
		this.background = base.transform.GetChild(1);
		this.cipher = base.transform.GetChild(2);
		this.display = this.GetComponentInPuzzleStats<PuzzleCodePad_Display>();
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00009687 File Offset: 0x00007887
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("e91b4d7c-6757-4405-add4-c9310992e2de", 1f);
		this.pressed.gameObject.SetActive(true);
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000096BB File Offset: 0x000078BB
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.pressed.gameObject.SetActive(false);
		this.ButtonDown();
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000096E0 File Offset: 0x000078E0
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		this.background.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", this.hover);
		this.cipher.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", this.hover);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00009739 File Offset: 0x00007939
	private void OnMouseExit()
	{
		this.background.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", 0f);
		this.cipher.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00009779 File Offset: 0x00007979
	protected virtual void ButtonDown()
	{
	}

	// Token: 0x04000158 RID: 344
	public float hover;

	// Token: 0x04000159 RID: 345
	protected Transform pressed;

	// Token: 0x0400015A RID: 346
	protected Transform background;

	// Token: 0x0400015B RID: 347
	protected Transform cipher;

	// Token: 0x0400015C RID: 348
	protected PuzzleCodePad_Display display;
}
