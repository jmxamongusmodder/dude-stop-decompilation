using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000182 RID: 386
	[CallbackIdentity(1303)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncProgress_t
	{
		// Token: 0x04000651 RID: 1617
		public const int k_iCallback = 1303;

		// Token: 0x04000652 RID: 1618
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchCurrentFile;

		// Token: 0x04000653 RID: 1619
		public AppId_t m_nAppID;

		// Token: 0x04000654 RID: 1620
		public uint m_uBytesTransferredThisChunk;

		// Token: 0x04000655 RID: 1621
		public double m_dAppPercentComplete;

		// Token: 0x04000656 RID: 1622
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUploading;
	}
}
