using UnityEngine;

public class PuzzleNonWaitingBus_Door : MonoBehaviour
{
	public bool isOpen;
	public Animator passenger;
	public Transform latePassenger;
	public bool blocked;
	public bool waitingOutside;
	public bool twoAreIn;
	public bool everybodyIsIn;
	public float hoverWhiten;
	public Transform duckSprite;
}
