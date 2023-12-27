using UnityEngine;

public class CupMonsterCup_Cup : MonoBehaviour
{
	public Transform collider;
	public float endPosition;
	public float risingSpeed;
	public float waitBeforeAppearing;
	public Color cupTint;
	public float removeTintTime;
	public ParticleSystem[] boilParticles;
	public ParticleSystem dropsParticles;
	public GameObject cupObj;
	public Transform potSprites;
	public float shakeTimeMax;
	public float timerMin;
	public float timerMax;
	public float amountMin;
	public float amountMax;
	public float burstDelayMin;
	public float burstDelayMax;
	public float burstSlowMin;
	public float burstSlowMax;
	public float burstSlowIncrease;
	public float burstSlowDecrease;
	public float burstTimeMin;
	public float burstTimeMax;
}
