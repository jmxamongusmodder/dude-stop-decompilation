using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000101 RID: 257
	public static class SteamApps
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x00008CD3 File Offset: 0x000070D3
		public static bool BIsSubscribed()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribed();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00008CDF File Offset: 0x000070DF
		public static bool BIsLowViolence()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsLowViolence();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00008CEB File Offset: 0x000070EB
		public static bool BIsCybercafe()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsCybercafe();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00008CF7 File Offset: 0x000070F7
		public static bool BIsVACBanned()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsVACBanned();
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00008D03 File Offset: 0x00007103
		public static string GetCurrentGameLanguage()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetCurrentGameLanguage());
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00008D14 File Offset: 0x00007114
		public static string GetAvailableGameLanguages()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetAvailableGameLanguages());
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00008D25 File Offset: 0x00007125
		public static bool BIsSubscribedApp(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribedApp(appID);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00008D32 File Offset: 0x00007132
		public static bool BIsDlcInstalled(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsDlcInstalled(appID);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00008D3F File Offset: 0x0000713F
		public static uint GetEarliestPurchaseUnixTime(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetEarliestPurchaseUnixTime(nAppID);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00008D4C File Offset: 0x0000714C
		public static bool BIsSubscribedFromFreeWeekend()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribedFromFreeWeekend();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00008D58 File Offset: 0x00007158
		public static int GetDLCCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetDLCCount();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00008D64 File Offset: 0x00007164
		public static bool BGetDLCDataByIndex(int iDLC, out AppId_t pAppID, out bool pbAvailable, out string pchName, int cchNameBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = NativeMethods.ISteamApps_BGetDLCDataByIndex(iDLC, out pAppID, out pbAvailable, intPtr, cchNameBufferSize);
			pchName = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00008DA5 File Offset: 0x000071A5
		public static void InstallDLC(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_InstallDLC(nAppID);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00008DB2 File Offset: 0x000071B2
		public static void UninstallDLC(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_UninstallDLC(nAppID);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00008DBF File Offset: 0x000071BF
		public static void RequestAppProofOfPurchaseKey(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_RequestAppProofOfPurchaseKey(nAppID);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00008DCC File Offset: 0x000071CC
		public static bool GetCurrentBetaName(out string pchName, int cchNameBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = NativeMethods.ISteamApps_GetCurrentBetaName(intPtr, cchNameBufferSize);
			pchName = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00008E08 File Offset: 0x00007208
		public static bool MarkContentCorrupt(bool bMissingFilesOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_MarkContentCorrupt(bMissingFilesOnly);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00008E15 File Offset: 0x00007215
		public static uint GetInstalledDepots(AppId_t appID, DepotId_t[] pvecDepots, uint cMaxDepots)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetInstalledDepots(appID, pvecDepots, cMaxDepots);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00008E24 File Offset: 0x00007224
		public static uint GetAppInstallDir(AppId_t appID, out string pchFolder, uint cchFolderBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchFolderBufferSize);
			uint num = NativeMethods.ISteamApps_GetAppInstallDir(appID, intPtr, cchFolderBufferSize);
			pchFolder = ((num == 0U) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00008E61 File Offset: 0x00007261
		public static bool BIsAppInstalled(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsAppInstalled(appID);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00008E6E File Offset: 0x0000726E
		public static CSteamID GetAppOwner()
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamApps_GetAppOwner();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00008E80 File Offset: 0x00007280
		public static string GetLaunchQueryParam(string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				result = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetLaunchQueryParam(utf8StringHandle));
			}
			return result;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00008EC8 File Offset: 0x000072C8
		public static bool GetDlcDownloadProgress(AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetDlcDownloadProgress(nAppID, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00008ED7 File Offset: 0x000072D7
		public static int GetAppBuildId()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetAppBuildId();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00008EE3 File Offset: 0x000072E3
		public static void RequestAllProofOfPurchaseKeys()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_RequestAllProofOfPurchaseKeys();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00008EF0 File Offset: 0x000072F0
		public static SteamAPICall_t GetFileDetails(string pszFileName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszFileName))
			{
				result = (SteamAPICall_t)NativeMethods.ISteamApps_GetFileDetails(utf8StringHandle);
			}
			return result;
		}
	}
}
