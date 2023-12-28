using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
public class AudioVoice_CupCandy : AudioVoiceCups
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x0001AFF5 File Offset: 0x000193F5
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.enableCandySound = (UnityEngine.Random.value > 0.5f);
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0001B01C File Offset: 0x0001941C
	public void getCandy()
	{
		if (!this.active || !this.enableCandySound)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.onPickUp);
		this.voice.start(true);
	}

	// Token: 0x04000DCB RID: 3531
	[Space(10f)]
	public StandaloneLevelVoice onPickUp;

	// Token: 0x04000DCC RID: 3532
	private bool enableCandySound;
}
