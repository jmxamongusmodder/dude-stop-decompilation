using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000192 RID: 402
	[CallbackIdentity(1323)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		// Token: 0x040006AC RID: 1708
		public const int k_iCallback = 1323;

		// Token: 0x040006AD RID: 1709
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006AE RID: 1710
		public AppId_t m_nAppID;
	}
}
