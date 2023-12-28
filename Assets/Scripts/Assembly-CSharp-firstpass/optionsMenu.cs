using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000564 RID: 1380
public class optionsMenu : AbstractUIScreen
{
	// Token: 0x06001FB3 RID: 8115 RVA: 0x00098740 File Offset: 0x00096B40
	public override void Update()
	{
		base.Update();
		if (!this.active)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (Input.GetButtonDown("Horizontal") && currentSelectedGameObject != null)
		{
			float num = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
			if (currentSelectedGameObject == this.sound.gameObject)
			{
				this.soundSlider.value = Mathf.Round((this.soundSlider.value + 0.051f * num) * 10f) * 0.1f;
				Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
			}
			else if (currentSelectedGameObject == this.music.gameObject)
			{
				this.musicSlider.value = Mathf.Round((this.musicSlider.value + 0.051f * num) * 10f) * 0.1f;
				Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
			}
			else if (currentSelectedGameObject == this.voice.gameObject)
			{
				this.voiceSlider.value = Mathf.Round((this.voiceSlider.value + 0.051f * num) * 10f) * 0.1f;
				Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
			}
			else if (currentSelectedGameObject == this.volume.gameObject)
			{
				this.volumeSlider.value = Mathf.Round((this.volumeSlider.value + 0.051f * num) * 10f) * 0.1f;
				Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
			}
			else if (currentSelectedGameObject == this.language.gameObject)
			{
				this.bLanguage(num < 0f);
			}
			else if (currentSelectedGameObject == this.subtitleSize.gameObject)
			{
				this.bSubtitlesSize(num < 0f);
			}
			else
			{
				optionsButton component = currentSelectedGameObject.GetComponent<optionsButton>();
				if (component != null)
				{
					component.bSubmit();
				}
			}
		}
		this.voiceScript.OnVoiceVolumeChanged(this.voiceSlider.value);
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x00098992 File Offset: 0x00096D92
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x0009899C File Offset: 0x00096D9C
	public override void setScreen(Transform item)
	{
		this.voiceScript = item.GetComponent<AudioVoice_Options>();
		this.SetFullscreenText();
		this.SetLockMouseText();
		this.SetResolutionText();
		this.SetSubtitlesText();
		this.SetCollectDataText();
		this.SetSubtitlesSizeText();
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
		this.SetLanguageText();
		this.linesList = base.transform.GetComponentsInChildren<LineTranslator>();
		this.soundSlider.value = Audio.self.soundVolume;
		this.musicSlider.value = Audio.self.musicVolume;
		this.voiceSlider.value = Audio.self.voiceVolume;
		this.volumeSlider.value = Audio.self.masterVolume;
		if (!Global.self.isGameIntroActive)
		{
			this.newGameButton.gameObject.SetActive(false);
			Vector2 anchoredPosition = this.backButton.anchoredPosition;
			anchoredPosition.x = 0f;
			this.backButton.anchoredPosition = anchoredPosition;
		}
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x00098AEF File Offset: 0x00096EEF
	public void bAdvcancedOptions()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.advancedOptionsMenu, Vector2.down, true);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x00098B17 File Offset: 0x00096F17
	public override void setActive(bool active)
	{
		base.setActive(active);
		base.GetComponent<ButtonControl>().enabled = active;
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x00098B2C File Offset: 0x00096F2C
	public void ApplyVolume()
	{
		if (!this.active)
		{
			return;
		}
		if (this.volumeSlider.value == 0f || this.voiceSlider.value == 0f)
		{
			UIControl.self.showSubtitles = true;
			this.SetSubtitlesText();
		}
		Audio.self.SetVolume(this.volumeSlider.value, this.voiceSlider.value, this.musicSlider.value, this.soundSlider.value);
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00098BB8 File Offset: 0x00096FB8
	public void bCollectData()
	{
		if (!this.active)
		{
			return;
		}
		if (AnalyticsComponent.analyticsEnabled)
		{
			AnalyticsComponent.OptionsChanged("DATA_COLLECTION", false);
			AnalyticsComponent.Flush(false);
		}
		else
		{
			AnalyticsComponent.OptionsChanged("DATA_COLLECTION", true);
		}
		AnalyticsComponent.analyticsEnabled = !AnalyticsComponent.analyticsEnabled;
		this.SetCollectDataText();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x00098C24 File Offset: 0x00097024
	private void SetCollectDataText()
	{
		if (AnalyticsComponent.analyticsEnabled)
		{
			this.collectDataStatus.setTextToTranslate("ON", WordTranslationContainer.Theme.MENU);
		}
		else
		{
			this.collectDataStatus.setTextToTranslate("OFF", WordTranslationContainer.Theme.MENU);
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x00098C58 File Offset: 0x00097058
	public void bFullscreen()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.fullscreen = !Global.self.fullscreen;
		this.SetFullscreenText();
		this.screenHeight = Screen.height;
		this.screenWidth = Screen.width;
		Screen.fullScreen = Global.self.fullscreen;
		base.StartCoroutine(this.SetDelayedScreenResolution());
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x00098CD8 File Offset: 0x000970D8
	private IEnumerator SetDelayedScreenResolution()
	{
		yield return new WaitForSeconds(0.5f);
		Screen.SetResolution(this.screenWidth, this.screenHeight, Global.self.fullscreen);
		if (Global.self.fullscreen)
		{
			optionsMenu.LockMouse();
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
		this.SetResolutionText();
		yield break;
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x00098CF3 File Offset: 0x000970F3
	private void SetFullscreenText()
	{
		if (Global.self.fullscreen)
		{
			this.fullscreenStatus.setTextToTranslate("ON", WordTranslationContainer.Theme.MENU);
		}
		else
		{
			this.fullscreenStatus.setTextToTranslate("OFF", WordTranslationContainer.Theme.MENU);
		}
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x00098D2C File Offset: 0x0009712C
	public void bLockMouse()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.lockMouse = !Global.self.lockMouse;
		if (Global.self.fullscreen)
		{
			optionsMenu.LockMouse();
		}
		this.SetLockMouseText();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x00098D8B File Offset: 0x0009718B
	public static void LockMouse()
	{
		if (Global.self.lockMouse)
		{
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x00098DAD File Offset: 0x000971AD
	private void SetLockMouseText()
	{
		if (Global.self.lockMouse)
		{
			this.lockMouseStatus.setTextToTranslate("ON", WordTranslationContainer.Theme.MENU);
		}
		else
		{
			this.lockMouseStatus.setTextToTranslate("OFF", WordTranslationContainer.Theme.MENU);
		}
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x00098DE8 File Offset: 0x000971E8
	public void bLanguage(bool prev)
	{
		if (!this.active)
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
		this.SetLanguageText();
		this.ReloadLanguageOnScreen();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x00098E96 File Offset: 0x00097296
	private void SetLanguageText()
	{
		this.languageStatus.setTextToTranslate("CURRENT_LANGUAGE", WordTranslationContainer.Theme.MENU);
		optionsMenu.SetLocalizationAuthor(this.languageAuthor);
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00098EB4 File Offset: 0x000972B4
	public static void SetLocalizationAuthor(LineTranslator lineTranslator)
	{
		string text = LineTranslator.translateText("LOCALIZATION_AUTHOR", WordTranslationContainer.Theme.CREDITS, true, string.Empty);
		if (!string.IsNullOrEmpty(text))
		{
			lineTranslator.gameObject.SetActive(true);
			lineTranslator.setTextNoTranslation(text);
		}
		else
		{
			lineTranslator.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00098F04 File Offset: 0x00097304
	private void ReloadLanguageOnScreen()
	{
		foreach (LineTranslator lineTranslator in this.linesList)
		{
			lineTranslator.translateText(false);
		}
		this.SetResolutionText();
		this.expandInfo.SetLanguage();
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00098F49 File Offset: 0x00097349
	public void bSubtitles()
	{
		if (!this.active)
		{
			return;
		}
		UIControl.self.showSubtitles = !UIControl.self.showSubtitles;
		this.SetSubtitlesText();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x00098F89 File Offset: 0x00097389
	private void SetSubtitlesText()
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

	// Token: 0x06001FC7 RID: 8135 RVA: 0x00098FC1 File Offset: 0x000973C1
	public void bSubtitlesSize(bool prev)
	{
		if (!this.active)
		{
			return;
		}
		UIControl.self.setSubtitlesFontSize(prev);
		this.SetSubtitlesSizeText();
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x00098FF5 File Offset: 0x000973F5
	private void SetSubtitlesSizeText()
	{
		this.subtitlesStatusSize.setTextToTranslate(UIControl.self.subtitlesSizeListName[UIControl.self.subtitlesSizeInd], WordTranslationContainer.Theme.MENU);
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x00099018 File Offset: 0x00097418
	public void bResolution()
	{
		if (!this.active)
		{
			return;
		}
		this.SaveOptions();
		Global.self.makeNewLevel(Global.self.resolutionMenu, Vector2.right, true);
		Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x00099068 File Offset: 0x00097468
	public void SetResolutionText()
	{
		this.resolutionStatus.setTextNoTranslation(string.Concat(new object[]
		{
			Screen.width,
			" x ",
			Screen.height,
			" @",
			Screen.currentResolution.refreshRate
		}));
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x000990CA File Offset: 0x000974CA
	public void bBack()
	{
		if (!this.active)
		{
			return;
		}
		this.SaveOptions();
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x000990F8 File Offset: 0x000974F8
	public void bNewGame()
	{
		if (!this.active)
		{
			return;
		}
		this.SaveOptions();
		loadMenu.StartNewGame();
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x00099114 File Offset: 0x00097514
	private void SaveOptions()
	{
		optionsMenu.SaveAudioOptions();
		SaveLoad.setStr("Language", Global.self.currLanguage);
		SaveLoad.setInt("LockMouse", (!Global.self.lockMouse) ? 0 : 1);
		SaveLoad.setInt("CollectData", (!AnalyticsComponent.analyticsEnabled) ? 0 : 1);
		SaveLoad.setInt("MeasureUnits", (!Global.self.metricSystem) ? 0 : 1);
		AnalyticsComponent.OptionsChanged("LANGUAGE", Global.self.currLanguage);
		AnalyticsComponent.OptionsChanged("FULLSCREEN", Global.self.fullscreen);
		AnalyticsComponent.OptionsChanged("RESOLUTION", Screen.currentResolution.ToString());
		AnalyticsComponent.OptionsChanged("LOCK_CURSOR", Global.self.lockMouse);
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x000991F0 File Offset: 0x000975F0
	public static void SaveAudioOptions()
	{
		SaveLoad.setInt("SoundVolume", (int)(Audio.self.soundVolume * 100f));
		SaveLoad.setInt("MusicVolume", (int)(Audio.self.musicVolume * 100f));
		SaveLoad.setInt("VoiceVolume", (int)(Audio.self.voiceVolume * 100f));
		SaveLoad.setInt("MasterVolume", (int)(Audio.self.masterVolume * 100f));
		SaveLoad.setInt("SubtitlesSize", UIControl.self.subtitlesSizeInd);
		SaveLoad.setInt("Subtitles", (!UIControl.self.showSubtitles) ? 0 : 1);
		AnalyticsComponent.OptionsChanged("MASTER_VOLUME", Mathf.RoundToInt(Audio.self.masterVolume * 100f));
		AnalyticsComponent.OptionsChanged("VOICE_VOLUME", Mathf.RoundToInt(Audio.self.voiceVolume * 100f));
		AnalyticsComponent.OptionsChanged("MUSIC_VOLUME", Mathf.RoundToInt(Audio.self.musicVolume * 100f));
		AnalyticsComponent.OptionsChanged("SOUND_VOLUME", Mathf.RoundToInt(Audio.self.soundVolume * 100f));
		AnalyticsComponent.OptionsChanged("SUBTITLES", UIControl.self.showSubtitles);
		AnalyticsComponent.OptionsChanged("SUBTITLES_SIZE", UIControl.self.subtitlesSizeInd);
	}

	// Token: 0x040022D8 RID: 8920
	public RectTransform backButton;

	// Token: 0x040022D9 RID: 8921
	public RectTransform newGameButton;

	// Token: 0x040022DA RID: 8922
	public Transform volume;

	// Token: 0x040022DB RID: 8923
	public Slider volumeSlider;

	// Token: 0x040022DC RID: 8924
	public Transform voice;

	// Token: 0x040022DD RID: 8925
	public Slider voiceSlider;

	// Token: 0x040022DE RID: 8926
	public Transform music;

	// Token: 0x040022DF RID: 8927
	public Slider musicSlider;

	// Token: 0x040022E0 RID: 8928
	public Transform sound;

	// Token: 0x040022E1 RID: 8929
	public Slider soundSlider;

	// Token: 0x040022E2 RID: 8930
	public LineTranslator fullscreenStatus;

	// Token: 0x040022E3 RID: 8931
	public LineTranslator lockMouseStatus;

	// Token: 0x040022E4 RID: 8932
	public LineTranslator resolutionStatus;

	// Token: 0x040022E5 RID: 8933
	public LineTranslator subtitlesStatus;

	// Token: 0x040022E6 RID: 8934
	public LineTranslator subtitlesStatusSize;

	// Token: 0x040022E7 RID: 8935
	public Transform language;

	// Token: 0x040022E8 RID: 8936
	public LineTranslator languageAuthor;

	// Token: 0x040022E9 RID: 8937
	public Transform subtitleSize;

	// Token: 0x040022EA RID: 8938
	public LineTranslator languageStatus;

	// Token: 0x040022EB RID: 8939
	public LineTranslator collectDataStatus;

	// Token: 0x040022EC RID: 8940
	public CollectData_Expand expandInfo;

	// Token: 0x040022ED RID: 8941
	public Transform advancedOptions;

	// Token: 0x040022EE RID: 8942
	private int screenHeight;

	// Token: 0x040022EF RID: 8943
	private int screenWidth;

	// Token: 0x040022F0 RID: 8944
	private string[] langList;

	// Token: 0x040022F1 RID: 8945
	private int langInd;

	// Token: 0x040022F2 RID: 8946
	private LineTranslator[] linesList;

	// Token: 0x040022F3 RID: 8947
	private AudioVoice_Options voiceScript;
}
