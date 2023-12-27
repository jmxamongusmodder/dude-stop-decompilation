using UnityEngine;

public class PuzzleCatBreaksGlass_Cat : Draggable
{
	public bool glassShattered;
	public Transform dropoffAnimation;
	public float leftDropoffPoint;
	public float rightDropoffPoint;
	public Transform arrow;
	public Transform water;
	public float waterLimitOffset;
	public float pushBackDistance;
	public float pushBackTime;
	public float jumpTime;
	public float jumpHeight;
	public float length;
	public float amplitude;
	public float rotationTime;
	public float scaleTime;
	public StoryMode_TransitionCat transitionCat;
}
