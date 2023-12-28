using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
[RequireComponent(typeof(SpriteRenderer))]
public class FlickrLights : MonoBehaviour
{
	// Token: 0x0600142F RID: 5167 RVA: 0x00033F84 File Offset: 0x00032384
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.color = this.sr.color;
		this.color.a = 0f;
		this.sr.color = this.color;
		this.shift = UnityEngine.Random.value * 100f;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x00033FE0 File Offset: 0x000323E0
	private void Update()
	{
		this.shift += Time.deltaTime * this.speed;
		if (Mathf.PerlinNoise(this.shift, 0f) > this.treshhold)
		{
			this.color.a = 1f;
		}
		else
		{
			this.color.a = 0f;
		}
		this.sr.color = this.color;
	}

	// Token: 0x0400115B RID: 4443
	private SpriteRenderer sr;

	// Token: 0x0400115C RID: 4444
	private Color color;

	// Token: 0x0400115D RID: 4445
	public float speed = 2f;

	// Token: 0x0400115E RID: 4446
	public float treshhold = 0.4f;

	// Token: 0x0400115F RID: 4447
	private float shift;
}
