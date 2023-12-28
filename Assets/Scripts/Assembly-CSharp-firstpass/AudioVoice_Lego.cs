using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class AudioVoice_Lego : AudioVoiceDefault
{
	// Token: 0x060011AD RID: 4525 RVA: 0x000227A2 File Offset: 0x00020BA2
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!on && this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x000227D8 File Offset: 0x00020BD8
	public void playWeirdShape()
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.wierdShape);
		this.voice.start(true);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0002282C File Offset: 0x00020C2C
	public void playTower()
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null)
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.tower);
		this.voice.start(true);
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x0002287D File Offset: 0x00020C7D
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "right")
		{
			base.StartCoroutine(this.enableExit());
		}
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x000228A4 File Offset: 0x00020CA4
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subsctibeToEnding(item);
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000228B0 File Offset: 0x00020CB0
	private IEnumerator enableExit()
	{
		yield return new WaitForSeconds(10f);
		base.endVoicedEnding(this.voice);
		Global.self.canExitEndScreen = true;
		yield break;
	}

	// Token: 0x04000EC6 RID: 3782
	public StandaloneLevelVoice wierdShape;

	// Token: 0x04000EC7 RID: 3783
	public StandaloneLevelVoice tower;
}
