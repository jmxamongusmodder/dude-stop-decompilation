using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class exitMenu : AbstractUIScreen
{
	// Token: 0x06001EFB RID: 7931 RVA: 0x000931B6 File Offset: 0x000915B6
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x000931BE File Offset: 0x000915BE
	public override void setScreen(Transform item)
	{
		this.exitingText.SetActive(false);
		this.abortSavingText.SetActive(false);
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x000931D8 File Offset: 0x000915D8
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.right, true);
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x00093218 File Offset: 0x00091618
	public void bExit()
	{
		if (!this.active)
		{
			return;
		}
		if (this.abort)
		{
			Application.Quit();
			if (this.coroutine != null)
			{
				base.StopCoroutine(this.coroutine);
			}
		}
		else
		{
			if (SaveLoad.Saving)
			{
				Debug.LogError("Stil saving");
				this.coroutine = base.StartCoroutine(this.TryToExit());
				return;
			}
			Application.Quit();
		}
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0009328C File Offset: 0x0009168C
	private IEnumerator TryToExit()
	{
		this.active = false;
		this.confirmText.SetActive(false);
		this.exitingText.SetActive(true);
		this.buttonNo.SetActive(false);
		this.buttonYes.SetActive(false);
		float time = 0f;
		bool showen = false;
		while (SaveLoad.Saving)
		{
			time += Time.deltaTime;
			if (time > 3f && !showen)
			{
				showen = true;
				this.active = true;
				this.abort = true;
				this.exitingText.SetActive(false);
				this.abortSavingText.SetActive(true);
				this.buttonNo.SetActive(true);
				this.buttonYes.SetActive(true);
			}
			yield return null;
		}
		Application.Quit();
		yield break;
	}

	// Token: 0x04002242 RID: 8770
	public GameObject confirmText;

	// Token: 0x04002243 RID: 8771
	public GameObject exitingText;

	// Token: 0x04002244 RID: 8772
	public GameObject abortSavingText;

	// Token: 0x04002245 RID: 8773
	public GameObject buttonYes;

	// Token: 0x04002246 RID: 8774
	public GameObject buttonNo;

	// Token: 0x04002247 RID: 8775
	private bool abort;

	// Token: 0x04002248 RID: 8776
	private Coroutine coroutine;
}
