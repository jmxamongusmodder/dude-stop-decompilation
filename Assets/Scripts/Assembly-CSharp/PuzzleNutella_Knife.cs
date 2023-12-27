using UnityEngine;

public class PuzzleNutella_Knife : Draggable
{
	public SpriteRenderer bin;
	public float minimalSnapLine;
	public float snapDist;
	public bool canBeThrownOut;
	public Transform jar;
	public Rect jarEntryLimits;
	public float bottomJarLimit;
	public SpriteRenderer[] jarCreams;
	public int mouseMovements;
	public float verticalPosition;
	public float horizontalPosition;
	public float returnTime;
	public SpriteRenderer[] creamSprites;
	public SpriteRenderer knifeSprite;
	public Collider2D knifeBlade;
	public Collider2D bread;
	public Vector2 colliderOffset;
	public SpriteRenderer[] breadCreams;
}
