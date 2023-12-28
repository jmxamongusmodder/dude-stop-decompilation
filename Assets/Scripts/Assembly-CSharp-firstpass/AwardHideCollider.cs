using System;

// Token: 0x02000322 RID: 802
public class AwardHideCollider : Award
{
	// Token: 0x060013FB RID: 5115 RVA: 0x00032774 File Offset: 0x00030B74
	public override void setCupState()
	{
		if (Global.self.cupList[this.awardName] == CupStatus.ShowAnimation || Global.self.cupList[this.awardName] == CupStatus.Exist)
		{
			base.gameObject.SetActive(false);
		}
	}
}
