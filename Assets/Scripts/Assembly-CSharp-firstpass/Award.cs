using UnityEngine;

public class Award : MonoBehaviour
{
	public AwardName awardName;
	public Transform cup;
	public Transform cupSilhouette;
	public string animationTrigger;
	public string animationSound;
	public bool hideSprite;
	public Transform[] showList;
	public Transform[] mouseOverList;
	public float whiteAmountMax;
	public PuzzleStats ps;
	public Transform[] animations;
}
