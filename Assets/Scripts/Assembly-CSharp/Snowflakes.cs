using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
[RequireComponent(typeof(ParticleSystem))]
public class Snowflakes : MonoBehaviour
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600143D RID: 5181 RVA: 0x00034340 File Offset: 0x00032740
	public static Snowflakes self
	{
		get
		{
			if (Snowflakes._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Snowflakes");
				if (gameObject == null)
				{
					Snowflakes._self = null;
				}
				else
				{
					Snowflakes._self = gameObject.GetComponent<Snowflakes>();
				}
			}
			return Snowflakes._self;
		}
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x0003438F File Offset: 0x0003278F
	private void Start()
	{
		this.particles = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x000343A0 File Offset: 0x000327A0
	private void Update()
	{
		if (this.timeCurrent > 0f)
		{
			this.timeCurrent = Mathf.MoveTowards(this.timeCurrent, 0f, Time.deltaTime);
			if (this.timeCurrent <= 0f)
			{
				this.particles.EnableEmmision(false);
			}
		}
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x000343F4 File Offset: 0x000327F4
	public void Emmit()
	{
		if (this.timeCurrent <= 0f)
		{
			this.particles.EnableEmmision(true);
		}
		this.timeCurrent = this.time;
	}

	// Token: 0x04001169 RID: 4457
	private static Snowflakes _self;

	// Token: 0x0400116A RID: 4458
	[SerializeField]
	private float time = 1f;

	// Token: 0x0400116B RID: 4459
	private float timeCurrent;

	// Token: 0x0400116C RID: 4460
	private ParticleSystem particles;
}
