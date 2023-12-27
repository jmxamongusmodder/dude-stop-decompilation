using UnityEngine;
using UnityEngine.UI;

public class optionsMenu : AbstractUIScreen
{
	public RectTransform backButton;
	public RectTransform newGameButton;
	public Transform volume;
	public Slider volumeSlider;
	public Transform voice;
	public Slider voiceSlider;
	public Transform music;
	public Slider musicSlider;
	public Transform sound;
	public Slider soundSlider;
	public LineTranslator fullscreenStatus;
	public LineTranslator lockMouseStatus;
	public LineTranslator resolutionStatus;
	public LineTranslator subtitlesStatus;
	public LineTranslator subtitlesStatusSize;
	public Transform language;
	public LineTranslator languageAuthor;
	public Transform subtitleSize;
	public LineTranslator languageStatus;
	public LineTranslator collectDataStatus;
	public CollectData_Expand expandInfo;
	public Transform advancedOptions;
}
