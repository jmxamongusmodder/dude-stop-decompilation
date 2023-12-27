using UnityEngine;

public class CompletionIcon : MonoBehaviour
{
	public RectTransform bg;
	public RectTransform win;
	public RectTransform lose;
	public float speed;
	public RectTransform banner;
	public ParticleSystem particles;
	public Vector2 bannerYLimit;
	public AnimationCurve bannerYCurve;
	public float bannerShowTimeMax;
	public float bannerAnimTimeMax;
}
