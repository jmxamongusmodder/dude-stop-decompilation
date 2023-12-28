using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000186 RID: 390
	[CallbackIdentity(1311)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		// Token: 0x04000662 RID: 1634
		public const int k_iCallback = 1311;

		// Token: 0x04000663 RID: 1635
		public EResult m_eResult;

		// Token: 0x04000664 RID: 1636
		public PublishedFileId_t m_nPublishedFileId;
	}
}
