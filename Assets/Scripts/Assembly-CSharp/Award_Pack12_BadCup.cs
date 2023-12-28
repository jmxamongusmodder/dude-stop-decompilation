using System;

// Token: 0x02000316 RID: 790
public class Award_Pack12_BadCup : Award_Pack12
{
	// Token: 0x060013B6 RID: 5046 RVA: 0x00031370 File Offset: 0x0002F770
	public override void setCupState()
	{
		if (Global.self.getNextPuzzleToChangeVoiceParent() != null)
		{
			levelPackControl component = Global.self.getNextPuzzleToChangeVoiceParent().GetComponent<levelPackControl>();
			if (component != null && component.packIndex != 12)
			{
				base.setCupState();
				return;
			}
		}
		if (SerializableGameStats.self.isGameFinished && SerializablePackSavedStats.Get(11).bestBadSolvedPuzzleCount == SerializablePackSavedStats.Get(11).badEndCount)
		{
			Global.self.GetCup(this.awardName);
		}
		base.setCupState();
	}
}
