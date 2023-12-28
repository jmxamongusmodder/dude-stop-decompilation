using System;

// Token: 0x02000314 RID: 788
public class Award_Pack1 : Award
{
	// Token: 0x060013B1 RID: 5041 RVA: 0x00031244 File Offset: 0x0002F644
	protected override void setText()
	{
		if (this.awardName == AwardName.None)
		{
			return;
		}
		AwardData awardData = AwardData.Get(this.awardName, Global.self.currLanguage);
		if (awardData == null)
		{
			this.text = "ERROR";
			this.textHas = "ERROR";
			return;
		}
		this.text = TextFormating.formatNotAcquiredAward(awardData.titleNotAcquired, awardData.descriptionNotAcquired, awardData.good, awardData.reqPercent, awardData.reqCount, awardData.cup100);
		this.textHas = TextFormating.formatAcquiredAward(awardData.titleAcquired, awardData.descriptionAcquired, awardData.good, true, awardData.reqCount, awardData.cup100);
		this.text = TextFormating.format(this.text);
		this.textHas = TextFormating.format(this.textHas);
	}
}
