using System;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200051E RID: 1310
public class CollectData_Expand : MonoBehaviour
{
	// Token: 0x06001E06 RID: 7686 RVA: 0x0008754C File Offset: 0x0008594C
	private void Awake()
	{
		this.initialText = LineTranslator.GetTextWithStats(base.GetComponent<Text>());
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x0008755F File Offset: 0x0008595F
	private void Start()
	{
		this.SetLanguage();
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x00087568 File Offset: 0x00085968
	public void SetLanguage()
	{
		LineTranslator.SetFontAndSize(this.initialText, new Text[]
		{
			base.GetComponent<Text>()
		});
		this.longStr = LineTranslator.translateText("COLLECT_DATA_INFO", WordTranslationContainer.Theme.MENU, false, string.Empty);
		int num = this.longStr.IndexOf("(cut)");
		if (num >= 0)
		{
			this.shortStr = this.longStr.Substring(0, num);
			this.shortStr = this.shortStr + "(" + LineTranslator.translateText("READ_MORE", WordTranslationContainer.Theme.MENU, false, string.Empty) + ")";
			this.longStr = this.longStr.Replace("(cut)", string.Empty);
			this.longStr = this.longStr.Replace("  ", " ");
		}
		else
		{
			this.shortStr = this.longStr;
		}
		base.GetComponent<Text>().text = this.shortStr;
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x00087658 File Offset: 0x00085A58
	public void OnClick()
	{
		this.showLong = !this.showLong;
		if (this.showLong)
		{
			base.GetComponent<Text>().text = this.longStr;
		}
		else
		{
			base.GetComponent<Text>().text = this.shortStr;
		}
	}

	// Token: 0x04002149 RID: 8521
	private bool showLong;

	// Token: 0x0400214A RID: 8522
	private string shortStr = string.Empty;

	// Token: 0x0400214B RID: 8523
	private string longStr = string.Empty;

	// Token: 0x0400214C RID: 8524
	private TextWithStats initialText;
}
