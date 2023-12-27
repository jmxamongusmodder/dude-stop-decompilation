using UnityEngine;

public class JigsawPiece : Draggable
{
	public bool grouped;
	public bool finished;
	public bool center;
	public Vector2 centerOffset;
	public AnimationCurve centerMovement;
}
