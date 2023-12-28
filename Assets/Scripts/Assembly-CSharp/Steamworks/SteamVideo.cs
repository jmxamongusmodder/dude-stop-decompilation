using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200011B RID: 283
	public static class SteamVideo
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0000F270 File Offset: 0x0000D670
		public static void GetVideoURL(AppId_t unVideoAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamVideo_GetVideoURL(unVideoAppID);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000F27D File Offset: 0x0000D67D
		public static bool IsBroadcasting(out int pnNumViewers)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamVideo_IsBroadcasting(out pnNumViewers);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000F28A File Offset: 0x0000D68A
		public static void GetOPFSettings(AppId_t unVideoAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamVideo_GetOPFSettings(unVideoAppID);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000F298 File Offset: 0x0000D698
		public static bool GetOPFStringForApp(AppId_t unVideoAppID, out string pchBuffer, ref int pnBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(pnBufferSize);
			bool flag = NativeMethods.ISteamVideo_GetOPFStringForApp(unVideoAppID, intPtr, ref pnBufferSize);
			pchBuffer = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
