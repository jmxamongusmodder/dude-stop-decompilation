using UnityEngine;

public class CupFishingChest_Rod : MonoBehaviour
{
	public ParticleSystem bubbles;
	public SpriteRenderer[] rodSprites;
	public SpriteRenderer waterlineSprite;
	public CupFishingChest_Controller controller;
	public Transform line;
	public Transform lineStart;
	public float lineScaleQuotient;
	public Vector2 hookLineOffsets;
	public CupFishingChest_Hook hook;
	public AnimationCurve descendCurve;
	public AnimationCurve chestAscendCurve;
	public AnimationCurve stuffRotationCurve;
	public AnimationCurve junkAscendCurve;
	public float minX;
	public float maxX;
	public float mouseLerpSpeed;
	public float mouseChaseSpeed;
	public float mouseOffset;
	public Transform[] junkPool;
	public float junkFloatSpeed;
	public Vector2 junkThrowForce;
	public float minJunkTime;
	public float maxJunkTime;
	public float minJunkX;
	public float maxJunkX;
	public float junkWobbleAmplitude;
	public float junkDestructionTime;
	public Transform chest;
	public Transform sponge;
	public float chestY;
	public float chestGarbageOffset;
}
