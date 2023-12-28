using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000100 RID: 256
	public static class SteamAppList
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00008C2C File Offset: 0x0000702C
		public static uint GetNumInstalledApps()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetNumInstalledApps();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00008C38 File Offset: 0x00007038
		public static uint GetInstalledApps(AppId_t[] pvecAppID, uint unMaxAppIDs)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetInstalledApps(pvecAppID, unMaxAppIDs);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00008C48 File Offset: 0x00007048
		public static int GetAppName(AppId_t nAppID, out string pchName, int cchNameMax)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameMax);
			int num = NativeMethods.ISteamAppList_GetAppName(nAppID, intPtr, cchNameMax);
			pchName = ((num == -1) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00008C88 File Offset: 0x00007088
		public static int GetAppInstallDir(AppId_t nAppID, out string pchDirectory, int cchNameMax)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameMax);
			int num = NativeMethods.ISteamAppList_GetAppInstallDir(nAppID, intPtr, cchNameMax);
			pchDirectory = ((num == -1) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00008CC6 File Offset: 0x000070C6
		public static int GetAppBuildId(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetAppBuildId(nAppID);
		}
	}
}
