using System;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class AudioVoice_WrongDress : AudioVoiceDefault
{
	// Token: 0x0600132F RID: 4911 RVA: 0x0002EEFC File Offset: 0x0002D2FC
	protected override void setActiveDefault()
	{
		if (Global.self.endCutscenePack12)
		{
			this.voice = Audio.self.playVoice(LevelVoice.getVoice(this.ENDLines, LevelVoice.Type.Start, null));
			this.voice.start(true);
		}
		else
		{
			base.setActiveDefault();
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x0002EF54 File Offset: 0x0002D354
	public void onWrongSnap()
	{
		base.playVoice(this.snapLine, !Global.self.endCutscenePack12, true);
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x0002EF71 File Offset: 0x0002D371
	public void socksAboveShoes()
	{
		base.playVoice(this.socksAboveLine, !Global.self.endCutscenePack12, true);
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0002EF8E File Offset: 0x0002D38E
	public void alotOnHuman()
	{
		base.playVoice(this.newTrendLine, !Global.self.endCutscenePack12, true);
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x0002EFAB File Offset: 0x0002D3AB
	public override void subsctibeToEnding(endTextControl item)
	{
		if (Global.self.endCutscenePack12)
		{
			base.playSpecificEnd(this.ENDLines, item);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x04001028 RID: 4136
	[Space(10f)]
	public StandaloneLevelVoice ENDLines;

	// Token: 0x04001029 RID: 4137
	[Space(10f)]
	public StandaloneLevelVoice snapLine;

	// Token: 0x0400102A RID: 4138
	public StandaloneLevelVoice socksAboveLine;

	// Token: 0x0400102B RID: 4139
	public StandaloneLevelVoice newTrendLine;
}
