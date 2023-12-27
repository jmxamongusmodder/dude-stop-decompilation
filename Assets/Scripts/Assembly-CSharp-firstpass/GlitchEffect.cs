using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class GlitchEffect : ImageEffectBase
{
	public Texture2D displacementMap;
	public bool vertical;
	public Vector2 showEach;
	public Vector2 showFor;
	public float updateEach;
	public Vector2 scale;
	public float scaleShift;
	public float displacement;
	public float shiftSpeedMax;
	public float colorMaxAdd;
}
