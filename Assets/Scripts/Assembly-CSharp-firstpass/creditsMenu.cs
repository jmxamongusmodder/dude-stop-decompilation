using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class creditsMenu : AbstractUIScreen
{
	// Token: 0x06001EA5 RID: 7845 RVA: 0x0008E6DD File Offset: 0x0008CADD
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x0008E6E5 File Offset: 0x0008CAE5
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.right, true);
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x0008E70D File Offset: 0x0008CB0D
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x0008E70F File Offset: 0x0008CB0F
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.ScrollCredits());
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x0008E72C File Offset: 0x0008CB2C
	private IEnumerator ScrollCredits()
	{
		yield return new WaitForSeconds(1f);
		float posY = this.scroll.anchoredPosition.y;
		float dist = this.scroll.sizeDelta.y - UIControl.self.GetComponent<RectTransform>().sizeDelta.y;
		while (posY < dist)
		{
			posY = Mathf.MoveTowards(posY, dist, this.scrollSpeed * Time.deltaTime);
			this.scroll.anchoredPosition = this.scroll.anchoredPosition.setY(posY);
			if (Input.GetButtonDown("Cancel"))
			{
				this.bBack();
				yield break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x040021D4 RID: 8660
	public RectTransform scroll;

	// Token: 0x040021D5 RID: 8661
	public float scrollSpeed = 50f;
}
