using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class CupToyMachine_Cup : MonoBehaviour
{
	// Token: 0x060015A5 RID: 5541 RVA: 0x00042FF4 File Offset: 0x000413F4
	private void OnMouseDown()
	{
		Global.CupAcquired(base.transform);
		base.StartCoroutine(this.darkenSprites());
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00043010 File Offset: 0x00041410
	private IEnumerator darkenSprites()
	{
		float time = 0f;
		float alpha = 0f;
		while (alpha < this.darkAmount)
		{
			time += Time.deltaTime;
			alpha = time / this.darkTime * this.darkAmount;
			Color c = this.darken[0].color;
			c.a = alpha;
			foreach (SpriteRenderer spriteRenderer in this.darken)
			{
				spriteRenderer.color = c;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001362 RID: 4962
	public SpriteRenderer[] darken;

	// Token: 0x04001363 RID: 4963
	public float darkTime = 2f;

	// Token: 0x04001364 RID: 4964
	public float darkAmount = 0.6f;
}
