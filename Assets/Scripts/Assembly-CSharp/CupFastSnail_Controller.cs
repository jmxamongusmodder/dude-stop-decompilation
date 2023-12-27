using UnityEngine;

public class CupFastSnail_Controller : MonoBehaviour
{
	public float firstThreshold;
	public float secondThreshold;
	public float thirdThreshold;
	public float lastThreshold;
	public Transform fastestSnail;
	public Transform otherSnail;
	public Transform playerSnail;
	public Transform finishLine;
	public float secondFinishLinePosition;
	public float waitBeforeTrophy;
	public Transform icon;
	public Transform uiIcon;
	public SpriteRenderer UIFill;
	public Transform countDown;
	public float fillInitial;
	public float fillOnClick;
	public float fillFallof;
	public float scaleFallof;
}
