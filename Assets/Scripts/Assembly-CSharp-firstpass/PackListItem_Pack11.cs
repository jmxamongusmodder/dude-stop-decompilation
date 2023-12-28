using System;
using System.Linq;
using UnityEngine;

// Token: 0x0200056E RID: 1390
public class PackListItem_Pack11 : MonoBehaviour, IDynamicPackListItem
{
	// Token: 0x06002005 RID: 8197 RVA: 0x0009BF74 File Offset: 0x0009A374
	public Transform addPuzzle()
	{
		if (!Global.self.currPuzzle.GetComponent<PuzzleStats>().isMenu && Global.self.previousPuzzleSolvedAsMonster == true)
		{
			int num = AwardController.self.getSolvedOrder().Count((bool x) => !x);
			if (num > 1)
			{
				return null;
			}
			int num2 = AwardController.self.getSolvedOrder().Count((bool x) => x);
			foreach (PackListItem_Pack11.PuzzleList puzzleList in this.list)
			{
				if (puzzleList.requiresBadCount <= num2 && SerializablePuzzleStats.Get(puzzleList.puzzle.name).loadedTimes <= 0)
				{
					return puzzleList.puzzle;
				}
			}
		}
		return null;
	}

	// Token: 0x04002332 RID: 9010
	public PackListItem_Pack11.PuzzleList[] list;

	// Token: 0x0200056F RID: 1391
	[Serializable]
	public class PuzzleList
	{
		// Token: 0x04002335 RID: 9013
		[Tooltip("How many bad puzzles needs to happen for this to trigger")]
		public int requiresBadCount;

		// Token: 0x04002336 RID: 9014
		public Transform puzzle;
	}
}
