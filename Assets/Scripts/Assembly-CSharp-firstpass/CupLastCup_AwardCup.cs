using System;

// Token: 0x02000358 RID: 856
public class CupLastCup_AwardCup : PuzzleCup
{
	// Token: 0x060014DB RID: 5339 RVA: 0x0003ACF8 File Offset: 0x000390F8
	private void OnMouseDown()
	{
		if (!base.enabled || !this.canBeAcquired)
		{
			return;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_LastCup>().pickUpCup();
		Global.CupAcquired(base.transform);
	}

	// Token: 0x04001267 RID: 4711
	public bool canBeAcquired;
}
