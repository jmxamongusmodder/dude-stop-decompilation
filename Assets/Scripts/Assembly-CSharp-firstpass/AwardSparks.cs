using System;
using UnityEngine;

// Token: 0x02000331 RID: 817
[RequireComponent(typeof(ParticleSystem))]
public class AwardSparks : MonoBehaviour
{
	// Token: 0x06001421 RID: 5153 RVA: 0x0003395B File Offset: 0x00031D5B
	private void Start()
	{
		this.timerCurr = UnityEngine.Random.Range(this.timerMin, this.timerMax);
		this.ps = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x00033980 File Offset: 0x00031D80
	private void Update()
	{
		if (this.timerCurr > 0f)
		{
			this.timerCurr -= Time.deltaTime;
			if (this.timerCurr <= 0f)
			{
				this.timerCurr = UnityEngine.Random.Range(this.timerMin, this.timerMax);
				this.ps.Emit(UnityEngine.Random.Range(1, this.amountMax));
			}
		}
	}

	// Token: 0x0400113F RID: 4415
	public float timerMin = 0.1f;

	// Token: 0x04001140 RID: 4416
	public float timerMax = 10f;

	// Token: 0x04001141 RID: 4417
	private float timerCurr;

	// Token: 0x04001142 RID: 4418
	public int amountMax = 10;

	// Token: 0x04001143 RID: 4419
	private ParticleSystem ps;
}
