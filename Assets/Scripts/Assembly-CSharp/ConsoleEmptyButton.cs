using System;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000528 RID: 1320
public class ConsoleEmptyButton : MonoBehaviour
{
	// Token: 0x06001E5A RID: 7770 RVA: 0x00089D0C File Offset: 0x0008810C
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
		if (UIControl.self.useBackupFont)
		{
			this.text.font = UIControl.self.backupFont;
			this.text.fontSize = Mathf.RoundToInt((float)this.text.fontSize * UIControl.self.SmallFontScale);
		}
		this.bPressed();
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x00089D78 File Offset: 0x00088178
	public void bPressed()
	{
		if (Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>())
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_CupDuck>().ClickUselessOptionsButton();
		}
		string text = LineTranslator.translateText(this.guid, this.type, false, string.Empty);
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		if (array.Length == 0)
		{
			this.text.text = "ERROR";
			return;
		}
		if (array.Length == 1)
		{
			this.text.text = text;
			return;
		}
		if (array.Length == 3)
		{
			int num = 0;
			if (int.TryParse(array[1], out num))
			{
				int num2 = 0;
				if (int.TryParse(array[2], out num2))
				{
					if (this.index < num || this.index > num2)
					{
						this.index = num;
					}
					this.text.text = string.Concat(new object[]
					{
						this.appendToStart,
						array[0].TrimEnd(new char[0]),
						" ",
						this.index
					});
					this.index++;
					return;
				}
			}
		}
		if (this.index == 0 || this.index >= array.Length)
		{
			this.index = 1;
		}
		this.text.text = this.appendToStart + array[0].TrimEnd(new char[0]) + " " + array[this.index];
		this.index++;
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x00089F0C File Offset: 0x0008830C
	private void OnValidate()
	{
		string text = LineTranslator.translateText(this.guid, this.type, true, string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		base.GetComponent<Text>().text = this.appendToStart + array[0];
	}

	// Token: 0x040021AD RID: 8621
	[Space(10f)]
	public WordTranslationContainer.Theme type;

	// Token: 0x040021AE RID: 8622
	public string guid;

	// Token: 0x040021AF RID: 8623
	[Space(10f)]
	public string appendToStart;

	// Token: 0x040021B0 RID: 8624
	private Text text;

	// Token: 0x040021B1 RID: 8625
	private int index;
}
