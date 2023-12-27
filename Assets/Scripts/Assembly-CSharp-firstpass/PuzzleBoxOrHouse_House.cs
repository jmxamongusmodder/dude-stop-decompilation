using UnityEngine;

public class PuzzleBoxOrHouse_House : PivotDraggable
{
	public Animator ceilingCat;
	public Transform cat;
	public Transform boxBottomCollider;
	public float bottomColliderOffset;
	public float movingOutTime;
	public Vector2 tableSnap;
	public float snapDist;
	public Collider2D leftLeg;
	public Collider2D rightLeg;
	public GameObject nailFiller;
}
