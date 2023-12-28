using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class AwardFollowMouse : MonoBehaviour
{
	// Token: 0x06001414 RID: 5140 RVA: 0x00033250 File Offset: 0x00031650
	private void Awake()
	{
		this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
		this.initialPos = base.transform.localPosition;
		this.targetPos = this.initialPos + UnityEngine.Random.insideUnitCircle * this.range;
		base.transform.localPosition = this.initialPos + UnityEngine.Random.insideUnitCircle * this.range;
		this.speed = UnityEngine.Random.Range(this.speedMin, this.speedMax);
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x000332F0 File Offset: 0x000316F0
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.targetPos = this.initialPos + UnityEngine.Random.insideUnitCircle * this.range;
				this.speed = UnityEngine.Random.Range(this.speedMin, this.speedMax);
			}
		}
		Vector2 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 a2 = a - base.transform.position;
		if (Vector2.SqrMagnitude(a2) < this.minMouseDistance && !this.resting)
		{
			this.targetPos = this.initialPos + a2.normalized * this.range;
			this.speed = this.followSpeed;
			this.timer = this.timerMin;
			this.followTime += Time.deltaTime;
			if (this.followTime > this.maxFollowTime)
			{
				this.resting = true;
			}
		}
		if (this.resting)
		{
			this.followTime -= Time.deltaTime * 0.5f;
			if (this.followTime <= 0f)
			{
				this.resting = false;
			}
		}
		base.transform.localPosition = Vector2.Lerp(base.transform.localPosition, this.targetPos, Time.deltaTime * this.speed);
	}

	// Token: 0x0400111F RID: 4383
	public float timerMin = 5f;

	// Token: 0x04001120 RID: 4384
	public float timerMax = 10f;

	// Token: 0x04001121 RID: 4385
	private float timer;

	// Token: 0x04001122 RID: 4386
	public float range = 0.5f;

	// Token: 0x04001123 RID: 4387
	private float speed = 1f;

	// Token: 0x04001124 RID: 4388
	public float speedMin = 0.2f;

	// Token: 0x04001125 RID: 4389
	public float speedMax = 1.5f;

	// Token: 0x04001126 RID: 4390
	private Vector2 initialPos;

	// Token: 0x04001127 RID: 4391
	private Vector2 targetPos;

	// Token: 0x04001128 RID: 4392
	public float chanceToFollowMouse = 0.1f;

	// Token: 0x04001129 RID: 4393
	public float minMouseDistance = 5f;

	// Token: 0x0400112A RID: 4394
	public float followSpeed = 1f;

	// Token: 0x0400112B RID: 4395
	public float maxFollowTime = 5f;

	// Token: 0x0400112C RID: 4396
	private float followTime;

	// Token: 0x0400112D RID: 4397
	private bool resting;
}
