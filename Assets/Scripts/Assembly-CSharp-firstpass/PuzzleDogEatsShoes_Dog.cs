using UnityEngine;

public class PuzzleDogEatsShoes_Dog : Draggable
{
	public Transform fridge;
	public int collisions;
	public float collisionForce;
	public Vector2 fridgeJump;
	public float requiredVelocity;
	public Transform slipper;
	public Transform inactiveSlipper;
	public float slipperJumpForce;
	public float minExplosion;
	public float maxExplosion;
	public Transform scene;
	public float sceneOffset;
	public AnimationCurve sceneChange;
	public Transform door;
	public Transform doorLine;
	public Transform doorThreshold;
	public float endMovementSpeed;
	public float mouthDistance;
	public float eatingDistance;
	public float length;
	public float amplitude;
	public float rotationTime;
	public float scaleTime;
	public Transform arrow;
}
