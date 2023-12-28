using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AF RID: 687
public class AudioVoice_CupExamGood : AudioVoiceCups
{
	// Token: 0x060010DC RID: 4316 RVA: 0x0001C1B9 File Offset: 0x0001A5B9
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.playWait());
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0001C1DC File Offset: 0x0001A5DC
	private IEnumerator playWait()
	{
		yield return new WaitForSeconds(this.waitTime);
		if (!this.thrown)
		{
			this.voice = Audio.self.playVoice(this.waitVoice);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0001C1F8 File Offset: 0x0001A5F8
	public void hatThrown()
	{
		if (!this.active || this.thrown)
		{
			return;
		}
		this.thrown = true;
		if (this.voice != null)
		{
			this.voice.stop();
		}
		Global.self.canBePaused = false;
		this.voice = Audio.self.playVoice(this.endVoice);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0001C280 File Offset: 0x0001A680
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "end")
		{
			this.voiceStopped = true;
			Global.self.canBePaused = true;
		}
		else if (markerName == "hide")
		{
			this.hideAll = true;
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0001C2D3 File Offset: 0x0001A6D3
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subscribeToMarkers(item, false);
	}

	// Token: 0x04000DDF RID: 3551
	public StandaloneLevelVoice waitVoice;

	// Token: 0x04000DE0 RID: 3552
	private bool thrown;

	// Token: 0x04000DE1 RID: 3553
	public float waitTime = 5f;

	// Token: 0x04000DE2 RID: 3554
	[HideInInspector]
	public bool voiceStopped;

	// Token: 0x04000DE3 RID: 3555
	public bool hideAll;
}
