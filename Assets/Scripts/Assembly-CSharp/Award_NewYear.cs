using System;

// Token: 0x02000313 RID: 787
public class Award_NewYear : Award
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060013AC RID: 5036 RVA: 0x00031175 File Offset: 0x0002F575
	private bool showCup
	{
		get
		{
			return Global.self.cupList[AwardName.ChristmasEvent] != CupStatus.Empty || Global.self.nyCoinCurrent > 0;
		}
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x0003119D File Offset: 0x0002F59D
	protected override void Start()
	{
		if (this.showCup)
		{
			base.Start();
		}
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000311B0 File Offset: 0x0002F5B0
	public override void setCupState()
	{
		if (this.showCup)
		{
			base.setCupState();
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x000311D4 File Offset: 0x0002F5D4
	protected override void setText()
	{
		base.setText();
		int num = this.text.IndexOf("</color>");
		if (num <= 0)
		{
			return;
		}
		this.text = this.text.Insert(num, string.Format(" ({0}/{1})", Global.self.nyCoinCurrent, Global.self.nyCoinMax));
	}
}
