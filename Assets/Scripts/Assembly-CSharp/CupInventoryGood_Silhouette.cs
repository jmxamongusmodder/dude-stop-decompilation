using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class CupInventoryGood_Silhouette : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x0000842F File Offset: 0x0000662F
	private void Start()
	{
		this.rend = base.GetComponentInChildren<SpriteRenderer>();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00008440 File Offset: 0x00006640
	private void Update()
	{
		Color color = this.rend.color;
		float target = (float)((!this.snapped) ? 1 : 0);
		color.a = Mathf.MoveTowards(color.a, target, this.alphaChangeSpeed * Time.deltaTime);
		this.rend.color = color;
	}

	// Token: 0x0400012C RID: 300
	public float alphaChangeSpeed;

	// Token: 0x0400012D RID: 301
	[HideInInspector]
	public bool snapped;

	// Token: 0x0400012E RID: 302
	private SpriteRenderer rend;
}
