using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A3 RID: 419
	[CallbackIdentity(3406)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadItemResult_t
	{
		// Token: 0x040006EB RID: 1771
		public const int k_iCallback = 3406;

		// Token: 0x040006EC RID: 1772
		public AppId_t m_unAppID;

		// Token: 0x040006ED RID: 1773
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006EE RID: 1774
		public EResult m_eResult;
	}
}
