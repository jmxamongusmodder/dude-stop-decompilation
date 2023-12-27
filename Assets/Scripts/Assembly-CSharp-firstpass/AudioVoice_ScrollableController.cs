using UnityEngine;
using UnityEngine.UI;

public class AudioVoice_ScrollableController : AudioVoice
{
	public float transitionSlowDownSpeed;
	public bool enableNextButton;
	public bool DebbugSkipVoicesOnLevel;
	public bool shortDuck;
	public float DebugShortWaitTimers;
	public bool DebbugSkipFirstScreen;
	public bool DebbugSkipSecondScreen;
	public bool DebbugSkipThirdScreen;
	public GameObject loadingScreen;
	public LineTranslator loadingText;
	public GameObject continueButton;
	public GameObject PCscreen;
	public Transform chatParent;
	public Transform voiceMessagePref;
	public Transform friendMessagePref;
	public GameObject typingNotification;
	public float distBetweenMsg;
	public float distBetweenSameMsg;
	public float textBorder;
	public Texture2D mouseCursor;
	public GameObject errorMsg;
	public Text errorMsgText;
	public GameObject errorButtonClose;
	public GameObject errorButtonDisctonnect;
	public GameObject popupMsg;
	public GameObject popupYesButton;
	public GameObject popupCancelButton;
	public Color barColorDefault;
	public Color barColorSelect;
	public Color barColorWarning;
	public float colorLerpSpeed;
	public AnimationCurve colorLerpCurve;
	public GameObject errorBar;
	public GameObject gameBar;
	public GameObject chatBar;
	public GameObject popupBar;
	public Animator cursorParent;
}
