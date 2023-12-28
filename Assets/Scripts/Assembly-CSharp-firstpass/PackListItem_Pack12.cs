using System;
using UnityEngine;

// Token: 0x02000570 RID: 1392
public class PackListItem_Pack12 : MonoBehaviour, IDynamicPackListItem
{
	// Token: 0x0600200A RID: 8202 RVA: 0x0009C094 File Offset: 0x0009A494
	public Transform addPuzzle()
	{
		if (SerializableGameStats.self.isGameFinished)
		{
			return null;
		}
		if (Global.self.currPuzzle.name == this.afterPuzzle.name && AwardController.self.getCurrentProgress(true) == 100)
		{
			return this.insertPuzzle;
		}
		return null;
	}

	// Token: 0x04002337 RID: 9015
	public Transform afterPuzzle;

	// Token: 0x04002338 RID: 9016
	public Transform insertPuzzle;
}
