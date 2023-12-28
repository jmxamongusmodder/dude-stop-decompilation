using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class AudioVoice_CupExamBad : AudioVoiceCups
{
	// Token: 0x060010D7 RID: 4311 RVA: 0x0001BFE3 File Offset: 0x0001A3E3
	public override void setActive(bool on)
	{
		if (Global.self.CountPackPlayedTimes(false, 0) == 0)
		{
			this.startVoice = this.normalStart;
		}
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.playWait());
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0001C024 File Offset: 0x0001A424
	private IEnumerator playWait()
	{
		yield return new WaitForSeconds(this.waitTime);
		if (!this.touched)
		{
			this.voice = Audio.self.playVoice(this.waitVoice);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0001C040 File Offset: 0x0001A440
	public void hatFalls()
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.lastVoice);
		Global.self.canExitEndScreen = false;
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			Global.self.canExitEndScreen = true;
		});
		this.voice.start(true);
	}

	// Token: 0x04000DD9 RID: 3545
	public StandaloneLevelVoice normalStart;

	// Token: 0x04000DDA RID: 3546
	public StandaloneLevelVoice waitVoice;

	// Token: 0x04000DDB RID: 3547
	public StandaloneLevelVoice lastVoice;

	// Token: 0x04000DDC RID: 3548
	public bool touched;

	// Token: 0x04000DDD RID: 3549
	public float waitTime = 9f;
}
