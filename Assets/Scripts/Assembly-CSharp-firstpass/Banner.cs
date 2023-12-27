using UnityEngine;

public class Banner : MonoBehaviour
{
	public SpriteRenderer bannerSprite;
	public SpriteRenderer rollSprite;
	public SpriteRenderer lineSprite;
	public SpriteRenderer shadowSprite;
	public Transform bannerRoll;
	public Transform rollContainer;
	public Sprite bannerGood;
	public Sprite rollGood;
	public Sprite lineGood;
	public Sprite bannerBad;
	public Sprite rollBad;
	public Sprite lineBad;
	public GameObject particlesGood;
	public GameObject particlesBad;
	public Vector2 rollStartPosition;
	public Vector2 rollEndPosition;
	public float showShadowMarkY;
	public float shiftBadPosX;
	public float curveSpeed;
	public AnimationCurve showCurve;
	public AnimationCurve bannerCurve;
	public float bannerDist;
	public AnimationCurve rollScaleCurve;
	public int cupLayer;
}
