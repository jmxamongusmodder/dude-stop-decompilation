using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
[RequireComponent(typeof(SpriteRenderer))]
public class Flickr : MonoBehaviour
{
	// Token: 0x0600142C RID: 5164 RVA: 0x00033E9C File Offset: 0x0003229C
	private void Awake()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
		this.color = this.sr.color;
		this.color.a = 0f;
		this.sr.color = this.color;
		this.shift = UnityEngine.Random.value * 100f;
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x00033EF8 File Offset: 0x000322F8
	private void Update()
	{
		this.shift += Time.deltaTime * this.speed;
		this.color.a = Mathf.Lerp(this.color.a, Mathf.PerlinNoise(this.shift, 0f), this.lerpSpeed * Time.deltaTime);
		this.sr.color = this.color;
	}

	// Token: 0x04001156 RID: 4438
	private SpriteRenderer sr;

	// Token: 0x04001157 RID: 4439
	private Color color;

	// Token: 0x04001158 RID: 4440
	public float speed = 1.5f;

	// Token: 0x04001159 RID: 4441
	public float lerpSpeed = 4f;

	// Token: 0x0400115A RID: 4442
	private float shift;
}
