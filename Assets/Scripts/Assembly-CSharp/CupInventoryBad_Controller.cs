using UnityEngine;

public class CupInventoryBad_Controller : MonoBehaviour
{
	public CupInventoryBad_Potato potato;
	public float cupAppearanceTime;
	public Transform duck;
	public float waitBeforeDuck;
	public float duckStartOffset;
	public float duckEndOffset;
	public float duckMoveTime;
	public Transform blackScreen;
	public float blackScreenTime;
	public float blackScreenLerpTime;
	public float waitBeforeDropping;
	public float maxDropVelocity;
	public float dropTorque;
	public AnimationCurve dirtyCupMovement;
}
