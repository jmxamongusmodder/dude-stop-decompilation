using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000029 RID: 41
[EnabledManually]
public class PuzzleCoinsMissHand_Coin : EnhancedDraggable
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00009D6D File Offset: 0x00007F6D
	private SpriteRenderer sr
	{
		get
		{
			return base.GetComponent<SpriteRenderer>();
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00009D75 File Offset: 0x00007F75
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.transform.tag == "GlobalCollider")
		{
			this.touchedWall = true;
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00009D98 File Offset: 0x00007F98
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onBill = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.onHand = true;
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00009DD8 File Offset: 0x00007FD8
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onBill = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.onHand = false;
		}
		else if (other.tag == "Player" && !this.coinLeftWallet)
		{
			base.GetComponent<SpriteRenderer>().sortingOrder += 15;
			this.coinLeftWallet = true;
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00009E62 File Offset: 0x00008062
	public override void OnMouseDown()
	{
		if (!this.touchedWall)
		{
			return;
		}
		base.OnMouseDown();
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00009E76 File Offset: 0x00008076
	protected override void MouseDowned()
	{
		this.SortCoinLayers();
		base.body.isKinematic = false;
		if (this.onHand)
		{
			base.transform.SetParent(this.GetPuzzleStats().transform);
		}
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00009EAC File Offset: 0x000080AC
	private void SortCoinLayers()
	{
		(from x in this.wallet.coins
		where x.sortingOrder > this.sr.sortingOrder
		select x).ToList<SpriteRenderer>().ForEach(delegate(SpriteRenderer x)
		{
			x.sortingOrder--;
		});
		this.sr.sortingOrder = 20;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00009F0C File Offset: 0x0000810C
	protected override void MouseUpped()
	{
		base.body.isKinematic = (this.onBill || this.onHand);
		if (base.body.isKinematic)
		{
			base.body.velocity = Vector2.zero;
			base.body.angularVelocity = 0f;
		}
		this.CheckVictoryConditions();
		if (this.onHand)
		{
			base.transform.SetParent(this.handTransform, true);
			Audio.self.playOneShot("1bb078a3-3920-459a-9562-a0efb22b25a7", 1f);
		}
		else if (base.body.isKinematic)
		{
			Audio.self.playOneShot("245e7acc-a3ee-4c0a-ade6-8b57e780fe2e", 1f);
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00009FCC File Offset: 0x000081CC
	private void CheckVictoryConditions()
	{
		int num = (from x in this.GetComponentsInPuzzleStats(false)
		where x.onBill || x.onHand
		select x).Sum((PuzzleCoinsMissHand_Coin x) => x.number);
		if (this.onBill)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_CoinsMissHand>().placeOnTable();
		}
		if (num >= 7)
		{
			if (num > 7)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_CoinsMissHand>().tipped();
			}
			bool flag = (from x in this.GetComponentsInPuzzleStats(false)
			where x.onBill
			select x).Count<PuzzleCoinsMissHand_Coin>() > 0;
			if (flag)
			{
				Global.LevelCompleted(0f, true);
			}
			else
			{
				Global.LevelFailed(0f, true);
			}
		}
	}

	// Token: 0x04000166 RID: 358
	public int number;

	// Token: 0x04000167 RID: 359
	private bool touchedWall;

	// Token: 0x04000168 RID: 360
	private bool onHand;

	// Token: 0x04000169 RID: 361
	private bool onBill;

	// Token: 0x0400016A RID: 362
	public Transform handTransform;

	// Token: 0x0400016B RID: 363
	public PuzzleCoinsMissHand_Wallet wallet;

	// Token: 0x0400016C RID: 364
	private bool coinLeftWallet;

	// Token: 0x0400016D RID: 365
	private const int BILL_SUM = 7;
}
