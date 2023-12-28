using System;
using ExcelData;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class Award_Duck : Award
{
	// Token: 0x060013A4 RID: 5028 RVA: 0x00030FA8 File Offset: 0x0002F3A8
	public override void setCupState()
	{
		base.setCupState();
		if (SerializableGameStats.self.isGameFinished)
		{
			this.noDuckSprite.SetActive(true);
			this.cup.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00030FDC File Offset: 0x0002F3DC
	protected override void setText()
	{
		if (SerializableGameStats.self.isGameFinished)
		{
			AwardData awardData = AwardData.Get(this.awardName, Global.self.currLanguage);
			if (awardData == null)
			{
				this.text = "ERROR";
				this.textHas = "ERROR";
				return;
			}
			string title = WordTranslationContainer.Get(WordTranslationContainer.Theme.PACK_MENU, "NO_DUCK_AWARD_TITLE", Global.self.currLanguage);
			string descr = WordTranslationContainer.Get(WordTranslationContainer.Theme.PACK_MENU, "NO_DUCK_AWARD_DESCR", Global.self.currLanguage);
			this.text = string.Empty;
			this.textHas = TextFormating.formatAcquiredAward(title, descr, awardData.good, awardData.reqPercent, awardData.reqCount, awardData.cup100);
			this.text = TextFormating.format(this.text);
			this.textHas = TextFormating.format(this.textHas);
		}
		else
		{
			base.setText();
		}
	}

	// Token: 0x04001075 RID: 4213
	public GameObject noDuckSprite;
}
