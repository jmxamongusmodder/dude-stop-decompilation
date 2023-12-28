using System;

// Token: 0x020004F3 RID: 1267
public class StandaloneLevelVoiceGuid
{
	// Token: 0x06001D91 RID: 7569 RVA: 0x00081DA0 File Offset: 0x000801A0
	public StandaloneLevelVoiceGuid(StandaloneLevelVoice entry, Guid guid, string fmodName)
	{
		this.bankName = entry.bankName;
		this.levelVoiceId = entry.levelVoiceId;
		this.guid = guid;
		this.fmodName = fmodName;
	}

	// Token: 0x04001E35 RID: 7733
	public string bankName;

	// Token: 0x04001E36 RID: 7734
	public string levelVoiceId;

	// Token: 0x04001E37 RID: 7735
	public Guid guid;

	// Token: 0x04001E38 RID: 7736
	public string fmodName;
}
