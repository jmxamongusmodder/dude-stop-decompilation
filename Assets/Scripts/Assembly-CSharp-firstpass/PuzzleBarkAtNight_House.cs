using UnityEngine;

public class PuzzleBarkAtNight_House : MonoBehaviour
{
	public float windowCooldownTime;
	public int maxClicks;
	public Transform topWindow;
	public int topWindowClicks;
	public Transform leftWindow;
	public int leftWindowClicks;
	public Transform rightWindow;
	public int rightWindowClicks;
	public Transform garbage;
	public int garbageMinClicks;
	public float garbageTimeToLive;
	public float garbageDisappearTime;
	public float singleGarbageProbability;
	public float doubleGarbageProbability;
	public float doubleGarbageWait;
	public float garbageScatter;
	public Vector2 garbageBaseForce;
	public Vector2 garbageRandomForce;
	public bool finished;
}
