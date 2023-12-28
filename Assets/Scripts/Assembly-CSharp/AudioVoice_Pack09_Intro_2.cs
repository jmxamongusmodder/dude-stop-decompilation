using System;

// Token: 0x020002E0 RID: 736
public class AudioVoice_Pack09_Intro_2 : AudioVoice
{
	// Token: 0x0600123A RID: 4666 RVA: 0x00026D59 File Offset: 0x00025159
	private void Awake()
	{
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00026D68 File Offset: 0x00025168
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			global::Console.self.canOpen = true;
		});
		this.voice.start(true);
		global::Console.self.canOpen = false;
		Audio.self.playLoopSound("ef7abcd3-2afb-49b8-8428-ace9ac9ae158");
	}
}
