using UnityEngine;

public class PuzzleBarkAtNight_CelestialBody : MonoBehaviour
{
	public AnimationCurve risingAnimation;
	public AnimationCurve settingAnimation;
	public float sunPeakTime;
	public float moonPeakTime;
	public float secondMoonPeakTime;
	public int days;
	public Transform night;
	public Transform sun;
	public Transform moon;
	public SpriteRenderer dog;
	public SpriteRenderer dogMouth;
	public float flyingDogWait;
	public Transform house;
	public float nightAlpha;
	public float nightLerpTime;
	public float horizon;
	public float minPosition;
	public float peakPosition;
}
