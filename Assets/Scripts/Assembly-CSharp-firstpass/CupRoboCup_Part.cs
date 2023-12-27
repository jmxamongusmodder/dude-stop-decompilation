using UnityEngine;

public class CupRoboCup_Part : Draggable
{
	public Transform cup;
	public Transform correctSpot;
	public CupRoboCup_Part prerequisite;
	public CupRoboCup_Part anotherPrerequisite;
	public float snapDist;
	public float returnMoveSpeed;
	public float returnLerpSpeed;
}
