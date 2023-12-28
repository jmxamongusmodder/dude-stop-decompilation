using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class PuzzleMotherCall_Phone : InventoryDraggable
{
	// Token: 0x06001B5C RID: 7004 RVA: 0x00071021 File Offset: 0x0006F421
	public void StopVibrating()
	{
		this.vibrating = false;
		base.StopCoroutine(this.vibrations);
		if (base.body == null)
		{
			base.gameObject.AddComponent<Rigidbody2D>();
			base.body.bodyType = RigidbodyType2D.Kinematic;
		}
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x0007105F File Offset: 0x0006F45F
	public void Move()
	{
		base.StartCoroutine(this.MovingCoroutine());
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x0007106E File Offset: 0x0006F46E
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.vibrating && this.vibrations == null)
		{
			this.vibrations = base.StartCoroutine(this.VibrationCoroutine());
		}
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x0007109E File Offset: 0x0006F49E
	public override void OnDisable()
	{
		if (this.vibrating)
		{
			base.StopCoroutine(this.vibrations);
			this.vibrations = null;
		}
		base.OnDisable();
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x000710C4 File Offset: 0x0006F4C4
	protected override void ChangeLooks()
	{
		this.bigSprites.gameObject.SetActive(true);
		this.smallSprite.gameObject.SetActive(false);
		base.GetComponent<PolygonCollider2D>().enabled = true;
		base.body.simulated = true;
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x00071100 File Offset: 0x0006F500
	public override void DroppedInInventory()
	{
		this.bigSprites.gameObject.SetActive(false);
		base.GetComponent<PolygonCollider2D>().enabled = false;
		base.body.simulated = false;
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x0007112C File Offset: 0x0006F52C
	private IEnumerator MovingCoroutine()
	{
		Global.self.canBePaused = false;
		Global.PauseArrows(this.offsetTime + 0.2f);
		yield return null;
		Vector2 endPosition = base.transform.localPosition + this.offsetAfterEnd;
		float timer = 0f;
		while (timer != this.offsetTime)
		{
			timer = Mathf.MoveTowards(timer, this.offsetTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(base.transform.localPosition, endPosition, timer / this.offsetTime);
			yield return null;
		}
		this.inventoryReturnPoint = base.transform.position;
		this.returnToPoint = true;
		base.GetComponent<PolygonCollider2D>().enabled = true;
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x00071148 File Offset: 0x0006F548
	private IEnumerator VibrationCoroutine()
	{
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		yield return new WaitForEndOfFrame();
		for (;;)
		{
			timer = 0f;
			Audio.self.playOneShot("650df069-ed4d-48ce-9990-b83dc61531cf", 1f);
			while (timer < this.vibrationTime)
			{
				timer = Mathf.MoveTowards(timer, this.vibrationTime, Time.deltaTime);
				bool left = (int)(timer * 10f) % 2 == 1;
				float sign = (float)((!left) ? 1 : -1);
				base.transform.localPosition = new Vector2(start.x + this.vibrationShift * sign, base.transform.localPosition.y);
				yield return null;
			}
			yield return new WaitForSeconds(this.waitBetweenVibrations);
		}
		yield break;
	}

	// Token: 0x040019A2 RID: 6562
	[Header("End of call")]
	public Vector2 offsetAfterEnd;

	// Token: 0x040019A3 RID: 6563
	public float offsetTime;

	// Token: 0x040019A4 RID: 6564
	public float vibrationTime;

	// Token: 0x040019A5 RID: 6565
	public float vibrationShift;

	// Token: 0x040019A6 RID: 6566
	public float waitBetweenVibrations;

	// Token: 0x040019A7 RID: 6567
	public Transform bigSprites;

	// Token: 0x040019A8 RID: 6568
	public Transform smallSprite;

	// Token: 0x040019A9 RID: 6569
	private Coroutine vibrations;

	// Token: 0x040019AA RID: 6570
	private bool vibrating = true;
}
