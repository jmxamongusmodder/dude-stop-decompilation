using UnityEngine;

public class PuzzleCatDoor_Cat : Draggable
{
	public Transform door;
	public float mouseClickDistance;
	public Transform meow;
	public float meowDuration;
	public float meowRotation;
	public float meowMinScale;
	public float meowMaxScale;
	public float meowMouth;
	public float meowCount;
	public float length;
	public float amplitude;
	public float rotationTime;
	public float scaleTime;
	public float meowStartPosition;
	public float maxPositionBeforeDoor;
	public float automaticExitPosition;
	public float maxPositionAfterDoor;
	public float waitAfterOpenDoor;
	public float waitAfterEnd;
	public float waitAfterBrokenWindow;
	public Animator jumpingAnimation;
	public Transform jumpingCat;
	public float positionBeforeFlip;
	public float speed;
	public Transform sleepingCat;
	public Transform arrow;
}
