using System;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public class Old_PuzzleBowl : Draggable
{
	// Token: 0x0600179B RID: 6043 RVA: 0x0004F690 File Offset: 0x0004DA90
	private void Update()
	{
		this.soundTimer = Mathf.MoveTowards(this.soundTimer, 0f, Time.deltaTime);
		if (this.dragged)
		{
			this.timer = 0f;
		}
		else if (this.timer >= this.waitBeforeTransition)
		{
			if (this.success)
			{
				Global.LevelCompleted(0f, true);
			}
			else
			{
				Global.LevelFailed(0f, true);
			}
		}
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x0004F70C File Offset: 0x0004DB0C
	private void OnCollisionEnter2D(Collision2D hit)
	{
		if (this.soundTimer <= 0f && (hit.collider.tag == "FailCollider" || hit.collider.tag == "SuccessCollider"))
		{
			this.soundTimer += 0.1f;
		}
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x0004F76F File Offset: 0x0004DB6F
	private void OnCollisionExit2D()
	{
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x0004F771 File Offset: 0x0004DB71
	private void OnTriggerStay2D(Collider2D other)
	{
		this.timer += Time.deltaTime;
		this.success = (other.tag == "SuccessCollider");
	}

	// Token: 0x04001566 RID: 5478
	public float waitBeforeTransition = 2f;

	// Token: 0x04001567 RID: 5479
	private float soundTimer;

	// Token: 0x04001568 RID: 5480
	private float timer;

	// Token: 0x04001569 RID: 5481
	private bool success;
}
