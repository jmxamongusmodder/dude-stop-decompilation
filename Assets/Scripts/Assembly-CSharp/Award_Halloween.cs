using System;

// Token: 0x02000312 RID: 786
public class Award_Halloween : Award
{
	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000310BC File Offset: 0x0002F4BC
	private bool showCup
	{
		get
		{
			return Global.self.cupList[AwardName.Halloween] != CupStatus.Empty || Global.self.ghostCountCurrent > 0;
		}
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000310E4 File Offset: 0x0002F4E4
	protected override void Start()
	{
		if (this.showCup)
		{
			base.Start();
		}
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000310F7 File Offset: 0x0002F4F7
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

	// Token: 0x060013AA RID: 5034 RVA: 0x0003111C File Offset: 0x0002F51C
	protected override void setText()
	{
		base.setText();
		this.text = this.text.Replace("#", Global.self.ghostCountCurrent + "/" + Global.self.ghostCountMax);
	}
}
