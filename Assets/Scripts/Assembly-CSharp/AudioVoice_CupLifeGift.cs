using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class AudioVoice_CupLifeGift : AudioVoiceCups
{
	// Token: 0x0600110B RID: 4363 RVA: 0x0001D190 File Offset: 0x0001B590
	public void startWaitHammer()
	{
		base.StartCoroutine(this.waitLine());
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0001D1A0 File Offset: 0x0001B5A0
	private IEnumerator waitLine()
	{
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(10f);
		if (!this.hammerUsed)
		{
			this.voice = Audio.self.playVoice(this.waitHammer);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.hammerPlaying = false;
			});
			this.voice.start(true);
			this.hammerPlaying = true;
		}
		yield break;
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0001D1BC File Offset: 0x0001B5BC
	public void useHammer()
	{
		this.hammerUsed = true;
		if (this.hammerPlaying)
		{
			this.voice.stop();
			this.voice = Audio.self.playVoice(this.waitInterrupt);
			this.voice.start(true);
			this.hammerPlaying = false;
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0001D210 File Offset: 0x0001B610
	public void OpenBox()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice2 = Audio.self.playVoice(this.openBox);
		this.voice2.start(true);
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x0001D265 File Offset: 0x0001B665
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subsctibeToEnding(item);
		base.StartCoroutine(this.AfterEnd());
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0001D27C File Offset: 0x0001B67C
	private IEnumerator AfterEnd()
	{
		Global.self.canExitEndScreen = false;
		while (this.voice2.isPlaying())
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice(this.afterEnd);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.voiceEnded));
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0001D297 File Offset: 0x0001B697
	private void voiceEnded(VoiceLine line)
	{
		Global.self.canExitEndScreen = true;
	}

	// Token: 0x04000E0B RID: 3595
	public StandaloneLevelVoice waitHammer;

	// Token: 0x04000E0C RID: 3596
	public StandaloneLevelVoice waitInterrupt;

	// Token: 0x04000E0D RID: 3597
	public StandaloneLevelVoice openBox;

	// Token: 0x04000E0E RID: 3598
	public StandaloneLevelVoice afterEnd;

	// Token: 0x04000E0F RID: 3599
	private VoiceLine voice2;

	// Token: 0x04000E10 RID: 3600
	private bool hammerUsed;

	// Token: 0x04000E11 RID: 3601
	private bool hammerPlaying;
}
