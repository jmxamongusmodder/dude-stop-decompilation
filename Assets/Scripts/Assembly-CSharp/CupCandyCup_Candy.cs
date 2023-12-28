using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class CupCandyCup_Candy : MonoBehaviour
{
	// Token: 0x06001470 RID: 5232 RVA: 0x00035ED2 File Offset: 0x000342D2
	private void Start()
	{
		this.body = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00035EE0 File Offset: 0x000342E0
	private void FixedUpdate()
	{
		this.body.velocity = Vector2.ClampMagnitude(this.body.velocity, this.maxMagnitude);
		this.body.angularVelocity = Mathf.Clamp(this.body.angularVelocity, -this.maxAngularVelocity, this.maxAngularVelocity);
	}

	// Token: 0x040011C6 RID: 4550
	[HideInInspector]
	public float maxMagnitude;

	// Token: 0x040011C7 RID: 4551
	[HideInInspector]
	public float maxAngularVelocity;

	// Token: 0x040011C8 RID: 4552
	private Rigidbody2D body;
}
