using UnityEngine;

public class PuzzleStuckBalloon_StringPart : PivotDraggable
{
	public Transform lastStringPart;
	public Transform tree;
	public new Transform snapPoint;
	public float snapDist;
	public float unsnapDist;
	public float unsnapThreshold;
	public float waitAfterUnsnap;
	public float pullDistance;
	public float pullSpeed;
}
