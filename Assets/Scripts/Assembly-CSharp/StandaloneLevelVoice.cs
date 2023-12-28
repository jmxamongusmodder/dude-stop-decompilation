using System;

// Token: 0x020004F2 RID: 1266
[Serializable]
public sealed class StandaloneLevelVoice
{
	// Token: 0x06001D90 RID: 7568 RVA: 0x00081D8A File Offset: 0x0008018A
	public StandaloneLevelVoice(string bankName, string levelVoiceId)
	{
		this.bankName = bankName;
		this.levelVoiceId = levelVoiceId;
	}

	// Token: 0x04001E33 RID: 7731
	public string bankName;

	// Token: 0x04001E34 RID: 7732
	public string levelVoiceId;
}
