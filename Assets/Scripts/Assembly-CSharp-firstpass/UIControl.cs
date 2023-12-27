using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
	public Sprite whiteSprite;
	public GameObject secondCanvas;
	public Transform chatParent;
	public Transform chatLine;
	public bool showChat;
	public bool useBackupFontEditor;
	public Font backupFont;
	public Font defaultFontSubtitles;
	[SerializeField]
	private float backupFontSubtitlesBGShiftY;
	[SerializeField]
	private float backupBigFontScale;
	[SerializeField]
	private float backupSmallFontScale;
	[SerializeField]
	private float backupOutlineScale;
	public float subtitlesExtraWaitTime;
	public GameObject packsGraph;
	public bool showSubtitles;
	public float[] subtitlesSizeList;
	public string[] subtitlesSizeListName;
	public int subtitlesSizeInd;
	public int subtitlesDefaultSize;
	public float subtitlesDefaultLineSpacing;
	public RectTransform subtitlesParent;
	public Text subtitles;
	public Image subtitlesBG;
	public Vector2 subtitlesBgBorder;
	public float subtitlesBgShiftY;
	public float subtitleBGalpha;
	public float waitPerWord;
	public float minSubtitlesTime;
	public bool subtitilesIgnoreInventory;
	public Transform endScreenParent;
	public Transform[] endScreenList;
	public CompletionStateControl completionLine;
	public timeLineControl timeLineUI;
	public RectTransform jigsawContainer;
	public RectTransform jigsawFlyPoint;
	public Text jigsawText;
	public float jigsawMaxScale;
	public float jigsawScaleDownSpeed;
	public AnimationCurve jigsawMoveCurve;
	public float jigsawMoveSpeed;
	public float jigsawShowTime;
	public float jigsawTopPos;
	public Console console;
	public bool timerEnabled;
	public UITimer timer;
	public bool showFPS;
	public Text fpsCounter;
	public float fpsFrequency;
	public Transform noConnectionScreen_Pregfab;
	public AchievementPopup achievementPopup;
	public Transform popupDuck;
	public NYCoinUI nyCoin;
}
