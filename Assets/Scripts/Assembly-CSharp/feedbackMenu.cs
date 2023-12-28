using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000549 RID: 1353
public class feedbackMenu : AbstractUIScreen
{
	// Token: 0x06001F01 RID: 7937 RVA: 0x00093438 File Offset: 0x00091838
	public override void Update()
	{
		base.Update();
		if (this.active && this.feedbackCanvas.GetComponent<CanvasGroup>().alpha < 1f)
		{
			this.feedbackCanvas.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 5f;
		}
		if (this.hide && this.feedbackCanvas.GetComponent<CanvasGroup>().alpha > 0f)
		{
			this.feedbackCanvas.GetComponent<CanvasGroup>().alpha -= Time.deltaTime * 5f;
			if (this.feedbackCanvas.GetComponent<CanvasGroup>().alpha <= 0f)
			{
				Global.self.makeNewLevel(Global.self.exitMenu, Vector2.left, true);
			}
		}
		if (!this.sended && this.active)
		{
			this.sendButton.interactable = (this.text.text != string.Empty);
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x00093543 File Offset: 0x00091943
	protected override void cancelPressed()
	{
		this.bSkip();
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0009354B File Offset: 0x0009194B
	public override void removeScreen()
	{
		UnityEngine.Object.Destroy(this.feedbackCanvas);
		base.removeScreen();
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0009355E File Offset: 0x0009195E
	public override void setScreen(Transform item)
	{
		this.feedbackCanvas.transform.SetParent(null);
		this.feedbackCanvas.GetComponent<CanvasGroup>().alpha = 0f;
		this.sendButton.interactable = false;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x00093592 File Offset: 0x00091992
	public override void setActive(bool active)
	{
		this.feedbackCanvas.SetActive(active);
		base.setActive(active);
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x000935A7 File Offset: 0x000919A7
	public void bSend()
	{
		if (!this.active || this.sended)
		{
			return;
		}
		this.sending.text = "sending...";
		this.sendButton.interactable = false;
		this.sended = true;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x000935E3 File Offset: 0x000919E3
	public void bSkip()
	{
		if (!this.active)
		{
			return;
		}
		this.hide = true;
		this.active = false;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x000935FF File Offset: 0x000919FF
	public void onSuccess(bool action)
	{
		if (!base.enabled)
		{
			return;
		}
		if (action)
		{
			this.sending.text = "done";
		}
		else
		{
			this.sending.text = "error";
		}
	}

	// Token: 0x04002249 RID: 8777
	public Button sendButton;

	// Token: 0x0400224A RID: 8778
	public GameObject feedbackCanvas;

	// Token: 0x0400224B RID: 8779
	public Text text;

	// Token: 0x0400224C RID: 8780
	public Text sending;

	// Token: 0x0400224D RID: 8781
	private bool sended;

	// Token: 0x0400224E RID: 8782
	private bool hide;
}
