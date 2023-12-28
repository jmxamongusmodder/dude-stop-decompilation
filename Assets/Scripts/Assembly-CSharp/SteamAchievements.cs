using System;
using System.Collections.Generic;

// Token: 0x0200050A RID: 1290
public static class SteamAchievements
{
	// Token: 0x06001DA9 RID: 7593 RVA: 0x0008566D File Offset: 0x00083A6D
	public static Dictionary<string, SteamAchievements.Entry> GetAllEntries()
	{
		return SteamAchievements.entries;
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x00085674 File Offset: 0x00083A74
	public static SteamAchievements.Entry GetEntry(string id)
	{
		if (!SteamAchievements.entries.ContainsKey(id))
		{
			SteamAchievements.entries.Add(id, new SteamAchievements.Entry
			{
				id = id
			});
		}
		return SteamAchievements.entries[id];
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x000856B8 File Offset: 0x00083AB8
	public static void SetEntry(string id)
	{
		if (!SteamAchievements.entries.ContainsKey(id))
		{
			SteamAchievements.entries.Add(id, new SteamAchievements.Entry
			{
				id = id
			});
		}
	}

	// Token: 0x04002111 RID: 8465
	private static Dictionary<string, SteamAchievements.Entry> entries = new Dictionary<string, SteamAchievements.Entry>();

	// Token: 0x0200050B RID: 1291
	public sealed class Entry
	{
		// Token: 0x04002112 RID: 8466
		public string id;

		// Token: 0x04002113 RID: 8467
		public bool achieved;
	}
}
