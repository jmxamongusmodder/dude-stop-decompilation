using System;
using ExcelData;
using UnityEngine;

// Token: 0x0200050F RID: 1295
public static class TextFormating
{
	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x00085DE7 File Offset: 0x000841E7
	public static int AwardTextSize
	{
		get
		{
			return Mathf.RoundToInt(28f * UIControl.self.SmallFontScale);
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00085DFE File Offset: 0x000841FE
	public static int AwardEmptyLineSize
	{
		get
		{
			return Mathf.RoundToInt(10f * UIControl.self.SmallFontScale);
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x00085E15 File Offset: 0x00084215
	public static int AwardSubtextSize
	{
		get
		{
			return Mathf.RoundToInt(24f * UIControl.self.SmallFontScale);
		}
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x00085E2C File Offset: 0x0008422C
	public static string formatNotAcquiredAward(string title, string descr, bool good = false, bool reqPrecent = false, bool reqNumber = false, bool cup100 = false)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Format("<color=white>{0}</color>", title.ToUpper()).AddTextSize(TextFormating.AwardTextSize);
		text3 += TextFormating.emptyLine(true, -1);
		text3 += TextFormating.preserveEmptyLines(descr);
		if (reqPrecent)
		{
			if (cup100)
			{
				text = "100%";
			}
			else
			{
				text = AwardController.self.getNeededProc(!good, false) + "%";
			}
			text2 = AwardController.self.getBestProgressProc(!good) + "%";
		}
		else
		{
			if (!reqNumber)
			{
				return text3;
			}
			text = TextFormating.getPackNeededCountToSolve();
			text2 = TextFormating.getPackNeededCountToSolve(good);
		}
		text3 += TextFormating.emptyLine(true, -1);
		if (good)
		{
			text3 += string.Format("<size={2}>{3}{0} <color=white>{1}</color></size>", new object[]
			{
				LineTranslator.translateText("NEEDED_GOOD", WordTranslationContainer.Theme.AWARD, false, string.Empty),
				text,
				TextFormating.AwardSubtextSize,
				" - "
			});
		}
		else
		{
			text3 += string.Format("<size={2}>{3}{0} <color=white>{1}</color></size>", new object[]
			{
				LineTranslator.translateText("NEEDED_BAD", WordTranslationContainer.Theme.AWARD, false, string.Empty),
				text,
				TextFormating.AwardSubtextSize,
				" - "
			});
		}
		return text3 + string.Format("\n<size={2}>{3}{0} <color=white>{1}</color></size>", new object[]
		{
			LineTranslator.translateText("BEST_RESULT", WordTranslationContainer.Theme.AWARD, false, string.Empty),
			text2,
			TextFormating.AwardSubtextSize,
			" - "
		});
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x00085FD4 File Offset: 0x000843D4
	public static string formatAcquiredAward(string title, string descr, bool good = false, bool reqPercent = false, bool reqNumber = false, bool cup100 = false)
	{
		string text = string.Empty;
		string text2 = string.Format("<color=white>{0}</color>", title.ToUpper()).AddTextSize(TextFormating.AwardTextSize);
		text2 += TextFormating.emptyLine(true, -1);
		text2 += TextFormating.preserveEmptyLines(descr);
		if (reqPercent)
		{
			if (cup100)
			{
				text = "100%";
			}
			else
			{
				text = AwardController.self.getNeededProc(!good, false) + "%";
			}
		}
		else
		{
			if (!reqNumber)
			{
				return text2;
			}
			text = TextFormating.getPackNeededCountToSolve();
		}
		text2 += TextFormating.emptyLine(true, -1);
		if (good)
		{
			text2 += string.Format("<size={2}>{3}{0} <color=white>{1}</color></size>", new object[]
			{
				LineTranslator.translateText("GOT_FOR_BEING_GOOD", WordTranslationContainer.Theme.AWARD, false, string.Empty),
				text,
				TextFormating.AwardSubtextSize,
				" - "
			});
		}
		else
		{
			text2 += string.Format("<size={2}>{3}{0} <color=white>{1}</color></size>", new object[]
			{
				LineTranslator.translateText("GOT_FOR_BEING_BAD", WordTranslationContainer.Theme.AWARD, false, string.Empty),
				text,
				TextFormating.AwardSubtextSize,
				" - "
			});
		}
		return text2;
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x00086110 File Offset: 0x00084510
	private static string preserveEmptyLines(string text)
	{
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (string.IsNullOrEmpty(array[i]))
			{
				array[i] = TextFormating.emptyLine(false, TextFormating.AwardTextSize);
			}
			else if (i == array.Length - 1)
			{
				if (!TextFormating.formatParentheses(ref array[i]))
				{
					array[i] = array[i].AddTextSize(TextFormating.AwardTextSize);
				}
			}
			else
			{
				array[i] = array[i].AddTextSize(TextFormating.AwardTextSize);
			}
		}
		return string.Join("\n", array);
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x000861AF File Offset: 0x000845AF
	public static string emptyLine(bool addNewLines = true, int size = -1)
	{
		if (size == -1)
		{
			size = TextFormating.AwardEmptyLineSize;
		}
		return string.Format("{0}<size={1}> </size>{0}", (!addNewLines) ? string.Empty : "\n", size);
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x000861E4 File Offset: 0x000845E4
	private static string getPackNeededCountToSolve()
	{
		int num2;
		int num3;
		int num = Global.self.CountPlayedPuzzlesInPack(out num2, out num3);
		return num + "/" + num;
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x00086218 File Offset: 0x00084618
	private static string getPackNeededCountToSolve(bool good)
	{
		int num2;
		int num3;
		int num = Global.self.CountPlayedPuzzlesInPack(out num2, out num3);
		if (good)
		{
			return num2 + "/" + num;
		}
		return num3 + "/" + num;
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x00086268 File Offset: 0x00084668
	private static bool formatParentheses(ref string text)
	{
		if (text.EndsWith(")"))
		{
			int startIndex = text.LastIndexOf("(");
			text = text.Remove(startIndex, 1);
			text = text.Remove(text.Length - 1, 1);
			string value = TextFormating.emptyLine(false, -1) + string.Format("\n<size={0}>{1}", TextFormating.AwardSubtextSize, " - ");
			text = text.Insert(startIndex, value) + "</size>";
			return true;
		}
		if (text.EndsWith("]"))
		{
			int startIndex2 = text.LastIndexOf("[");
			text = text.Remove(startIndex2, 1);
			text = text.Remove(text.Length - 1, 1);
			string value2 = TextFormating.emptyLine(false, -1) + string.Format("\n<size={0}>(", TextFormating.AwardSubtextSize);
			text = text.Insert(startIndex2, value2) + ")</size>";
			return true;
		}
		return false;
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x00086364 File Offset: 0x00084764
	public static string format(string txt)
	{
		if (string.IsNullOrEmpty(txt))
		{
			return string.Empty;
		}
		txt = txt.Replace("(blockCount)", "<color=white>" + CupLego_Global.UnusedPieces().ToString() + "</color>");
		return txt;
	}

	// Token: 0x04002121 RID: 8481
	public const int AWARD_TEXT_SIZE = 28;

	// Token: 0x04002122 RID: 8482
	private const int AWARD_EMPTY_LINE_SIZE = 10;

	// Token: 0x04002123 RID: 8483
	private const int AWARD_SUBTEXT_SIZE = 24;

	// Token: 0x04002124 RID: 8484
	public const string AWARD_SUBTEXT_START_CHARS = " - ";
}
