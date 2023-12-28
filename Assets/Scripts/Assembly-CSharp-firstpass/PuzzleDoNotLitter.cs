using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public class PuzzleDoNotLitter : MonoBehaviour
{
	// Token: 0x060019DA RID: 6618 RVA: 0x000629DB File Offset: 0x00060DDB
	private void Update()
	{
		base.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(base.GetComponent<Rigidbody2D>().velocity, this.maximumVelocity);
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x000629FE File Offset: 0x00060DFE
	private void OnTriggerExit2D(Collider2D other)
	{
		this.waitTimer = 0f;
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x00062A0C File Offset: 0x00060E0C
	private void OnTriggerStay2D(Collider2D other)
	{
		this.waitTimer = Mathf.MoveTowards(this.waitTimer, this.wait, Time.deltaTime);
		if (base.enabled && this.waitTimer == this.wait)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Back");
			if (other.tag == "FailCollider")
			{
				Global.LevelFailed(0f, true);
				Audio.self.playOneShot("1b02afc5-6d94-4ddf-a73c-226635d091e9", 1f);
			}
			else if (other.tag == "SuccessCollider")
			{
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x040017E8 RID: 6120
	public float maximumVelocity;

	// Token: 0x040017E9 RID: 6121
	public float wait = 0.5f;

	// Token: 0x040017EA RID: 6122
	private float waitTimer;
}
