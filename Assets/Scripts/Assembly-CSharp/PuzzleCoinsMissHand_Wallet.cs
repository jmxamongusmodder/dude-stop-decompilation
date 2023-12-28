using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class PuzzleCoinsMissHand_Wallet : Draggable
{
	// Token: 0x060000FF RID: 255 RVA: 0x0000A28C File Offset: 0x0000848C
	private void Update()
	{
		this.CheckShake();
		this.CheckTimer();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000A29A File Offset: 0x0000849A
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.verticalDirection = PuzzleCoinsMissHand_Wallet.Going.Down;
		this.horizontalDirection = PuzzleCoinsMissHand_Wallet.Going.Left;
		base.OnMouseDown();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000A2BC File Offset: 0x000084BC
	protected override void MouseUpped()
	{
		this.shakes = 0;
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000A2C8 File Offset: 0x000084C8
	private void CheckTimer()
	{
		this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
		if (this.timer == 0f)
		{
			this.shakes = 0;
			this.verticalDirection = PuzzleCoinsMissHand_Wallet.Going.Down;
			this.horizontalDirection = PuzzleCoinsMissHand_Wallet.Going.Left;
			this.ResetPosition();
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000A31C File Offset: 0x0000851C
	private void CheckShake()
	{
		if (!this.dragged || this.finishedShaking)
		{
			return;
		}
		if (this.verticalDirection == PuzzleCoinsMissHand_Wallet.Going.Down && base.transform.position.y < this.startingPosition.y - this.shakeDistance)
		{
			this.verticalDirection = PuzzleCoinsMissHand_Wallet.Going.Up;
			this.IncrementShake();
			this.ResetPosition();
		}
		else if (this.verticalDirection == PuzzleCoinsMissHand_Wallet.Going.Up && base.transform.position.y > this.startingPosition.y + this.shakeDistance)
		{
			this.verticalDirection = PuzzleCoinsMissHand_Wallet.Going.Down;
			this.IncrementShake();
			this.ResetPosition();
		}
		else if (this.horizontalDirection == PuzzleCoinsMissHand_Wallet.Going.Left && base.transform.position.x < this.startingPosition.x - this.shakeDistance)
		{
			this.horizontalDirection = PuzzleCoinsMissHand_Wallet.Going.Right;
			this.IncrementShake();
			this.ResetPosition();
		}
		else if (this.horizontalDirection == PuzzleCoinsMissHand_Wallet.Going.Right && base.transform.position.x > this.startingPosition.x + this.shakeDistance)
		{
			this.horizontalDirection = PuzzleCoinsMissHand_Wallet.Going.Left;
			this.IncrementShake();
			this.ResetPosition();
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000A472 File Offset: 0x00008672
	private void IncrementShake()
	{
		Audio.self.playOneShot("4bdf0788-f262-4ef0-ae6c-77dcecaa34d1", 1f);
		this.shakes++;
		this.CheckCycle();
		this.timer = this.shakeTime;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000A4A9 File Offset: 0x000086A9
	private void ResetPosition()
	{
		this.startingPosition = base.transform.position;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000A4BC File Offset: 0x000086BC
	private void CheckCycle()
	{
		if (this.shakes != this.shakesPerCycle[this.cycle])
		{
			return;
		}
		this.cycle++;
		this.shakes = 0;
		Transform[] array = null;
		switch (this.cycle)
		{
		case 1:
			array = this.firstCoins;
			break;
		case 2:
			array = this.secondCoins;
			break;
		case 3:
			array = this.thirdCoins;
			break;
		case 4:
			this.fly.position = base.transform.position;
			this.fly.gameObject.SetActive(true);
			Audio.self.playOneShot("709d2f4a-a6d7-4337-97ff-43414eae61bc", 1f);
			break;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_CoinsMissHand>().onThrowCoins();
		this.ThrowCoins(array);
		if (this.cycle == this.shakesPerCycle.Length)
		{
			this.finishedShaking = true;
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000A5B8 File Offset: 0x000087B8
	private void ThrowCoins(Transform[] coins)
	{
		if (coins == null)
		{
			return;
		}
		Audio.self.playOneShot("c9a5dcb8-b799-4699-9660-c004a4b8a016", 1f);
		foreach (Transform transform in coins)
		{
			transform.gameObject.SetActive(true);
			Rigidbody2D component = transform.GetComponent<Rigidbody2D>();
			float y = UnityEngine.Random.Range(this.throwForceMin, this.throwForce.y);
			float x = UnityEngine.Random.Range(-this.throwForce.x, this.throwForce.x);
			float torque = UnityEngine.Random.Range(-this.throwTorque, this.throwTorque);
			component.isKinematic = false;
			component.AddForce(new Vector2(x, y));
			component.AddTorque(torque);
			transform.SetParent(base.transform.parent);
			transform.GetComponent<PuzzleCoinsMissHand_Coin>().enabled = true;
		}
	}

	// Token: 0x04000179 RID: 377
	public float shakeDistance = 1f;

	// Token: 0x0400017A RID: 378
	public float shakeTime = 0.5f;

	// Token: 0x0400017B RID: 379
	public Vector2 throwForce;

	// Token: 0x0400017C RID: 380
	public float throwForceMin = 200f;

	// Token: 0x0400017D RID: 381
	public float throwTorque;

	// Token: 0x0400017E RID: 382
	public Transform[] firstCoins;

	// Token: 0x0400017F RID: 383
	public Transform[] secondCoins;

	// Token: 0x04000180 RID: 384
	public Transform[] thirdCoins;

	// Token: 0x04000181 RID: 385
	public SpriteRenderer[] coins;

	// Token: 0x04000182 RID: 386
	public int[] shakesPerCycle;

	// Token: 0x04000183 RID: 387
	private bool finishedShaking;

	// Token: 0x04000184 RID: 388
	private int shakes;

	// Token: 0x04000185 RID: 389
	private int cycle;

	// Token: 0x04000186 RID: 390
	private float timer;

	// Token: 0x04000187 RID: 391
	private PuzzleCoinsMissHand_Wallet.Going verticalDirection;

	// Token: 0x04000188 RID: 392
	private PuzzleCoinsMissHand_Wallet.Going horizontalDirection;

	// Token: 0x04000189 RID: 393
	[Header("Fly")]
	public Transform fly;

	// Token: 0x0200002C RID: 44
	private enum Going
	{
		// Token: 0x0400018B RID: 395
		Up,
		// Token: 0x0400018C RID: 396
		Down,
		// Token: 0x0400018D RID: 397
		Left,
		// Token: 0x0400018E RID: 398
		Right
	}
}
