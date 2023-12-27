using UnityEngine;

public class CupJigsawBad_Cup : MonoBehaviour
{
	public ParticleSystem particles;
	public Transform[] pieces;
	public Vector2[] pieceForces;
	public float[] pieceTorques;
	public float torque;
	public Transform shelf;
	public ParticleSystem shelfDust;
	public AnimationCurve rotationCurve;
	public float waitAfterRotation;
	public Vector2 shelfForce;
	public Transform anvil;
	public ParticleSystem anvilDust;
	public float minimalAnvilPosition;
	public float anvilHitDistance;
	public Vector2 anvilForce;
	public float anvilTorque;
	public float floorOffset;
}
