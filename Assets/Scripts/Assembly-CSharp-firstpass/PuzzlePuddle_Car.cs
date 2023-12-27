using UnityEngine;

public class PuzzlePuddle_Car : MonoBehaviour
{
	public float thresholdSpeed;
	public float failX;
	public float turnOffX;
	public ParticleSystem particles;
	public Transform[] goodFaces;
	public Transform[] sadFaces;
	public float particleEmission;
	public float particleSpeed;
	public float particleSpeedMin;
	public float particleSpeedMax;
	public float particleSize;
	public float particleSizeMin;
	public float particleSizeMax;
	public bool levelEnded;
}
