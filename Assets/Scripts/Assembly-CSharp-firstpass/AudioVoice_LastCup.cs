using UnityEngine;

public class AudioVoice_LastCup : AudioVoice
{
	public StandaloneLevelVoice endLine;
	public bool DebugSkipLine;
	public ParticleSystem firework;
	public ParticleSystem confety;
	public Color[] fireworkColors;
	public AnimationCurve fireworkIntensity;
	public float fireworkAmount;
	public CupLastCup_Controller controller;
	public bool canRemoveCup;
	public Transform newBG;
	public bool skipSilence;
	public float awkwardSilenceMax;
}
