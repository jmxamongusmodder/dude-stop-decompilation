using UnityEngine;
using System.Collections.Generic;

public class PuzzleFridgePaintings : Draggable
{
	public AnimationCurve binCurve;
	public Transform crumpledPaper;
	public float crumplingTime;
	public float crumplingScale;
	public List<Transform> magnets;
	public Transform idealMagnet;
	public float rotationTime;
	public float snapDist;
	public float throwUpY;
	public float fridgeScale;
	public float fridgeScaleDistance;
	public float returnTime;
}
