using System;
using UnityEngine;

[Serializable]
public class PuzzleKeysPhone_Object : InventoryDraggable
{
	public float leftSnapX;
	public float rightSnapX;
	public float pocketY;
	public float snapDistance;
	public AnimationCurve insertionCurve;
	public float minSnapY;
	public float minMoveY;
	public float successY;
	public Transform smallSprite;
	public Transform bigSprites;
}
