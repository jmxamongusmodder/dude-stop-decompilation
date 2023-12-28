using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A2 RID: 418
	[CallbackIdentity(3405)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ItemInstalled_t
	{
		// Token: 0x040006E8 RID: 1768
		public const int k_iCallback = 3405;

		// Token: 0x040006E9 RID: 1769
		public AppId_t m_unAppID;

		// Token: 0x040006EA RID: 1770
		public PublishedFileId_t m_nPublishedFileId;
	}
}
