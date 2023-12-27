using UnityEngine;

public class CupFastSnail_Snail : MonoBehaviour
{
	public enum SnailType
	{
		none = 0,
		player = 1,
		faster = 2,
		slower = 3,
	}

	public float animatorMultiplier;
	public float originalSpeed;
	public float weightSpeed;
	public Transform weight;
	public SnailType type;
}
