using UnityEngine;

public class InventoryDraggable : PivotDraggable
{
	public bool returnToInventory;
	public bool returnToPoint;
	public Vector2 inventoryReturnPoint;
	public float inventoryReturnSpeed;
	public bool checkBounds;
}
