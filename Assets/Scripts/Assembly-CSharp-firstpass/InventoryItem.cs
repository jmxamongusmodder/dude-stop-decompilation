using UnityEngine;

public class InventoryItem : MonoBehaviour
{
	public Transform itemSprite;
	public float scale;
	public float moveBackSpeed;
	public Transform cleanSprite;
	public InventoryDraggable cleanDraggable;
	public Transform[] puzzleList;
	public InventoryDraggable[] draggableList;
}
