using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class AudioVoice_CupCake : AudioVoiceCups
{
	// Token: 0x060010BC RID: 4284 RVA: 0x0001AE9F File Offset: 0x0001929F
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.waitLine());
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0001AEC4 File Offset: 0x000192C4
	private IEnumerator waitLine()
	{
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		if (!this.candle)
		{
			this.voice = Audio.self.playVoice(this.waitVoice);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0001AEDF File Offset: 0x000192DF
	public void candleIn()
	{
		this.candle = true;
	}

	// Token: 0x04000DC9 RID: 3529
	public StandaloneLevelVoice waitVoice;

	// Token: 0x04000DCA RID: 3530
	private bool candle;
}
