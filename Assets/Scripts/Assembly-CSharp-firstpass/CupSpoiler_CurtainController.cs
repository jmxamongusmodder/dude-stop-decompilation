using UnityEngine;

public class CupSpoiler_CurtainController : MonoBehaviour
{
	public Transform cup;
	public Transform confetty;
	public Transform back;
	public Transform left;
	public Transform right;
	public AnimationCurve horizontalMovement;
	public float horizontalCurveTime;
	public AnimationCurve verticalMovement;
	public float verticalCurveTime;
	public float curtainWidth;
	public AnimationCurve firstLegenDaryCurve;
	public AnimationCurve secondLegenDaryCurve;
	public float timeAfterFirstCurtain;
	public float timeBetweenCurtains;
	public float timeBeforeFirstLegenDary;
	public float timeBeforeSecondLegenDary;
	public float timeAfterLegenDary;
	public float timeBeforeLast;
	public float topEndPos;
	public AnimationCurve lastCurve;
	public float timeBeforeLastCurve;
	public float elevation;
	public float colorChangeWaitSides;
	public float colorChangeWaitTop;
	public float colorChangeSpeed;
	public float minimalColorValue;
	public float reverseColorWait;
}
