using System;
using UnityEngine;

// Token: 0x020003B5 RID: 949
public class Old_PuzzleLego : Draggable
{
	// Token: 0x060017A0 RID: 6048 RVA: 0x0004F7B0 File Offset: 0x0004DBB0
	private void Update()
	{
		bool? flag = this.state;
		if (flag != null)
		{
			if (this.secondsBeforeTransition <= 0f)
			{
				if (this.state == true && base.transform.rotation.eulerAngles.z < 1f)
				{
					Global.LevelCompleted(0f, true);
				}
				else if (this.state == false)
				{
					Global.LevelFailed(0f, true);
				}
			}
			else
			{
				this.secondsBeforeTransition -= Time.deltaTime;
			}
		}
		else if (this.secondsBeforeTransition != this.waitBeforeTransition)
		{
			this.secondsBeforeTransition = this.waitBeforeTransition;
		}
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x0004F89C File Offset: 0x0004DC9C
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.state = new bool?(true);
		}
		else if (other.tag == "FailCollider")
		{
			this.state = new bool?(false);
		}
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x0004F8F0 File Offset: 0x0004DCF0
	private void OnTriggerExit2D()
	{
		this.state = null;
	}

	// Token: 0x0400156A RID: 5482
	public float waitBeforeTransition = 2f;

	// Token: 0x0400156B RID: 5483
	private float secondsBeforeTransition;

	// Token: 0x0400156C RID: 5484
	private bool? state;
}
