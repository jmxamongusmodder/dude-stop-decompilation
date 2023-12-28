using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class AwadFlyInCircle : MonoBehaviour
{
	// Token: 0x06001407 RID: 5127 RVA: 0x000328FB File Offset: 0x00030CFB
	private void Start()
	{
		this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
		this.pos = UnityEngine.Random.insideUnitCircle * this.range;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0003292C File Offset: 0x00030D2C
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timer = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.pos = UnityEngine.Random.insideUnitCircle * this.range;
			}
		}
		base.transform.localPosition = Vector2.Lerp(base.transform.localPosition, this.pos, Time.deltaTime * this.speed);
	}

	// Token: 0x04001104 RID: 4356
	public float timerMin = 0.1f;

	// Token: 0x04001105 RID: 4357
	public float timerMax = 0.2f;

	// Token: 0x04001106 RID: 4358
	private float timer;

	// Token: 0x04001107 RID: 4359
	private Vector2 pos;

	// Token: 0x04001108 RID: 4360
	public float speed = 1f;

	// Token: 0x04001109 RID: 4361
	public float range = 2f;
}
