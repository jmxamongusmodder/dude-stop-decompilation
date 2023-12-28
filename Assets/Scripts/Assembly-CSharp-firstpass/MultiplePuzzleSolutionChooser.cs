using System;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public class MultiplePuzzleSolutionChooser : MonoBehaviour
{
	// Token: 0x06001798 RID: 6040 RVA: 0x0004F604 File Offset: 0x0004DA04
	private void Awake()
	{
		if (this.chosen)
		{
			return;
		}
		MultiplePuzzleSolutionChooser[] componentsInPuzzleStats = this.GetComponentsInPuzzleStats(true);
		foreach (MultiplePuzzleSolutionChooser multiplePuzzleSolutionChooser in componentsInPuzzleStats)
		{
			multiplePuzzleSolutionChooser.chosen = true;
			multiplePuzzleSolutionChooser.gameObject.SetActive(false);
		}
		componentsInPuzzleStats.GetRandom<MultiplePuzzleSolutionChooser>().SetGoodBad();
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x0004F65D File Offset: 0x0004DA5D
	public void SetGoodBad()
	{
		this.GetPuzzleStats().goBadAfterTime = this.goBadAfterTime;
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001564 RID: 5476
	public bool goBadAfterTime;

	// Token: 0x04001565 RID: 5477
	protected bool chosen;
}
