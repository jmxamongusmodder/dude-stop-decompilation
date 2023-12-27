using UnityEngine;

public class PuzzleDeodorant_Can : Draggable
{
	public float distance;
	public float secondsForNormal;
	public float secondsForStench;
	public float secondsForBad;
	public ParticleSystem stenchParticles;
	public float minStench;
	public float maxStench;
	public Transform human;
	public ParticleSystem particles;
	public bool enableTracking;
	public float lerpSpeed;
	public float moveSpeed;
}
