using UnityEngine;

public class PuzzleCoinsMissHand_Wallet : Draggable
{
	public float shakeDistance;
	public float shakeTime;
	public Vector2 throwForce;
	public float throwForceMin;
	public float throwTorque;
	public Transform[] firstCoins;
	public Transform[] secondCoins;
	public Transform[] thirdCoins;
	public SpriteRenderer[] coins;
	public int[] shakesPerCycle;
	public Transform fly;
}
