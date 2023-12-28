using System;
using System.Collections.Generic;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000559 RID: 1369
public class LineTranslator : MonoBehaviour
{
	// Token: 0x06001F71 RID: 8049 RVA: 0x000968D0 File Offset: 0x00094CD0
	private void Awake()
	{
		if (this.components.Length > 0)
		{
			this.initialText = LineTranslator.GetTextWithStats(this.components[0]);
		}
		LineTranslator.SetFontAndSize(this.initialText, this.components);
		this.scriptControlled = false;
		this.translateText(false);
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x00096920 File Offset: 0x00094D20
	public static void SetFontAndSize(TextWithStats init, params Text[] list)
	{
		if (init == null)
		{
			return;
		}
		Font font = (!UIControl.self.useBackupFont) ? init.font : UIControl.self.backupFont;
		foreach (Text text in list)
		{
			if (!Global.self.pack10CutsceneActive || !(text.font == UIControl.self.backupFont))
			{
				if (font != null)
				{
					text.font = font;
				}
				text.fontSize = Mathf.RoundToInt((float)init.size * ((!(text.font == UIControl.self.defaultFontSubtitles)) ? UIControl.self.BigFontScale : UIControl.self.SmallFontScale));
				Outline component = text.GetComponent<Outline>();
				if (component != null)
				{
					component.effectDistance = init.outline * UIControl.self.OutlineScale;
				}
			}
		}
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x00096A2C File Offset: 0x00094E2C
	public static TextWithStats GetTextWithStats(Text text)
	{
		TextWithStats textWithStats = new TextWithStats
		{
			font = text.font,
			size = text.fontSize
		};
		if (text.GetComponent<Outline>() != null)
		{
			textWithStats.outline = text.GetComponent<Outline>().effectDistance;
		}
		return textWithStats;
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x00096A7C File Offset: 0x00094E7C
	public void setTextToTranslate(string GUID, WordTranslationContainer.Theme TYPE)
	{
		this.guid = GUID;
		this.type = TYPE;
		this.translateText(false);
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x00096A94 File Offset: 0x00094E94
	public void setTextNoTranslation(string text)
	{
		this.currentText = text;
		text += this.appendToTheEnd;
		text = this.appendToStart + text;
		text = text.Replace("(n)", "\n");
		foreach (Text text2 in this.components)
		{
			text2.text = text;
		}
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x00096AFC File Offset: 0x00094EFC
	public bool translateText(bool debbug = false)
	{
		if (this.previousFontWasBackup != UIControl.self.useBackupFont)
		{
			LineTranslator.SetFontAndSize(this.initialText, this.components);
			this.previousFontWasBackup = UIControl.self.useBackupFont;
		}
		if (this.scriptControlled)
		{
			return false;
		}
		string text = LineTranslator.translateText(this.guid, this.type, debbug, Global.self.currLanguage);
		if (text == null)
		{
			return false;
		}
		this.currentText = text;
		text += this.appendToTheEnd;
		text = this.appendToStart + text;
		if (this.canHaveErrors && !debbug)
		{
			text = LineTranslator.addErrorsToText(text);
		}
		foreach (Text text2 in this.components)
		{
			text2.text = text;
		}
		return true;
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x00096BD4 File Offset: 0x00094FD4
	public static string addErrorsToText(string txt)
	{
		if (string.IsNullOrEmpty(txt) || UIControl.self.useBackupFont)
		{
			return txt;
		}
		char errorChar = WordTranslationContainer.errorChar;
		if (!txt.Contains(errorChar.ToString()))
		{
			return txt;
		}
		List<int> list = new List<int>();
		int num = 0;
		foreach (char c in txt)
		{
			if (c == errorChar)
			{
				list.Add(num);
			}
			num++;
		}
		int startIndex = list[UnityEngine.Random.Range(0, list.Count)];
		txt = txt.Remove(startIndex, 1);
		string text2 = WordTranslationContainer.errorSwapList[errorChar];
		txt = txt.Insert(startIndex, text2[UnityEngine.Random.Range(0, text2.Length)].ToString());
		return txt;
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x00096CBC File Offset: 0x000950BC
	public static string translateText(string GUID, WordTranslationContainer.Theme type, bool debbug = false, string lang = "")
	{
		if (string.IsNullOrEmpty(GUID) || Global.self == null)
		{
			return string.Empty;
		}
		if (lang == string.Empty)
		{
			lang = Global.self.currLanguage;
		}
		string text = WordTranslationContainer.Get(type, GUID.ToUpper(), lang);
		if (text == null)
		{
			if (!debbug)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Can't tranlate ",
					GUID.ToUpper(),
					" from ",
					type,
					" to ",
					lang,
					";"
				}));
			}
			text = null;
		}
		else
		{
			text = text.Replace("(n)", "\n");
		}
		return text;
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x00096D80 File Offset: 0x00095180
	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.guid))
		{
			this.guid = this.guid.ToUpper();
			this.guid = this.guid.Replace(" ", "_");
		}
		if (this.components == null || this.components.Length == 0)
		{
			this.components = base.transform.GetComponentsInChildren<Text>();
			if (this.components == null || this.components.Length == 0)
			{
				Debug.LogWarning("Line translator must have texts!");
			}
		}
		if (!this.translateText(true))
		{
			string text = this.enText.Replace("(n)", "\n");
			text += this.appendToTheEnd;
			text = this.appendToStart + text;
			foreach (Text text2 in this.components)
			{
				text2.text = text;
			}
		}
	}

	// Token: 0x040022A7 RID: 8871
	public bool scriptControlled;

	// Token: 0x040022A8 RID: 8872
	[Space(10f)]
	public WordTranslationContainer.Theme type;

	// Token: 0x040022A9 RID: 8873
	public string guid;

	// Token: 0x040022AA RID: 8874
	[Multiline]
	public string enText;

	// Token: 0x040022AB RID: 8875
	[Space(10f)]
	public string comment;

	// Token: 0x040022AC RID: 8876
	[Space(10f)]
	public Text[] components;

	// Token: 0x040022AD RID: 8877
	[HideInInspector]
	public string currentText;

	// Token: 0x040022AE RID: 8878
	public bool canHaveErrors;

	// Token: 0x040022AF RID: 8879
	[Space(10f)]
	[Multiline]
	public string appendToTheEnd;

	// Token: 0x040022B0 RID: 8880
	public string appendToStart;

	// Token: 0x040022B1 RID: 8881
	private bool previousFontWasBackup;

	// Token: 0x040022B2 RID: 8882
	private TextWithStats initialText;
}
