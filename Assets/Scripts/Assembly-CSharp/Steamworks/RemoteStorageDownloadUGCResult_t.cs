using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018C RID: 396
	[CallbackIdentity(1317)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x0400067A RID: 1658
		public const int k_iCallback = 1317;

		// Token: 0x0400067B RID: 1659
		public EResult m_eResult;

		// Token: 0x0400067C RID: 1660
		public UGCHandle_t m_hFile;

		// Token: 0x0400067D RID: 1661
		public AppId_t m_nAppID;

		// Token: 0x0400067E RID: 1662
		public int m_nSizeInBytes;

		// Token: 0x0400067F RID: 1663
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName;

		// Token: 0x04000680 RID: 1664
		public ulong m_ulSteamIDOwner;
	}
}
