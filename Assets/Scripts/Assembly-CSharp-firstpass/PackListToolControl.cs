using UnityEngine;
using UnityEngine.UI;

public class PackListToolControl : MonoBehaviour
{
	public Color selectColor;
	public PackListToolColors lastSelectedColor;
	public Text currentSelect;
	public Transform itemPrefab;
	public Transform[] listParent;
	public GameObject[] puzzleList;
	public float minScale;
	public float maxScale;
	public float initialScale;
	public Vector2 maxShiftX;
	public Vector2 maxShiftY;
}
