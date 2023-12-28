using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000191 RID: 401
	[CallbackIdentity(1322)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x040006A9 RID: 1705
		public const int k_iCallback = 1322;

		// Token: 0x040006AA RID: 1706
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006AB RID: 1707
		public AppId_t m_nAppID;
	}
}
