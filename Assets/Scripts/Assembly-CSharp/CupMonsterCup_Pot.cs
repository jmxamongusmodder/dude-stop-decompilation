using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class CupMonsterCup_Pot : MonoBehaviour
{
	// Token: 0x06001555 RID: 5461 RVA: 0x0003EF53 File Offset: 0x0003D353
	public void IncreaseEmission()
	{
		this.darkEmission.IncrementEmissionRate(this.darkIncrement);
		this.lightEmission.IncrementEmissionRate(this.lightIncrement);
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x0003EF77 File Offset: 0x0003D377
	public void Splash()
	{
		this.splash.Emit();
	}

	// Token: 0x04001301 RID: 4865
	public ParticleSystem darkEmission;

	// Token: 0x04001302 RID: 4866
	public ParticleSystem lightEmission;

	// Token: 0x04001303 RID: 4867
	public ParticleSystem splash;

	// Token: 0x04001304 RID: 4868
	public float darkIncrement;

	// Token: 0x04001305 RID: 4869
	public float lightIncrement;
}
