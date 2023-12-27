using UnityEngine;
using System.Collections.Generic;

public class PuzzleJigsaw_Controller : MonoBehaviour
{
	public float waitBetweenPieces;
	public AnimationCurve shuffleMove;
	public float screenOffset;
	public float bottomOffset;
	public Vector2 randomOffset;
	public float waitBeforeExplosion;
	public List<JigsawPiece> pieces;
	public AnimationCurve unlockAnimation;
	public float waitBetweenUnlocks;
}
