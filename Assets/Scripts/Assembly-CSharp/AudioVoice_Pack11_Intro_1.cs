using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class AudioVoice_Pack11_Intro_1 : AudioVoice
{
	// Token: 0x06001262 RID: 4706 RVA: 0x00027E08 File Offset: 0x00026208
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active && !this.loaded)
		{
			GlitchEffectController.self.startGlitch(0.5f);
			UIControl.self.makeNoConnectionScreen(15f);
			Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
			Audio.self.playLoopSound("ef7abcd3-2afb-49b8-8428-ace9ac9ae158");
			this.loaded = true;
			this.voice = Audio.self.playVoice(this.onLoad);
			this.voice.start(true);
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x00027E94 File Offset: 0x00026294
	public void onContinue()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
		GlitchEffectController.self.startGlitch(0.5f);
		Global.self.changeTransitionTime(0.5f);
		if (Global.self.transitionCurrentSound.isValid())
		{
			Global.self.transitionCurrentSound.stop(STOP_MODE.IMMEDIATE);
		}
		UIControl.self.hideNoConnectionScreen();
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.onClick);
		this.voice.start(true);
		Global.self.getNextPuzzleToChangeVoiceParent().GetComponent<AudioVoiceReceive>().receiveVoice(this.voice);
	}

	// Token: 0x04000F6D RID: 3949
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000F6E RID: 3950
	public StandaloneLevelVoice onClick;

	// Token: 0x04000F6F RID: 3951
	private bool loaded;
}
