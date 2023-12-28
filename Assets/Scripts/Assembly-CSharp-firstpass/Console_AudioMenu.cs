using System;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000524 RID: 1316
public class Console_AudioMenu : MonoBehaviour
{
	// Token: 0x06001E40 RID: 7744 RVA: 0x00089600 File Offset: 0x00087A00
	private void Awake()
	{
		this.setSubtitlesSizeText();
		this.setSubtitlesText();
		this.soundSlider.value = Audio.self.soundVolume;
		this.musicSlider.value = Audio.self.musicVolume;
		this.voiceSlider.value = Audio.self.voiceVolume;
		this.volumeSlider.value = Audio.self.masterVolume;
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x0008966D File Offset: 0x00087A6D
	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			optionsMenu.SaveAudioOptions();
		}
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x00089683 File Offset: 0x00087A83
	public void bBack()
	{
		global::Console.self.mouseOverClick();
		global::Console.self.switchMenu(global::Console.self.pauseMenu);
		optionsMenu.SaveAudioOptions();
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x000896A8 File Offset: 0x00087AA8
	public void applyVolume()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		if (this.volumeSlider.value == 0f || this.voiceSlider.value == 0f)
		{
			UIControl.setSubtitles(true);
			this.setSubtitlesText();
		}
		Audio.self.SetVolume(this.volumeSlider.value, this.voiceSlider.value, this.musicSlider.value, this.soundSlider.value);
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x00089731 File Offset: 0x00087B31
	public void bSubtitlesSize(bool prev)
	{
		if (!global::Console.self.active)
		{
			return;
		}
		UIControl.self.setSubtitlesFontSize(prev);
		this.setSubtitlesSizeText();
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x00089754 File Offset: 0x00087B54
	private void setSubtitlesSizeText()
	{
		this.subtitlesStatusSize.setTextToTranslate(UIControl.self.subtitlesSizeListName[UIControl.self.subtitlesSizeInd], WordTranslationContainer.Theme.MENU);
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x00089777 File Offset: 0x00087B77
	public void bSubtitles()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		UIControl.setSubtitles(!UIControl.self.showSubtitles);
		this.setSubtitlesText();
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x000897A1 File Offset: 0x00087BA1
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

	// Token: 0x040021A1 RID: 8609
	[Header("Volume sliders")]
	public Slider volumeSlider;

	// Token: 0x040021A2 RID: 8610
	public Slider voiceSlider;

	// Token: 0x040021A3 RID: 8611
	public Slider musicSlider;

	// Token: 0x040021A4 RID: 8612
	public Slider soundSlider;

	// Token: 0x040021A5 RID: 8613
	[Header("Option buttons")]
	public LineTranslator subtitlesStatusSize;

	// Token: 0x040021A6 RID: 8614
	public LineTranslator subtitlesStatus;
}
