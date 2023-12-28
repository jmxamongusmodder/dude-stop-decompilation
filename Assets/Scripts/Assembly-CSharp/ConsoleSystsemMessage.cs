using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200053A RID: 1338
public class ConsoleSystsemMessage : MonoBehaviour
{
	// Token: 0x06001E9C RID: 7836 RVA: 0x0008DE8B File Offset: 0x0008C28B
	private void OnEnable()
	{
		base.StartCoroutine(this.showText());
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x0008DE9C File Offset: 0x0008C29C
	protected virtual IEnumerator showText()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			yield break;
		}
		string text = base.GetComponent<LineTranslator>().currentText;
		Text txt = base.GetComponent<Text>();
		if (!text.Contains("..."))
		{
			yield break;
		}
		this.isLoading = true;
		int ind = text.IndexOf("...");
		float waitTime = 0.1f * this.typeSpeed;
		txt.text = text.Substring(0, ind);
		yield return new WaitForSeconds(waitTime);
		txt.text = text.Substring(0, ind + 1);
		yield return new WaitForSeconds(waitTime);
		txt.text = text.Substring(0, ind + 2);
		yield return new WaitForSeconds(waitTime);
		txt.text = text.Substring(0, ind + 3);
		yield return new WaitForSeconds(waitTime);
		this.setEndText(txt, text);
		this.isLoading = false;
		yield break;
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x0008DEB7 File Offset: 0x0008C2B7
	protected virtual void setEndText(Text txt, string text)
	{
		txt.text = text;
	}

	// Token: 0x040021D1 RID: 8657
	[HideInInspector]
	public bool isLoading;

	// Token: 0x040021D2 RID: 8658
	public float typeSpeed = 1f;
}
