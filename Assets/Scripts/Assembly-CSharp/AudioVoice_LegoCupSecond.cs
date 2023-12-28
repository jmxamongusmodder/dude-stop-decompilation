using System;

// Token: 0x020002CE RID: 718
public class AudioVoice_LegoCupSecond : AudioVoiceCups
{
	// Token: 0x060011B4 RID: 4532 RVA: 0x0001D0FC File Offset: 0x0001B4FC
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.voice != null)
		{
			this.voice.stop();
		}
		string endText = LevelVoice.getEndText(this.endVoice, Global.self.currLanguage);
		item.SetEnding(endText, false);
	}
}
