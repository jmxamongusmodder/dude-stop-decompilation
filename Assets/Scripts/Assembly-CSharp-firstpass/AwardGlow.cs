using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class AwardGlow : MonoBehaviour
{
	// Token: 0x06001417 RID: 5143 RVA: 0x000334C5 File Offset: 0x000318C5
	private void Start()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.color = this.sr.color;
		this.color.a = 0f;
		this.sr.color = this.color;
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x00033508 File Offset: 0x00031908
	private void Update()
	{
		this.timeIntervalCurr -= Time.deltaTime;
		if (this.timeIntervalCurr <= 0f)
		{
			this.showWhite = true;
			this.timeIntervalCurr = UnityEngine.Random.Range(this.timeInterval * 0.6f, this.timeInterval * 1.6f);
		}
		if (this.showWhite)
		{
			this.color.a = this.color.a + this.whiteSpeed * Time.deltaTime;
			if (this.color.a > this.whiteAmount)
			{
				this.showWhite = false;
			}
			this.sr.color = this.color;
		}
		else if (this.color.a > 0f)
		{
			this.color.a = this.color.a - this.whiteSpeed * Time.deltaTime;
			this.sr.color = this.color;
		}
	}

	// Token: 0x0400112E RID: 4398
	public float timeInterval;

	// Token: 0x0400112F RID: 4399
	private float timeIntervalCurr = 2f;

	// Token: 0x04001130 RID: 4400
	public float whiteAmount = 0.3f;

	// Token: 0x04001131 RID: 4401
	public float whiteSpeed = 1f;

	// Token: 0x04001132 RID: 4402
	private bool showWhite;

	// Token: 0x04001133 RID: 4403
	private Color color;

	// Token: 0x04001134 RID: 4404
	private SpriteRenderer sr;
}
