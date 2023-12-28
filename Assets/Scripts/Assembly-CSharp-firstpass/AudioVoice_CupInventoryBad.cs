using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class AudioVoice_CupInventoryBad : AudioVoice
{
	// Token: 0x060010FA RID: 4346 RVA: 0x0001CC50 File Offset: 0x0001B050
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0001CCAC File Offset: 0x0001B0AC
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "showCup":
			this.showCups = true;
			break;
		case "slip":
			this.dropCupNow = true;
			break;
		case "dirty":
			this.dirtyCupNow = true;
			break;
		case "showDuck":
			this.showDuckNow = true;
			break;
		case "showBlack":
			this.showDuckBlack = true;
			UIControl.self.hideSubtitles();
			break;
		case "hideBlack":
			this.hideDuckBlack = true;
			break;
		case "showCups":
			this.duckEnded = true;
			break;
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0001CDCC File Offset: 0x0001B1CC
	public void pickPotato(bool first)
	{
		if (this.firstPotato && first)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.potatoFirst);
			this.voice.start(true);
		}
		else if (!this.firstPotato && this.midPotatoIndex < this.midPotato.Length && !this.potatoPlaying)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.midPotato[this.midPotatoIndex]);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.potatoPlaying = false;
			});
			this.voice.start(true);
			this.midPotatoIndex++;
			this.potatoPlaying = true;
		}
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0001CEDC File Offset: 0x0001B2DC
	public void pickCupDrop()
	{
		this.playVoice(this.slipAway);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0001CEEA File Offset: 0x0001B2EA
	public void pickCupDirty()
	{
		this.playVoice(this.dirty);
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0001CEF8 File Offset: 0x0001B2F8
	public void pickCupDuck()
	{
		this.playVoice(this.duck);
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0001CF08 File Offset: 0x0001B308
	private void playVoice(StandaloneLevelVoice line)
	{
		if (!this.active)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		this.firstPotato = false;
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0001CF84 File Offset: 0x0001B384
	public override void subsctibeToEnding(endTextControl item)
	{
		StandaloneLevelVoice entry = this.lastPotato;
		StandaloneLevelVoiceEnd voice = LevelVoice.getVoice(entry, LevelVoice.Type.End, new bool?(true), Global.self.currLanguage);
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(voice.entry);
		base.subscribeToMarkers(item, true);
		this.voice.start(true);
	}

	// Token: 0x04000DF6 RID: 3574
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000DF7 RID: 3575
	public StandaloneLevelVoice potatoFirst;

	// Token: 0x04000DF8 RID: 3576
	public StandaloneLevelVoice slipAway;

	// Token: 0x04000DF9 RID: 3577
	public StandaloneLevelVoice dirty;

	// Token: 0x04000DFA RID: 3578
	public StandaloneLevelVoice duck;

	// Token: 0x04000DFB RID: 3579
	public StandaloneLevelVoice lastPotato;

	// Token: 0x04000DFC RID: 3580
	public StandaloneLevelVoice[] midPotato;

	// Token: 0x04000DFD RID: 3581
	private bool firstPotato = true;

	// Token: 0x04000DFE RID: 3582
	private int midPotatoIndex;

	// Token: 0x04000DFF RID: 3583
	private bool potatoPlaying;

	// Token: 0x04000E00 RID: 3584
	[HideInInspector]
	public bool showCups;

	// Token: 0x04000E01 RID: 3585
	[HideInInspector]
	public bool dropCupNow;

	// Token: 0x04000E02 RID: 3586
	[HideInInspector]
	public bool dirtyCupNow;

	// Token: 0x04000E03 RID: 3587
	[HideInInspector]
	public bool showDuckNow;

	// Token: 0x04000E04 RID: 3588
	[HideInInspector]
	public bool showDuckBlack;

	// Token: 0x04000E05 RID: 3589
	[HideInInspector]
	public bool hideDuckBlack;

	// Token: 0x04000E06 RID: 3590
	[HideInInspector]
	public bool duckEnded;
}
