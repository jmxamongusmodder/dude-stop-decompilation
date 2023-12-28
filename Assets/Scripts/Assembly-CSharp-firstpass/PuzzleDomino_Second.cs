using System;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public class PuzzleDomino_Second : MonoBehaviour
{
	// Token: 0x060019D7 RID: 6615 RVA: 0x00062940 File Offset: 0x00060D40
	private void Update()
	{
		if (this.timeLeft != -1f)
		{
			if (this.timeLeft <= 0f)
			{
				Global.LevelFailed(0f, true);
			}
			else
			{
				this.timeLeft -= Time.deltaTime;
			}
		}
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x00062994 File Offset: 0x00060D94
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform == this.other && this.timeLeft == -1f)
		{
			this.timeLeft = this.timeToWait;
		}
	}

	// Token: 0x040017E5 RID: 6117
	public float timeToWait = 2f;

	// Token: 0x040017E6 RID: 6118
	public Transform other;

	// Token: 0x040017E7 RID: 6119
	private float timeLeft = -1f;
}
