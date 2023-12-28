using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class AudioVoice_LongCup : AudioVoice
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x00022CC4 File Offset: 0x000210C4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.startLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.startWaiting));
		this.voice.start(true);
		Global.self.canBePaused = false;
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x00022D44 File Offset: 0x00021144
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "ShowMail")
		{
			this.mailObject.SetActive(true);
			Global.self.canBePaused = true;
			Audio.self.playOneShot("74fb1c0e-aea6-4fd7-aabd-4e50722b011a", 1f);
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x00022D98 File Offset: 0x00021198
	public override void subsctibeToEnding(endTextControl item)
	{
		string endText = LevelVoice.getEndText(this.endLine, Global.self.currLanguage);
		item.SetEnding(endText, false);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x00022DC3 File Offset: 0x000211C3
	private void startWaiting(VoiceLine line)
	{
		base.StartCoroutine(this.idleVoice());
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x00022DD4 File Offset: 0x000211D4
	private IEnumerator idleVoice()
	{
		yield return new WaitForSeconds(5f);
		if (this.active && this.count == 0 && (this.voice == null || (this.voice != null && this.voice.removed)))
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupLongCup_ItsWaiting);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x00022DEF File Offset: 0x000211EF
	private void lineEnded(VoiceLine line)
	{
		this.showNextObj = true;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x00022DF8 File Offset: 0x000211F8
	public void takeOut()
	{
		if (this.count > 2 || !this.active)
		{
			return;
		}
		if (this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.showNextObj = false;
		if (this.count == 0)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupLongCup_WaitThats);
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.lineEnded));
			this.voice.start(true);
		}
		else if (this.count == 1)
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupLongCup_ThatForMy);
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.lineEnded));
			this.voice.start(true);
		}
		else
		{
			this.voice = Audio.self.playVoice(Voices.VoicePack03.CupLongCup_HereYouGo);
			this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.lineEnded));
			this.voice.start(true);
		}
		this.count++;
	}

	// Token: 0x04000ED3 RID: 3795
	[Space(10f)]
	public StandaloneLevelVoice startLine;

	// Token: 0x04000ED4 RID: 3796
	public StandaloneLevelVoice endLine;

	// Token: 0x04000ED5 RID: 3797
	private int count;

	// Token: 0x04000ED6 RID: 3798
	[HideInInspector]
	public GameObject mailObject;

	// Token: 0x04000ED7 RID: 3799
	[HideInInspector]
	public bool showNextObj = true;
}
