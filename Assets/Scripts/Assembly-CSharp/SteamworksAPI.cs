using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class SteamworksAPI : MonoBehaviour
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x000858D4 File Offset: 0x00083CD4
	public static SteamworksAPI self
	{
		get
		{
			if (SteamworksAPI._self == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag("Steam");
				if (gameObject == null)
				{
					SteamworksAPI._self = null;
				}
				else
				{
					SteamworksAPI._self = gameObject.GetComponent<SteamworksAPI>();
				}
			}
			return SteamworksAPI._self;
		}
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00085924 File Offset: 0x00083D24
	private void Start()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			if (this.canPlayOnlyWithSteam)
			{
				if (Global.self.DEBUG)
				{
					Debug.Log("STEAM: Exit");
				}
				Application.Quit();
			}
		}
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x00085978 File Offset: 0x00083D78
	private void OnEnable()
	{
		if (!SteamManager.Initialized)
		{
			return;
		}
		this.onUserStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
		this.onUserStatStored = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.OnUserStatStored));
		this.onAchievementStored = Callback<UserAchievementStored_t>.Create(new Callback<UserAchievementStored_t>.DispatchDelegate(this.OnAchievementStored));
		SteamUserStats.RequestCurrentStats();
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x000859DB File Offset: 0x00083DDB
	public void Shutdown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x000859F9 File Offset: 0x00083DF9
	public static bool AcquireAchievement(string achievement)
	{
		return SteamworksAPI.AcquireAchievement(SteamAchievements.GetEntry(achievement));
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x00085A08 File Offset: 0x00083E08
	public static bool AcquireAchievement(SteamAchievements.Entry achievement)
	{
		if (!SteamworksAPI.self || !SteamworksAPI.self.enabled)
		{
			return false;
		}
		if (!SteamManager.Initialized)
		{
			return false;
		}
		if (achievement.achieved)
		{
			return false;
		}
		bool flag = SteamUserStats.SetAchievement(achievement.id);
		if (flag)
		{
			if (Global.self.DEBUG)
			{
				Debug.Log("Acuire {" + achievement.id + "}");
			}
			achievement.achieved = true;
			SteamUserStats.StoreStats();
			return true;
		}
		return false;
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x00085A9C File Offset: 0x00083E9C
	public void RemoveAchievement(SteamAchievements.Entry achievement)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (!achievement.achieved)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log("Removing {" + achievement.id + "}");
		}
		bool flag = SteamUserStats.ClearAchievement(achievement.id);
		if (flag)
		{
			achievement.achieved = false;
			SteamUserStats.StoreStats();
		}
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00085B14 File Offset: 0x00083F14
	public void SetStat(string statName, int value)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Setting {",
				statName,
				"} to {",
				value,
				"}"
			}));
		}
		SteamUserStats.SetStat(statName, value);
		SteamUserStats.StoreStats();
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x00085B8C File Offset: 0x00083F8C
	public void ResetStats()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log("Resetting all stats and achievements");
		}
		bool flag = SteamUserStats.ResetAllStats(true);
		if (Global.self.DEBUG)
		{
			Debug.Log(flag);
		}
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x00085BEC File Offset: 0x00083FEC
	private void OnUserStatStored(UserStatsStored_t callback)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized || callback.m_nGameID != this.gameId)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log("stats saved");
		}
		SteamUserStats.RequestCurrentStats();
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x00085C44 File Offset: 0x00084044
	private void OnUserStatsReceived(UserStatsReceived_t callback)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized || callback.m_nGameID != this.gameId)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log("load stats");
		}
		AwardName[] array = (AwardName[])Enum.GetValues(typeof(AwardName));
		foreach (AwardName name in array)
		{
			AwardData awardData = AwardData.Get(name, Global.self.currLanguage);
			if (awardData != null && awardData.achievement)
			{
				SteamAchievements.SetEntry(name.ToString());
			}
		}
		foreach (KeyValuePair<string, SteamAchievements.Entry> keyValuePair in SteamAchievements.GetAllEntries())
		{
			if (!SteamUserStats.GetAchievement(keyValuePair.Key, out keyValuePair.Value.achieved) && Global.self.DEBUG)
			{
				Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + keyValuePair.Key);
			}
		}
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x00085D8C File Offset: 0x0008418C
	private void OnAchievementStored(UserAchievementStored_t callback)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!SteamManager.Initialized || callback.m_nGameID != this.gameId)
		{
			return;
		}
		if (Global.self.DEBUG)
		{
			Debug.Log("Achievement " + callback.m_rgchAchievementName);
		}
	}

	// Token: 0x04002119 RID: 8473
	private static SteamworksAPI _self;

	// Token: 0x0400211A RID: 8474
	public bool canPlayOnlyWithSteam;

	// Token: 0x0400211B RID: 8475
	public ulong gameId;

	// Token: 0x0400211C RID: 8476
	public bool inputMode;

	// Token: 0x0400211D RID: 8477
	protected Callback<UserAchievementStored_t> onAchievementStored;

	// Token: 0x0400211E RID: 8478
	protected Callback<UserStatsReceived_t> onUserStatsReceived;

	// Token: 0x0400211F RID: 8479
	protected Callback<UserStatsStored_t> onUserStatStored;

	// Token: 0x04002120 RID: 8480
	[Range(0f, 10f)]
	public int i;
}
