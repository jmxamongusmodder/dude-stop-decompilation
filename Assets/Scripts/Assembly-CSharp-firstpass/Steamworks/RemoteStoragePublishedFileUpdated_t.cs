using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000199 RID: 409
	[CallbackIdentity(1330)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x040006C9 RID: 1737
		public const int k_iCallback = 1330;

		// Token: 0x040006CA RID: 1738
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006CB RID: 1739
		public AppId_t m_nAppID;

		// Token: 0x040006CC RID: 1740
		public ulong m_ulUnused;
	}
}
