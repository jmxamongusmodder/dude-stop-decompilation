using UnityEngine;

public class PuzzleSlowLeftLine_Player : MonoBehaviour
{
	public PuzzleSlowLeftLine_MovingCar movingCar;
	public float movementSpeed;
	public bool limitLeft;
	public float leftX;
	public bool movedOut;
	public bool onLeftLane;
	public bool onScreen;
	public float thresholdSpeed;
}
