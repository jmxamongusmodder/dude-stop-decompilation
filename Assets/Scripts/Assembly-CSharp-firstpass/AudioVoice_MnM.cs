using System;

// Token: 0x020002D3 RID: 723
public class AudioVoice_MnM : AudioVoiceEndAchievement
{
	// Token: 0x060011CE RID: 4558 RVA: 0x00023198 File Offset: 0x00021598
	public void MadeMess()
	{
		if (!this.active || !base.enabled)
		{
			return;
		}
		base.playVoice(this.onMess, true, true);
	}

	// Token: 0x04000EDE RID: 3806
	public StandaloneLevelVoice onMess;
}
