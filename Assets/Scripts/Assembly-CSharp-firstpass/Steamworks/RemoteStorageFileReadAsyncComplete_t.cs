using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019B RID: 411
	[CallbackIdentity(1332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x040006CF RID: 1743
		public const int k_iCallback = 1332;

		// Token: 0x040006D0 RID: 1744
		public SteamAPICall_t m_hFileReadAsync;

		// Token: 0x040006D1 RID: 1745
		public EResult m_eResult;

		// Token: 0x040006D2 RID: 1746
		public uint m_nOffset;

		// Token: 0x040006D3 RID: 1747
		public uint m_cubRead;
	}
}
