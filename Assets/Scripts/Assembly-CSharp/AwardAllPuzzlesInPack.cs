using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class AwardAllPuzzlesInPack : AwardConditionAbstract
{
	// Token: 0x060013BE RID: 5054 RVA: 0x00031654 File Offset: 0x0002FA54
	public override void setAward()
	{
		base.setAward();
		this.isSaved = true;
		Global.self.Save();
		if ((from x in this.PuzzleList
		where x.GetComponent<PuzzleStats>() != null && x.GetComponent<PuzzleStats>().HasBadEnd
		select x).Count((Transform x) => SerializablePuzzleStats.Get(x.name).solvedAsBad == 0) == 0)
		{
			Global.self.GetCup(this.award);
		}
		if ((from x in this.PuzzleList
		where x.GetComponent<PuzzleStats>() != null && x.GetComponent<PuzzleStats>().HasGoodEnd
		select x).Count((Transform x) => SerializablePuzzleStats.Get(x.name).solvedAsGood == 0) == 0)
		{
			Global.self.GetCup(this.awardGood);
		}
		if ((from x in this.PuzzleList
		where x.GetComponent<PuzzleStats>() != null && (x.GetComponent<PuzzleStats>().HasBadEnd || x.GetComponent<PuzzleStats>().HasGoodEnd)
		select x).Count((Transform x) => SerializablePuzzleStats.Get(x.name).playedTimes == 0) == 0)
		{
			Global.self.GetCup(this.everySolved);
		}
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x00031798 File Offset: 0x0002FB98
	public void setList()
	{
		Transform packList = base.transform.parent.GetComponent<levelPackControl>().packList;
		PackListItem[] components = packList.GetComponents<PackListItem>();
		this.PuzzleList = new List<Transform>();
		components.ToList<PackListItem>().ForEach(delegate(PackListItem x)
		{
			this.PuzzleList.AddRange(x.List);
		});
	}

	// Token: 0x04001078 RID: 4216
	[Tooltip("Award for GOOD completion. normal Award is for the BAD completion")]
	public AwardName awardGood;

	// Token: 0x04001079 RID: 4217
	[Tooltip("Cup for solving all puzzles in the list, no matter bad or good")]
	public AwardName everySolved;

	// Token: 0x0400107A RID: 4218
	[Space(10f)]
	private List<Transform> PuzzleList;
}
