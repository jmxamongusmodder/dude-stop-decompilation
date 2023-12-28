using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class PuzzleCinemaPhone_Screen : MonoBehaviour
{
	// Token: 0x0600193D RID: 6461 RVA: 0x0005D57F File Offset: 0x0005B97F
	private void Start()
	{
		Audio.self.playLoopSound("9e43da33-0838-46ae-99c0-ae213d3d5dd6");
		this.soundPlaying = true;
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0005D597 File Offset: 0x0005B997
	private void OnDisable()
	{
		if (this.soundPlaying)
		{
		}
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x0005D5A4 File Offset: 0x0005B9A4
	public void StartGlowing()
	{
		base.StartCoroutine(this.GlowingCoroutine());
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x0005D5B4 File Offset: 0x0005B9B4
	private IEnumerator GlowingCoroutine()
	{
		Color c = this.glowList[0].color;
		float dist = 0f;
		for (;;)
		{
			dist += this.glowingSpeed * Time.deltaTime;
			c.a = Mathf.PerlinNoise(0f, dist) * this.glowingAmount + this.glowingMin;
			foreach (SpriteRenderer spriteRenderer in this.glowList)
			{
				spriteRenderer.enabled = true;
				spriteRenderer.color = c;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400173E RID: 5950
	public float glowingSpeed = 0.05f;

	// Token: 0x0400173F RID: 5951
	public float glowingAmount = 0.1f;

	// Token: 0x04001740 RID: 5952
	public float glowingMin = 0.2f;

	// Token: 0x04001741 RID: 5953
	public SpriteRenderer[] glowList;

	// Token: 0x04001742 RID: 5954
	private bool soundPlaying;
}
