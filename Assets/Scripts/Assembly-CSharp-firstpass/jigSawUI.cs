using UnityEngine;
using UnityEngine.UI;

public class jigSawUI : AbstractUIScreen
{
	public RectTransform[] buttons;
	public GameObject noPiecesText;
	public GameObject shaffleButton;
	public RectTransform backButton;
	public Text jigsawCount;
	public RectTransform jigsawParent;
	public AnimationCurve jigsawCurve;
}
