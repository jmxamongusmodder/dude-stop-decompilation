using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002B0 RID: 688
public class AudioVoice_CupFastShoe : AudioVoice
{
	// Token: 0x060010E2 RID: 4322 RVA: 0x0001C3BC File Offset: 0x0001A7BC
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Audio.self.muteVoiceInEditor)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.startVoice);
		this.voice.start(true);
		base.StartCoroutine(this.waitTip());
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0001C41C File Offset: 0x0001A81C
	private IEnumerator waitTip()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(10f);
		if (this.ended)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.tipVoice);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0001C438 File Offset: 0x0001A838
	public void playEndLine()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.ended = true;
		this.voice = Audio.self.playVoice(this.endVoice);
		this.voice.start(true);
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0001C494 File Offset: 0x0001A894
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subscribeToMarkers(item, true);
	}

	// Token: 0x04000DE4 RID: 3556
	[Space(10f)]
	public StandaloneLevelVoice startVoice;

	// Token: 0x04000DE5 RID: 3557
	public StandaloneLevelVoice endVoice;

	// Token: 0x04000DE6 RID: 3558
	public StandaloneLevelVoice tipVoice;

	// Token: 0x04000DE7 RID: 3559
	private bool ended;
}
