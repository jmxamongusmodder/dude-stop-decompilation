using System;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000526 RID: 1318
public class Console_ContactingDeveloperMenu : MonoBehaviour
{
	// Token: 0x06001E4C RID: 7756 RVA: 0x000899F4 File Offset: 0x00087DF4
	private void Awake()
	{
		this.langList = Global.self.languageList.Split(new char[]
		{
			','
		});
		foreach (string a in this.langList)
		{
			if (a == Global.self.currLanguage)
			{
				break;
			}
			this.langInd++;
		}
		this.setLanguageText();
		this.setSubtitlesText();
		this.volumeSlider.value = Audio.self.masterVolume;
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x00089A89 File Offset: 0x00087E89
	public void applyVolume()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		Audio.self.SetMasterVolume(this.volumeSlider.value);
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x00089AB0 File Offset: 0x00087EB0
	public void bSubtitles()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		UIControl.setSubtitles(!UIControl.self.showSubtitles);
		this.setSubtitlesText();
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x00089ADA File Offset: 0x00087EDA
	public void setSubtitlesText()
	{
		if (UIControl.self.showSubtitles)
		{
			this.subtitlesStatus.setTextToTranslate("ON", WordTranslationContainer.Theme.MENU);
		}
		else
		{
			this.subtitlesStatus.setTextToTranslate("OFF", WordTranslationContainer.Theme.MENU);
		}
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x00089B14 File Offset: 0x00087F14
	public void bLanguage(bool prev)
	{
		if (!global::Console.self.active)
		{
			return;
		}
		if (prev)
		{
			this.langInd--;
		}
		else
		{
			this.langInd++;
		}
		if (this.langInd < 0)
		{
			this.langInd = this.langList.Length - 1;
		}
		if (this.langInd >= this.langList.Length)
		{
			this.langInd = 0;
		}
		Global.self.currLanguage = this.langList[this.langInd];
		this.setLanguageText();
		this.reloadLanguageOnScreen();
		Global.self.currPuzzle.GetComponent<AudioVoice_loadingScreen>().setSubtitles();
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x00089BC8 File Offset: 0x00087FC8
	private void reloadLanguageOnScreen()
	{
		LineTranslator[] componentsInChildren = base.transform.GetComponentsInChildren<LineTranslator>(true);
		foreach (LineTranslator lineTranslator in componentsInChildren)
		{
			lineTranslator.translateText(false);
		}
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x00089C04 File Offset: 0x00088004
	private void setLanguageText()
	{
		this.languageStatus.setTextToTranslate("CURRENT_LANGUAGE", WordTranslationContainer.Theme.MENU);
		optionsMenu.SetLocalizationAuthor(this.localizationBy);
		global::Console.self.defaultInputField.GetComponent<LineTranslator>().translateText(false);
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x00089C38 File Offset: 0x00088038
	public void bMoreOptions()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		global::Console.self.hideConsole();
		Global.self.isGameIntroActive = true;
		Global.self.makeNewLevel(Global.self.optionsMenu, Vector2.right, true);
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x00089C84 File Offset: 0x00088084
	public void bStartPack()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		global::Console.self.hideConsole();
		loadMenu.StartNewGame();
	}

	// Token: 0x040021A7 RID: 8615
	public Slider volumeSlider;

	// Token: 0x040021A8 RID: 8616
	public LineTranslator subtitlesStatus;

	// Token: 0x040021A9 RID: 8617
	public LineTranslator languageStatus;

	// Token: 0x040021AA RID: 8618
	public LineTranslator localizationBy;

	// Token: 0x040021AB RID: 8619
	private string[] langList;

	// Token: 0x040021AC RID: 8620
	private int langInd;
}
