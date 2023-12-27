using UnityEngine;
using UnityEngine.UI;

public class DuckPopup : MonoBehaviour
{
	public Animator duckAnimator;
	public RectTransform[] leftRightList;
	public RectTransform textBox;
	public RectTransform arrow;
	public Text textField;
	public RectTransform speechBubble;
	public RectTransform background;
	public AnimationCurve bubbleAppearCurve;
	public RectTransform yesButton;
	public RectTransform yesNoButton;
	public RectTransform ratingContainer;
	public RectTransform viewLastMsgButton;
}
