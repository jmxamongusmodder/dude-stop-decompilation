using UnityEngine;

public class CupLastCup_Sign : MonoBehaviour
{
	public ParticleSystem sparks;
	public CupLastCup_Controller controller;
	public Transform weakLink;
	public float weakLinkLastPosition;
	public float[] weakLinkPositions;
	public SpriteRenderer weakLinkSprite;
	public float releaseDistance;
	public float releaseForce;
	public float releaseForceY;
	public bool checkJoints;
	public float canDragEach;
	public Vector2 allowedPosition;
	public float allowedDistance;
	public Transform awardCup;
	public Transform frontGlass;
	public Transform backGlass;
	public Transform brokenGlass;
	public Vector2 brokenGlassForce;
	public float brokenGlassTorque;
	public Vector2 awardCupCollisionForce;
	public Transform[] deletedJoints;
}
