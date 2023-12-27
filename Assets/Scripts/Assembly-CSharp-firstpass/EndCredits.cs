using UnityEngine;
using UnityEngine.UI;

public class EndCredits : AbstractUIScreen
{
	public RectTransform hintText;
	public RectTransform scroll;
	public float scrollSpeed;
	public Vector2 duckLoadSpeed;
	public bool DebbugSkipDuck;
	public GameObject duckScreen;
	public GameObject buttonContinue;
	public GameObject buttonYes;
	public Text duckText;
	public Vector2 typeSpeed;
	public Vector2 blinkSpeed;
	public float startSlow;
	public float slowLength;
}
