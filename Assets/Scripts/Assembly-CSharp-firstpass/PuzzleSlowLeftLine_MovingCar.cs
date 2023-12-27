using UnityEngine;

public class PuzzleSlowLeftLine_MovingCar : MonoBehaviour
{
	public Transform secondCar;
	public PuzzleSlowLeftLine_Player player;
	public float movementSpeed;
	public float minDistanceToPlayer;
	public float distanceToSecondCar;
	public float deccelerationDistance;
	public float deccelerationRate;
	public float waitBeforeSecondCar;
	public float waitTime;
	public float waitBeforeEnding;
}
