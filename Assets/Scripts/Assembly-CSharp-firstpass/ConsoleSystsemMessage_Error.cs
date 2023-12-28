using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200053B RID: 1339
public class ConsoleSystsemMessage_Error : ConsoleSystsemMessage
{
	// Token: 0x06001EA0 RID: 7840 RVA: 0x0008E445 File Offset: 0x0008C845
	protected override void setEndText(Text txt, string text)
	{
		base.setEndText(txt, text);
		base.StartCoroutine(this.addError(txt, text));
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x0008E460 File Offset: 0x0008C860
	private IEnumerator addError(Text txt, string text)
	{
		int index = 0;
		bool error = true;
		int ind = text.IndexOf("...");
		string yes = text.Substring(ind + 3, text.Length - ind - 3);
		yes = yes.Trim();
		string wrongText = text.Replace(yes, "ERROR");
		for (;;)
		{
			int num;
			index = (num = index) + 1;
			if (num >= 15)
			{
				break;
			}
			error = !error;
			if (error)
			{
				txt.text = wrongText;
			}
			else
			{
				txt.text = text;
			}
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.4f));
			if (index == 5)
			{
				yield return new WaitForSeconds(1f);
			}
			if (index == 10)
			{
				yield return new WaitForSeconds(1f);
			}
		}
		txt.text = text;
		yield break;
	}
}
