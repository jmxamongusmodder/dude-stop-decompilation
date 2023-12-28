using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
[RequireComponent(typeof(SpriteRenderer))]
public class AwardWhiteLine : CupLineShader
{
	// Token: 0x06001429 RID: 5161 RVA: 0x00033D78 File Offset: 0x00032178
	private void Start()
	{
		this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
		this.sr = base.GetComponent<SpriteRenderer>();
		this.sr.material.SetFloat("_Distance", -this.lineMaxX);
		this.lineX = -this.lineMaxX;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x00033DD4 File Offset: 0x000321D4
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.lineX = this.lineMaxX;
			}
		}
		if (this.lineX > -this.lineMaxX)
		{
			this.lineX -= Time.deltaTime * this.speed;
			this.sr.material.SetFloat("_Distance", this.lineX);
		}
	}

	// Token: 0x0400114F RID: 4431
	public float timerMin = 5f;

	// Token: 0x04001150 RID: 4432
	public float timerMax = 10f;

	// Token: 0x04001151 RID: 4433
	private float timer;

	// Token: 0x04001152 RID: 4434
	public float speed = 1f;

	// Token: 0x04001153 RID: 4435
	public float lineMaxX = 2f;

	// Token: 0x04001154 RID: 4436
	private float lineX;

	// Token: 0x04001155 RID: 4437
	private SpriteRenderer sr;
}
