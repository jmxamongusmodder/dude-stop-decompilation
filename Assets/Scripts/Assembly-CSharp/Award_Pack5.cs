using System;

// Token: 0x02000318 RID: 792
public class Award_Pack5 : Award
{
	// Token: 0x060013BC RID: 5052 RVA: 0x0003154D File Offset: 0x0002F94D
	public override void setCupState()
	{
		if (Global.self.cupList[this.awardName] == CupStatus.ShowAnimation)
		{
			base.transform.parent.GetComponent<AudioVoice_Pack05>().getCheeseCup();
		}
		base.setCupState();
	}
}
