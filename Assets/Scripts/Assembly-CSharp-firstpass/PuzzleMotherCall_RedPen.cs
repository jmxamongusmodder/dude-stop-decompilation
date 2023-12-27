using UnityEngine;

public class PuzzleMotherCall_RedPen : InventoryDraggable
{
	public Transform holder;
	public Vector2 holderOffset;
	public AnimationCurve insertionCurve;
	public float insertionTime;
	public float returnSpeed;
	public float snapDistance;
	public float scale;
	public int originalLayer;
	public int behindHolderLayer;
}
