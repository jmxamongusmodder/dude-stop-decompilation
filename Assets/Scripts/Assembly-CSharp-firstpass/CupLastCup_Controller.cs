using UnityEngine;

public class CupLastCup_Controller : MonoBehaviour
{
	public Transform signParent;
	public Rigidbody2D sign;
	public AnimationCurve signCurve;
	public float signForce;
	public Transform cup1;
	public float cup1force;
	public Transform cup2;
	public float cup2force;
	public Transform cup3;
	public AnimationCurve cup3animation;
	public float cup3force;
	public Vector2 cupOffset;
	public float torque;
	public Transform[] cupList;
	public Vector2 rndTorque;
	public float scaleDownTime;
}
