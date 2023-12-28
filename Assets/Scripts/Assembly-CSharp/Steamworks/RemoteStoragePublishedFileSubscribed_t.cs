using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000190 RID: 400
	[CallbackIdentity(1321)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x040006A6 RID: 1702
		public const int k_iCallback = 1321;

		// Token: 0x040006A7 RID: 1703
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006A8 RID: 1704
		public AppId_t m_nAppID;
	}
}
