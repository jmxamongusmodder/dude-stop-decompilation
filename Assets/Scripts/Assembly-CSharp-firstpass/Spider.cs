using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class Spider : MonoBehaviour
{
	// Token: 0x06001442 RID: 5186 RVA: 0x0003445D File Offset: 0x0003285D
	private void Awake()
	{
		if (this.coroutine != null)
		{
			return;
		}
		this.coroutine = base.StartCoroutine(this.Move());
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x00034480 File Offset: 0x00032880
	private IEnumerator Move()
	{
		this.shift = UnityEngine.Random.value * 100f;
		for (; ; )
		{
			this.shift += Time.deltaTime * this.speed;
			float prog = Mathf.PerlinNoise(this.shift, 0f);
			Vector3 pos = this.spider.localPosition;
			pos.y = Mathf.Lerp(this.minHeight, this.maxHeight, prog);
			this.spider.localPosition = pos;
			Vector3 webPos = this.web.localPosition;
			float dist = Mathf.Abs(this.webTop - pos.y);
			webPos.y = this.webTop - dist * 0.5f;
			this.web.localPosition = webPos;
			Vector3 scale = this.web.localScale;
			scale.y = dist * this.webScale;
			this.web.localScale = scale;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400116D RID: 4461
	public float speed = 0.3f;

	// Token: 0x0400116E RID: 4462
	[Header("Spider")]
	public Transform spider;

	// Token: 0x0400116F RID: 4463
	public float maxHeight = 1f;

	// Token: 0x04001170 RID: 4464
	public float minHeight = 2f;

	// Token: 0x04001171 RID: 4465
	private float shift;

	// Token: 0x04001172 RID: 4466
	[Header("Web")]
	public Transform web;

	// Token: 0x04001173 RID: 4467
	public float webTop = 1f;

	// Token: 0x04001174 RID: 4468
	public float webScale = 1.56f;

	// Token: 0x04001175 RID: 4469
	private Coroutine coroutine;
}
