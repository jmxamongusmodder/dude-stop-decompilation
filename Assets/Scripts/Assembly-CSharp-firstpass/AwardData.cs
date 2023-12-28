using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x0200031F RID: 799
public class AwardData
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060013EE RID: 5102 RVA: 0x00032109 File Offset: 0x00030509
	public static List<AwardData> data
	{
		get
		{
			if (AwardData._data == null)
			{
				AwardData._data = AwardData.Load();
			}
			return AwardData._data;
		}
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x00032124 File Offset: 0x00030524
	private static List<AwardData> Load()
	{
		return LocalizationLoader.self.LoadAwardData();
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00032130 File Offset: 0x00030530
	public static void ResetLanguage()
	{
		AwardData._data = AwardData.Load();
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x0003213C File Offset: 0x0003053C
	public static AwardData Get(AwardName name, string language)
	{
		return (from x in AwardData.data
		where x.index == (int)name && x.language == language
		select x).FirstOrDefault<AwardData>();
	}

	// Token: 0x0400109B RID: 4251
	private static List<AwardData> _data;

	// Token: 0x0400109C RID: 4252
	public int index;

	// Token: 0x0400109D RID: 4253
	public bool good;

	// Token: 0x0400109E RID: 4254
	public bool reqPercent;

	// Token: 0x0400109F RID: 4255
	public bool reqCount;

	// Token: 0x040010A0 RID: 4256
	public bool cup100;

	// Token: 0x040010A1 RID: 4257
	public bool achievement;

	// Token: 0x040010A2 RID: 4258
	public string language;

	// Token: 0x040010A3 RID: 4259
	public string titleAcquired;

	// Token: 0x040010A4 RID: 4260
	public string titleNotAcquired;

	// Token: 0x040010A5 RID: 4261
	public string descriptionAcquired;

	// Token: 0x040010A6 RID: 4262
	public string descriptionNotAcquired;

	// Token: 0x040010A7 RID: 4263
	public string shortDescriptionAcquired;
}
