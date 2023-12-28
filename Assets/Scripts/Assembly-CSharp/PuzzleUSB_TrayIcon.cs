using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class PuzzleUSB_TrayIcon : MonoBehaviour
{
	// Token: 0x06001CE7 RID: 7399 RVA: 0x0007D605 File Offset: 0x0007BA05
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.StartCoroutine(this.ScalingCoroutine());
		this.popup.Next();
	}

	// Token: 0x06001CE8 RID: 7400 RVA: 0x0007D62C File Offset: 0x0007BA2C
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001CE9 RID: 7401 RVA: 0x0007D655 File Offset: 0x0007BA55
	private void OnMouseExit()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x06001CEA RID: 7402 RVA: 0x0007D674 File Offset: 0x0007BA74
	private IEnumerator ScalingCoroutine()
	{
		Vector3 originalScale = base.transform.localScale;
		Vector3 smallScale = originalScale * this.scaleFactor;
		float timer = 0f;
		while (timer != this.scaleTimeout)
		{
			timer = Mathf.MoveTowards(timer, this.scaleTimeout, Time.deltaTime);
			base.transform.localScale = Vector3.Lerp(smallScale, originalScale, timer / this.scaleTimeout);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001B7E RID: 7038
	public PuzzleUSB_Popup popup;

	// Token: 0x04001B7F RID: 7039
	[Range(0f, 1f)]
	public float whiten = 0.2f;

	// Token: 0x04001B80 RID: 7040
	public float scaleFactor = 0.95f;

	// Token: 0x04001B81 RID: 7041
	public float scaleTimeout = 0.1f;
}
