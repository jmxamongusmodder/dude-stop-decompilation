using UnityEngine;

public class PuzzleStepOnCracks_Boot : Draggable
{
	public Transform crackColliders;
	public Collider2D footCollider;
	public PuzzleStepOnCracks_Cracks cracks;
	public Vector2 offset;
	public float maxDistance;
	public float maxAngle;
	public float rotationTime;
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;
	public Transform footprint;
	public float footprintShift;
	public Transform shoeAnimaton;
}
