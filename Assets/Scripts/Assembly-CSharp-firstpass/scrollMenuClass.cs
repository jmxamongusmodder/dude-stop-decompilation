using UnityEngine.UI;
using UnityEngine;

public class scrollMenuClass : AbstractUIScreen
{
	public Button prevButton;
	public Button nextButton;
	public Transform buttonList;
	public float buttonHeight;
	public int maxButtonCount;
	public float scrollTimeMax;
	public float scrollAmount;
	public float topPosToScroll;
	public float botPotToScroll;
	public ScrollRect scrollView;
	public float procToHideButtons;
	public float extraScroll;
}
