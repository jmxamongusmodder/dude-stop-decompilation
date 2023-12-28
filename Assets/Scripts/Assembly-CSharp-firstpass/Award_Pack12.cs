using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class Award_Pack12 : Award
{
	// Token: 0x060013B3 RID: 5043 RVA: 0x0003131A File Offset: 0x0002F71A
	protected override void Start()
	{
		if (this.showIfGameFinished == SerializableGameStats.self.isGameFinished)
		{
			base.Start();
		}
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x00031337 File Offset: 0x0002F737
	public override void setCupState()
	{
		if (this.showIfGameFinished == SerializableGameStats.self.isGameFinished)
		{
			base.setCupState();
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001076 RID: 4214
	[Space(10f)]
	public bool showIfGameFinished = true;
}
