using UnityEngine;

public class PuzzleGIFvsJIF_Screwdriver : PivotDraggable
{
	public Transform bomb;
	public float angle;
	public float changeSpeed;
	public float returnTime;
	public GameObject defused;
	public SpriteRenderer blink;
	public AnimationCurve blinkCurve;
	public bool blinkFaster;
	public float blickFasterSpeed;
}
