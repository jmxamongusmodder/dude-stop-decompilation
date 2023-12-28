using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class AwardFlyAnimation : MonoBehaviour
{
	// Token: 0x06001410 RID: 5136 RVA: 0x00032E80 File Offset: 0x00031280
	private void Awake()
	{
		this.box = base.GetComponent<BoxCollider2D>();
		this.sr = this.fly.GetComponent<SpriteRenderer>();
		this.flyPos = new Vector2(UnityEngine.Random.Range(0f, this.box.size.x), UnityEngine.Random.Range(0f, this.box.size.y));
		this.flyPos -= this.box.size * 0.5f;
		this.flyPos += this.box.offset;
		Color color = this.sr.color;
		color.a = 0f;
		this.sr.color = color;
		base.StartCoroutine(this.PlaySound());
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x00032F64 File Offset: 0x00031364
	private void Update()
	{
		if (this.show)
		{
			Color color = this.sr.color;
			color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime * 2f);
			if (color.a > 0.98f)
			{
				color.a = 1f;
				this.show = false;
			}
			this.sr.color = color;
		}
		this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
		if (this.timer <= 0f)
		{
			this.targetPos = new Vector2(UnityEngine.Random.Range(0f, this.box.size.x), UnityEngine.Random.Range(0f, this.box.size.y));
			this.targetPos -= this.box.size * 0.5f;
			this.targetPos += this.box.offset;
			this.timer = UnityEngine.Random.Range(this.maxTimer * 0.2f, this.maxTimer);
		}
		this.flyPos = Vector2.Lerp(this.flyPos, this.targetPos, Time.deltaTime * 2f);
		this.fly.localPosition = new Vector2(this.flyPos.x, this.flyPos.y + Mathf.PingPong(Time.time * 0.5f, 0.1f));
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x00033110 File Offset: 0x00031510
	private IEnumerator PlaySound()
	{
		for (;;)
		{
			Audio.self.playOneShot("4ecfefb7-ad2e-4f44-8641-e1c7d63f8859", 1f);
			yield return new WaitForSeconds(3f);
		}
		yield break;
	}

	// Token: 0x04001117 RID: 4375
	[Tooltip("Sprite with a fly pic")]
	public Transform fly;

	// Token: 0x04001118 RID: 4376
	private Vector2 flyPos = Vector2.zero;

	// Token: 0x04001119 RID: 4377
	private Vector2 targetPos;

	// Token: 0x0400111A RID: 4378
	private BoxCollider2D box;

	// Token: 0x0400111B RID: 4379
	private SpriteRenderer sr;

	// Token: 0x0400111C RID: 4380
	private float timer;

	// Token: 0x0400111D RID: 4381
	private float maxTimer = 2f;

	// Token: 0x0400111E RID: 4382
	private bool show = true;
}
