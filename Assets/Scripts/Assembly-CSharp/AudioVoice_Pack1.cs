using System;

// Token: 0x020002E1 RID: 737
public class AudioVoice_Pack1 : AudioVoice
{
	// Token: 0x0600123E RID: 4670 RVA: 0x00026E0C File Offset: 0x0002520C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (SerializableGameStats.self.isGameFinishedJustNow || this.debugVoiceOn)
		{
			SerializableGameStats.self.isGameFinishedJustNow = false;
			Global.self.Save();
			this.canExit = false;
			this.voice = Audio.self.playVoice(this.voiceLine);
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				this.canExit = true;
				base.SetPackStartButton(true);
			});
			this.voice.start(true);
			Global.self.GetCup(AwardName.COMPLETE_THE_GAME);
		}
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x00026EA9 File Offset: 0x000252A9
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.canExit)
		{
			if (click == ClickWhileVoice.start)
			{
				base.SetPackStartButton(false);
			}
			return false;
		}
		base.StartMusic(click);
		return base.isClickAllowed(click);
	}

	// Token: 0x04000F50 RID: 3920
	private bool canExit = true;

	// Token: 0x04000F51 RID: 3921
	public bool debugVoiceOn;
}
