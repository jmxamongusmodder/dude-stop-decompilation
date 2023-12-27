using UnityEngine;

public class PuzzleStats : MonoBehaviour
{
	public bool subtitlesBottom;
	public float subtitlesYShift;
	public float subtitlesBGalpha;
	public Transform background;
	public bool active;
	public bool isMenu;
	public bool loadUIOnStart;
	public Transform UIScreen;
	public Transform UIScreenCurr;
	public Transform UIScreenSecondary;
	public AudioVoice activeAudioVoice;
	public bool HasBadEnd;
	public bool HasGoodEnd;
	public bool goBadAfterTime;
	public float rapidFireTime;
	public bool doNotEndRapidFire;
	public bool hasJigSawPieces;
	public JigSaw_piece jigSawPieceOnPuzzle;
}
