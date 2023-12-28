using System;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class CupFishingChest_Controller : MonoBehaviour
{
	// Token: 0x0600149B RID: 5275 RVA: 0x0003856E File Offset: 0x0003696E
	private void Update()
	{
		this.PlaySound();
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x00038576 File Offset: 0x00036976
	public void RodActivated()
	{
		this.rodActive = true;
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0003857F File Offset: 0x0003697F
	public void UpdateHookPosition(float position)
	{
		this.hookPosition = position;
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x00038588 File Offset: 0x00036988
	private void PlaySound()
	{
		float paramValue = (!this.rodActive) ? 0f : this.hookPosition;
		Audio.self.playLoopSound("aa0ba26d-3cf0-4324-8f6c-00bae8bad891", "Water", paramValue);
	}

	// Token: 0x0400120A RID: 4618
	private bool rodActive;

	// Token: 0x0400120B RID: 4619
	private float hookPosition;
}
