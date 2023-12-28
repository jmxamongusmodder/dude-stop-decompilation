using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class AudioVoice_BigPowerSupply : AudioVoiceReceive
{
	// Token: 0x0600105E RID: 4190 RVA: 0x000161B0 File Offset: 0x000145B0
	public void onShake()
	{
		if (this.shakePlayed || this.shaked >= this.shakeLines.Length)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.shakeLines[this.shaked++]);
		this.voice.start(true);
		this.shakePlayed = true;
		base.StartCoroutine(this.allowShake());
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0001624C File Offset: 0x0001464C
	private IEnumerator allowShake()
	{
		yield return new WaitForSeconds(5f);
		this.shakePlayed = false;
		yield break;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x00016268 File Offset: 0x00014668
	public void onWrongHole()
	{
		if (this.wrongPlayed)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.wrongHoleLine);
		this.voice.start(true);
		this.wrongPlayed = true;
	}

	// Token: 0x04000D6A RID: 3434
	[Space(10f)]
	public StandaloneLevelVoice[] shakeLines;

	// Token: 0x04000D6B RID: 3435
	public StandaloneLevelVoice wrongHoleLine;

	// Token: 0x04000D6C RID: 3436
	private bool wrongPlayed;

	// Token: 0x04000D6D RID: 3437
	private bool shakePlayed;

	// Token: 0x04000D6E RID: 3438
	private int shaked;
}
