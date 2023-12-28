using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class AudioVoice_DoNotLitter : AudioVoiceReceive
{
	// Token: 0x06001153 RID: 4435 RVA: 0x0001FAAB File Offset: 0x0001DEAB
	public void juggle()
	{
		if (this.juggled)
		{
			return;
		}
		base.StartCoroutine(this.juggling());
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0001FAC8 File Offset: 0x0001DEC8
	private IEnumerator juggling()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		if (!base.enabled || !this.active || this.juggled)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.juggleLine);
		this.voice.start(true);
		this.juggled = true;
		yield break;
	}

	// Token: 0x04000E56 RID: 3670
	[Space(10f)]
	public StandaloneLevelVoice juggleLine;

	// Token: 0x04000E57 RID: 3671
	private bool juggled;
}
