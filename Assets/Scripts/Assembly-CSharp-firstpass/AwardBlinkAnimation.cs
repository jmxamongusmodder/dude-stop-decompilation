using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class AwardBlinkAnimation : MonoBehaviour
{
	// Token: 0x0600140D RID: 5133 RVA: 0x00032C44 File Offset: 0x00031044
	private void Awake()
	{
		this.sr = this.blink.GetComponent<SpriteRenderer>();
		this.timer = UnityEngine.Random.Range(1f, 5f);
		Color color = this.sr.color;
		color.a = 0f;
		this.sr.color = color;
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x00032C9C File Offset: 0x0003109C
	private void Update()
	{
		Color color = this.sr.color;
		if (this.show)
		{
			color.a = Mathf.Lerp(color.a, 1.2f, Time.deltaTime * 10f);
			this.blink.localScale = Vector3.Lerp(this.blink.localScale, Vector3.one, Time.deltaTime * 10f);
			if (color.a > 1.19f)
			{
				this.show = false;
			}
		}
		else
		{
			color.a = Mathf.Lerp(color.a, 0f, Time.deltaTime * 4f);
			this.blink.localScale = Vector3.Lerp(this.blink.localScale, Vector3.one * 0.5f, Time.deltaTime * 1f);
		}
		this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
		if (this.timer <= 0f)
		{
			this.blink.position = this.marker[UnityEngine.Random.Range(0, this.marker.Length)].position;
			this.blink.localScale = Vector3.one * 0.3f;
			color.a = 0f;
			this.show = true;
			this.timer = UnityEngine.Random.Range(this.timerMax * 0.5f, this.timerMax);
			Audio.self.playOneShot("5624b5f1-1d61-45bc-a9ba-fc002373cb37", 1f);
		}
		this.blink.Rotate(Vector3.forward * 1f);
		this.sr.color = color;
	}

	// Token: 0x04001111 RID: 4369
	[Tooltip("Sprite with blink pic")]
	public Transform blink;

	// Token: 0x04001112 RID: 4370
	[Tooltip("Empty objects to mark positions where blink can appear")]
	public Transform[] marker;

	// Token: 0x04001113 RID: 4371
	private bool show;

	// Token: 0x04001114 RID: 4372
	private float timer;

	// Token: 0x04001115 RID: 4373
	private float timerMax = 12f;

	// Token: 0x04001116 RID: 4374
	private SpriteRenderer sr;
}
