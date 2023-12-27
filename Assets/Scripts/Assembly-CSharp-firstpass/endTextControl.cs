using UnityEngine;

public class endTextControl : AbstractUIScreen
{
	public Transform textLine;
	public float rndRotation;
	public float initialY;
	public float moveEachLineY;
	public RectTransform hintText;
	public float hintTimeWait;
	public float cupMoveTime;
	public float cupScaleTime;
	public Vector2 cupWobbleEach;
	public float cupWobbleEachCurr;
	public float cupWobbleDistance;
	public float cupWobbleSpeed;
	public float cupMoveTargetShiftY;
	public float angleSpeedMax;
	public AnimationCurve cupFirstLapSpeed;
	public AnimationCurve cupLastLapSpeed;
	public float cupAngleWobbleSpeed;
	public Vector2 cupAngleWobbleEach;
	public float cupAngleWobbleEachCurr;
	public float cupAngleWobbleDistance;
	public Transform bannerPref;
}
