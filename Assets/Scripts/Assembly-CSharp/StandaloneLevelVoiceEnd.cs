using System;

// Token: 0x020004F4 RID: 1268
public class StandaloneLevelVoiceEnd
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x00081DCE File Offset: 0x000801CE
	public StandaloneLevelVoiceEnd(StandaloneLevelVoice entry, Guid guid, string fmodName, string endText)
	{
		this.entry = new StandaloneLevelVoiceGuid(entry, guid, fmodName);
		this.fmodName = fmodName;
		this.endText = endText;
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x00081DF3 File Offset: 0x000801F3
	public StandaloneLevelVoiceEnd(StandaloneLevelVoiceGuid entry, string endText)
	{
		this.entry = entry;
		this.fmodName = entry.fmodName;
		this.endText = endText;
	}

	// Token: 0x04001E39 RID: 7737
	public StandaloneLevelVoiceGuid entry;

	// Token: 0x04001E3A RID: 7738
	public string fmodName;

	// Token: 0x04001E3B RID: 7739
	public string endText;
}
