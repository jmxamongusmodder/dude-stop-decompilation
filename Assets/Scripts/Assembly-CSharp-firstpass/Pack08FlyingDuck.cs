using UnityEngine;

public class Pack08FlyingDuck : MonoBehaviour
{
	public float destinationY;
	public float timeToFly;
	public float rotationSpeed;
	public AnimationCurve appearCurve;
	public AnimationCurve hideCurve;
	public GameObject particles;
	public AnimationCurve showNight;
	public float showTime;
	public SpriteRenderer nightSprite;
	public bool allowClick;
}
