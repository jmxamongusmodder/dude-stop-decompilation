using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class AwardUpDown : MonoBehaviour
{
	// Token: 0x06001426 RID: 5158 RVA: 0x00033B6D File Offset: 0x00031F6D
	private void Start()
	{
		this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
		this.initialPos = base.transform.localPosition;
		this.targetPos = this.initialPos;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x00033BA8 File Offset: 0x00031FA8
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.targetPos = this.initialPos;
				this.targetPos.y = this.targetPos.y + UnityEngine.Random.Range(this.maxDown, this.maxUp);
			}
		}
		base.transform.localPosition = Vector2.Lerp(base.transform.localPosition, this.targetPos, Time.deltaTime * this.speed);
	}

	// Token: 0x04001147 RID: 4423
	public float timerMin = 5f;

	// Token: 0x04001148 RID: 4424
	public float timerMax = 10f;

	// Token: 0x04001149 RID: 4425
	private float timer;

	// Token: 0x0400114A RID: 4426
	public float maxUp;

	// Token: 0x0400114B RID: 4427
	public float maxDown = -0.7f;

	// Token: 0x0400114C RID: 4428
	public float speed = 1f;

	// Token: 0x0400114D RID: 4429
	private Vector2 initialPos;

	// Token: 0x0400114E RID: 4430
	private Vector2 targetPos;
}
