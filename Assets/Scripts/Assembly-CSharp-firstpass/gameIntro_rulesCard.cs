using System;
using ExcelData;
using UnityEngine;

// Token: 0x0200054B RID: 1355
public class gameIntro_rulesCard : AbstractUIScreen
{
	// Token: 0x06001F17 RID: 7959 RVA: 0x00093B0C File Offset: 0x00091F0C
	public override void setScreen(Transform item)
	{
		this.button.gameObject.SetActive(false);
		foreach (GameObject gameObject in this.slideList)
		{
			gameObject.SetActive(false);
		}
		this.slideList[this.slideIndex].SetActive(true);
		this.setCardName();
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x00093B6C File Offset: 0x00091F6C
	public void bNextSlide()
	{
		this.slideList[this.slideIndex].SetActive(false);
		this.slideIndex++;
		if (this.slideIndex >= this.slideList.Length)
		{
			this.slideIndex = 0;
		}
		this.slideList[this.slideIndex].SetActive(true);
		this.setCardName();
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x00093BD0 File Offset: 0x00091FD0
	public void bPrevSlide()
	{
		this.slideList[this.slideIndex].SetActive(false);
		this.slideIndex--;
		if (this.slideIndex < 0)
		{
			this.slideIndex = this.slideList.Length - 1;
		}
		this.slideList[this.slideIndex].SetActive(true);
		this.setCardName();
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x00093C34 File Offset: 0x00092034
	private void setCardName()
	{
		this.cardNameText.setTextToTranslate(this.cardNameText.guid.Substring(0, this.cardNameText.guid.LastIndexOf("_") + 1) + this.slideIndex, WordTranslationContainer.Theme.PACK_MENU);
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x00093C85 File Offset: 0x00092085
	protected override void cancelPressed()
	{
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x00093C88 File Offset: 0x00092088
	public void bStart()
	{
		if (!this.active)
		{
			return;
		}
		Audio.self.StartMusic("757e3a0a-c20a-4728-ab16-74dc9cf91a6b");
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x04002256 RID: 8790
	public Transform button;

	// Token: 0x04002257 RID: 8791
	public GameObject[] slideList;

	// Token: 0x04002258 RID: 8792
	public LineTranslator cardNameText;

	// Token: 0x04002259 RID: 8793
	private int slideIndex;
}
