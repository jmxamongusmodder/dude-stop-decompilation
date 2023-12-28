using System;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class AwardController_Pack12 : AwardController
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060013EC RID: 5100 RVA: 0x000320E8 File Offset: 0x000304E8
	protected override Transform resultScreen
	{
		get
		{
			if (!SerializableGameStats.self.isGameFinished)
			{
				return null;
			}
			return base.resultScreen;
		}
	}
}
