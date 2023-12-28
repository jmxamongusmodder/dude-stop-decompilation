using System;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class LegoPhysicsSound : PhysicsSound
{
	// Token: 0x060016E6 RID: 5862 RVA: 0x0004B2E0 File Offset: 0x000496E0
	public override void OnCollisionEnter2D(Collision2D other)
	{
		if (this.CanPlaySound(other))
		{
			base.OnCollisionEnter2D(other);
		}
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x0004B2F5 File Offset: 0x000496F5
	public override void OnCollisionStay2D(Collision2D other)
	{
		if (this.CanPlaySound(other))
		{
			base.OnCollisionStay2D(other);
		}
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x0004B30C File Offset: 0x0004970C
	protected bool CanPlaySound(Collision2D other)
	{
		LegoPiece component = base.GetComponent<LegoPiece>();
		LegoPiece component2 = other.transform.GetComponent<LegoPiece>();
		return component == null || component2 == null || (!component.lockX || !component2.lockX);
	}
}
