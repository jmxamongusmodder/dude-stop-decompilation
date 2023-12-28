using System;

// Token: 0x02000342 RID: 834
[Serializable]
public class AwardNameCheatList
{
	// Token: 0x0600145E RID: 5214 RVA: 0x0003536C File Offset: 0x0003376C
	public AwardNameCheatList(AwardName n, CupStatus s, int index)
	{
		this.name = n;
		this.state = s;
		this.packIndex = index;
	}

	// Token: 0x0400119F RID: 4511
	public AwardName name;

	// Token: 0x040011A0 RID: 4512
	public CupStatus state;

	// Token: 0x040011A1 RID: 4513
	public int packIndex;
}
