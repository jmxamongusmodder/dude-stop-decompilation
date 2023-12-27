using UnityEngine;

public class PuzzleCupSmallest_animation : MonoBehaviour
{
	public AnimationCurve pickUpCurve;
	public float pickUpTime;
	public float pickUpMaxY;
	public AnimationCurve swapCurve;
	public float swapTime;
	public float swapMaxY;
	public float betweenSwaps;
	public Transform[] cupList;
	public Transform diamond;
	public GameObject[] setActive;
}
