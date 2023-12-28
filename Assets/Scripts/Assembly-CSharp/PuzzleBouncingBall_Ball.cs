using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class PuzzleBouncingBall_Ball : MonoBehaviour
{
	// Token: 0x06001844 RID: 6212 RVA: 0x000549A8 File Offset: 0x00052DA8
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!base.enabled)
		{
			return;
		}
		if (other.collider.tag == "GlobalCollider" && this.active)
		{
			Global.LevelCompleted(0f, true);
			this.active = false;
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000549F8 File Offset: 0x00052DF8
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!base.enabled)
		{
			return;
		}
		if (other.tag == "SuccessCollider" && this.active)
		{
			Global.LevelFailed(0f, true);
			this.active = false;
		}
		else if (other.tag == "FailCollider" && this.active)
		{
			this.onSide = true;
			base.StartCoroutine(this.BalancingCoroutine());
		}
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x00054A7C File Offset: 0x00052E7C
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "FailCollider" && this.active)
		{
			this.onSide = false;
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x00054AA5 File Offset: 0x00052EA5
	private void OnEnable()
	{
		base.GetComponent<Rigidbody2D>().gravityScale = 1f;
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x00054AB8 File Offset: 0x00052EB8
	private IEnumerator BalancingCoroutine()
	{
		yield return new WaitForSeconds(this.balanceWaitTime);
		if (!this.onSide || !base.enabled)
		{
			yield break;
		}
		if (base.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < this.maxBalanceVelocity)
		{
			Global.LevelFailed(0f, true);
			base.GetComponent<Rigidbody2D>().isKinematic = true;
		}
		yield break;
	}

	// Token: 0x0400162C RID: 5676
	public float balanceWaitTime = 1.5f;

	// Token: 0x0400162D RID: 5677
	public float maxBalanceVelocity = 0.15f;

	// Token: 0x0400162E RID: 5678
	private bool active = true;

	// Token: 0x0400162F RID: 5679
	private bool onSide;
}
