using UnityEngine;

public class CupFastShoe_Controller : MonoBehaviour
{
	public Transform shoePrefab;
	public float waitBetweenFirstShoes;
	public Vector2 previewPosition;
	public float speedToPreviewPosition;
	public AnimationCurve peekabooCurve;
	public float peekabooReturnSpeed;
	public float boxY;
	public float timeToLive;
	public float firstX;
	public float firstY;
	public float minYIncrease;
	public float maxYIncrease;
	public float minTimer;
	public float maxTimer;
	public float flightSpeed;
	public float rotationSpeed;
	public AnimationCurve badShoeAppearance;
	public bool shoeClicked;
}
