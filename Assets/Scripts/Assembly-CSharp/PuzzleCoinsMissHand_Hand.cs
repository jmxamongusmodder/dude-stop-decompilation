using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class PuzzleCoinsMissHand_Hand : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x0000A10C File Offset: 0x0000830C
	private void Start()
	{
		this.startPosition = base.transform.position;
		this.coins = this.GetComponentsInPuzzleStats(true);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0000A131 File Offset: 0x00008331
	private void Update()
	{
		this.CheckCoins();
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000A13C File Offset: 0x0000833C
	private void CheckCoins()
	{
		Transform activeCoin = this.GetActiveCoin();
		Vector2 a = (!(activeCoin != null)) ? this.startPosition : activeCoin.position;
		Vector2 vector = a - this.startPosition;
		vector = Vector2.ClampMagnitude(vector, this.shiftDistance);
		base.transform.position = Vector2.MoveTowards(base.transform.position, this.startPosition + vector, this.shiftSpeed * Time.deltaTime);
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000A1CC File Offset: 0x000083CC
	private Transform GetActiveCoin()
	{
		PuzzleCoinsMissHand_Coin puzzleCoinsMissHand_Coin = (from x in this.coins
		where x.IsDragged()
		select x).FirstOrDefault<PuzzleCoinsMissHand_Coin>();
		if (puzzleCoinsMissHand_Coin == null)
		{
			return null;
		}
		float num = Vector2.Distance(base.transform.position, puzzleCoinsMissHand_Coin.transform.position);
		if (num > this.reactDistance || num < this.reactDistanceMin)
		{
			return null;
		}
		return puzzleCoinsMissHand_Coin.transform;
	}

	// Token: 0x04000172 RID: 370
	public float reactDistance;

	// Token: 0x04000173 RID: 371
	public float reactDistanceMin;

	// Token: 0x04000174 RID: 372
	public float shiftDistance;

	// Token: 0x04000175 RID: 373
	public float shiftSpeed;

	// Token: 0x04000176 RID: 374
	[HideInInspector]
	public PuzzleCoinsMissHand_Coin[] coins;

	// Token: 0x04000177 RID: 375
	private Vector2 startPosition;
}
