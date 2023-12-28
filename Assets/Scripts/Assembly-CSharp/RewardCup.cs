using System;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class RewardCup : MonoBehaviour
{
	// Token: 0x060015A8 RID: 5544 RVA: 0x000431B4 File Offset: 0x000415B4
	private void Update()
	{
		if (this.acquired)
		{
			return;
		}
		if (this.canGetCup)
		{
			if (Global.self.currPuzzle.GetComponent<AudioVoice_TrashCup>() && !Global.self.currPuzzle.GetComponent<AudioVoice_TrashCup>().canGetCup)
			{
				return;
			}
			Global.CupAcquired(base.transform);
			this.acquired = true;
		}
		if (this.leftOnce)
		{
			this.timeLeftFirst -= Time.deltaTime;
		}
		if (this.outOfBasket)
		{
			this.timeLeftComplete -= Time.deltaTime;
		}
		if (this.timeLeftComplete < 0f || this.timeLeftFirst < 0f)
		{
			this.canGetCup = true;
		}
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x0004327E File Offset: 0x0004167E
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.outOfBasket = false;
		}
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x0004329C File Offset: 0x0004169C
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.outOfBasket = false;
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000432BC File Offset: 0x000416BC
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.outOfBasket = true;
			this.timeLeftComplete = this.waitAfterCompleteExit;
			if (!this.leftOnce)
			{
				this.leftOnce = true;
				this.timeLeftFirst = this.waitAfterFirstExit;
			}
		}
	}

	// Token: 0x04001365 RID: 4965
	public float waitAfterFirstExit = 10f;

	// Token: 0x04001366 RID: 4966
	public float waitAfterCompleteExit = 2f;

	// Token: 0x04001367 RID: 4967
	private float timeLeftFirst;

	// Token: 0x04001368 RID: 4968
	private float timeLeftComplete;

	// Token: 0x04001369 RID: 4969
	private bool leftOnce;

	// Token: 0x0400136A RID: 4970
	private bool outOfBasket;

	// Token: 0x0400136B RID: 4971
	private bool acquired;

	// Token: 0x0400136C RID: 4972
	private bool canGetCup;
}
