using UnityEngine;

public class PuzzleBigPowerSupply_Plug : EnhancedDraggable
{
	public Transform snapPointParent;
	public Transform startingPoint;
	public float insertionTime;
	public float removingTime;
	public float snapDist;
	public float finalPos;
	public float defaultBottomLimit;
	public SnapPoint lockedSnapPoint;
	public Vector2 savedLocalPosition;
	public float removeY;
	public float insertY;
}
