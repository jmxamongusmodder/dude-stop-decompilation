using System;

// Token: 0x020002EA RID: 746
public class AudioVoice_Pack2 : AudioVoice
{
	// Token: 0x06001281 RID: 4737 RVA: 0x000287F0 File Offset: 0x00026BF0
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.stopReached));
		this.voice.start(true);
		this.clickAllowed = false;
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x00028851 File Offset: 0x00026C51
	private void stopReached(VoiceLine line)
	{
		this.clickAllowed = true;
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x0002885A File Offset: 0x00026C5A
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		return !this.active || (this.voice == null || this.voice.removed) || this.clickAllowed;
	}

	// Token: 0x04000F8B RID: 3979
	private bool clickAllowed;
}
