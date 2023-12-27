using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Book : MonoBehaviour
{
	public Canvas canvas;
	[SerializeField]
	private RectTransform BookPanel;
	public Transform background;
	public Transform pageList;
	public Transform[] bookPages;
	public bool interactable;
	public bool enableShadowEffect;
	public float returnFlipSpeed;
	public int currentPage;
	public AnimationCurve bigPageShadowAlpha;
	public AnimationCurve innerPageShadowAlpha;
	public Image ClippingPlane;
	public Image NextPageClip;
	public RectTransform Shadow;
	public RectTransform ShadowLTR;
	public RectTransform Left;
	public RectTransform LeftNext;
	public RectTransform LeftHotspot;
	public RectTransform Right;
	public RectTransform RightNext;
	public RectTransform RightHotstop;
	public UnityEvent OnFlip;
}
