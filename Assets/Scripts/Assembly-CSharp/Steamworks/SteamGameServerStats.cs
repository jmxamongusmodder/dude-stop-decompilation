using System;

namespace Steamworks
{
	// Token: 0x02000109 RID: 265
	public static class SteamGameServerStats
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0000ABCE File Offset: 0x00008FCE
		public static SteamAPICall_t RequestUserStats(CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerStats_RequestUserStats(steamIDUser);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000ABE0 File Offset: 0x00008FE0
		public static bool GetUserStat(CSteamID steamIDUser, string pchName, out int pData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_GetUserStat(steamIDUser, utf8StringHandle, out pData);
			}
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000AC28 File Offset: 0x00009028
		public static bool GetUserStat(CSteamID steamIDUser, string pchName, out float pData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_GetUserStat_(steamIDUser, utf8StringHandle, out pData);
			}
			return result;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000AC70 File Offset: 0x00009070
		public static bool GetUserAchievement(CSteamID steamIDUser, string pchName, out bool pbAchieved)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_GetUserAchievement(steamIDUser, utf8StringHandle, out pbAchieved);
			}
			return result;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000ACB8 File Offset: 0x000090B8
		public static bool SetUserStat(CSteamID steamIDUser, string pchName, int nData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_SetUserStat(steamIDUser, utf8StringHandle, nData);
			}
			return result;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000AD00 File Offset: 0x00009100
		public static bool SetUserStat(CSteamID steamIDUser, string pchName, float fData)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_SetUserStat_(steamIDUser, utf8StringHandle, fData);
			}
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000AD48 File Offset: 0x00009148
		public static bool UpdateUserAvgRateStat(CSteamID steamIDUser, string pchName, float flCountThisSession, double dSessionLength)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_UpdateUserAvgRateStat(steamIDUser, utf8StringHandle, flCountThisSession, dSessionLength);
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000AD90 File Offset: 0x00009190
		public static bool SetUserAchievement(CSteamID steamIDUser, string pchName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_SetUserAchievement(steamIDUser, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000ADD4 File Offset: 0x000091D4
		public static bool ClearUserAchievement(CSteamID steamIDUser, string pchName)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamGameServerStats_ClearUserAchievement(steamIDUser, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000AE18 File Offset: 0x00009218
		public static SteamAPICall_t StoreUserStats(CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerStats_StoreUserStats(steamIDUser);
		}
	}
}
