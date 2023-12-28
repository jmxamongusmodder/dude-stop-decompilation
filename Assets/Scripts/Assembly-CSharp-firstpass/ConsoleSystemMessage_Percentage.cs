using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000539 RID: 1337
public class ConsoleSystemMessage_Percentage : ConsoleSystsemMessage
{
	// Token: 0x06001E9A RID: 7834 RVA: 0x0008E100 File Offset: 0x0008C500
	protected override IEnumerator showText()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			yield break;
		}
		string text = base.GetComponent<LineTranslator>().currentText;
		Text txt = base.GetComponent<Text>();
		if (!text.Contains("0%"))
		{
			yield break;
		}
		this.isLoading = true;
		float waitTime = 0.01f;
		int proc = 0;
		int max = (!this.showError) ? 100 : 146;
		while (proc < max)
		{
			proc = (int)Mathf.MoveTowards((float)proc, (float)max, 3f);
			txt.text = text.Replace("0%", proc + "%");
			yield return new WaitForSeconds(waitTime);
			if ((double)UnityEngine.Random.value > 0.7)
			{
				yield return new WaitForSeconds(waitTime * 10f);
			}
			if ((double)UnityEngine.Random.value > 0.9)
			{
				yield return new WaitForSeconds(waitTime * 10f);
			}
		}
		if (this.showError)
		{
			this.isLoading = false;
			int index = 0;
			for (;;)
			{
				int num;
				index = (num = index) + 1;
				if (num >= 8)
				{
					break;
				}
				txt.text = text.Replace("0%", "101%");
				yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
				txt.text = text.Replace("0%", "100%");
				yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
			}
			txt.text = text.Replace("0%", "100%");
		}
		this.isLoading = false;
		yield break;
	}

	// Token: 0x040021D0 RID: 8656
	public bool showError;
}
