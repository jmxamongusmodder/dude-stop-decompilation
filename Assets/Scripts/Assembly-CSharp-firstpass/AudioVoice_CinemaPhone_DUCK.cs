using UnityEngine;

public class AudioVoice_CinemaPhone_DUCK : AudioVoice
{
	public bool debugForceOn;
	public StandaloneLevelVoice onLoad;
	public StandaloneLevelVoice onYesClick;
	public StandaloneLevelVoice oneStar;
	public StandaloneLevelVoice twoStars;
	public StandaloneLevelVoice threeStars;
	public StandaloneLevelVoice endText;
	public GameObject smileOrigin;
	public GameObject smileCopy;
	public GameObject phone;
	public float phoneMaxY;
	public float phoneSpeed;
	public GameObject phoneRecScreen;
	public GameObject phoneRecIcon;
	public SpriteRenderer screenSprite;
	public SpriteRenderer[] phoneGlow;
	public float phoneGlowShift;
	public Texture2D cursor;
}
