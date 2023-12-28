using System;
using UnityEngine;

// Token: 0x0200057F RID: 1407
public class ScreenEndCard_Pack01 : ScreenEndCard
{
	// Token: 0x06002060 RID: 8288 RVA: 0x0009EF3C File Offset: 0x0009D33C
	public override void setScreen(Transform item)
	{
		base.setScreen(item);
		this.txt.translateText(false);
		this.txt.setTextNoTranslation(this.txt.currentText.Replace("#", Global.self.CountPackPlayedTimes(0).ToString()));
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x0009EF96 File Offset: 0x0009D396
	public override void bContinue()
	{
		if (!Global.self.currPuzzle.GetComponent<AudioVoice_Pack1_NoReward>().isClickAllowed(ClickWhileVoice.back))
		{
			return;
		}
		base.bContinue();
	}

	// Token: 0x040023A7 RID: 9127
	public LineTranslator txt;

	// Token: 0x040023A8 RID: 9128
	public ButtonTemplate buttonContinue;
}
