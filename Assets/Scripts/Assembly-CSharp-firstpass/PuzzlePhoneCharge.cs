using UnityEngine;

public class PuzzlePhoneCharge : Draggable
{
	public AnimationCurve startAnimation;
	public Transform phonePercent;
	public int percent;
	public int percentOnSecondRun;
	public float permilleChangeTime;
	public Transform phoneCharge;
	public float phoneChargeMax;
	public Color chargingColor;
	public Color noChargeColor;
	public float noChargeWait;
	public float noChargeChangeTime;
	public float snapPointY;
	public float snapDistance;
}
