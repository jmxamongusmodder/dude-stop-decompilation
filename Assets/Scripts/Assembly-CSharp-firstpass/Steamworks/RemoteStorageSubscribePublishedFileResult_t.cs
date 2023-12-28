using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000188 RID: 392
	[CallbackIdentity(1313)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x0400066A RID: 1642
		public const int k_iCallback = 1313;

		// Token: 0x0400066B RID: 1643
		public EResult m_eResult;

		// Token: 0x0400066C RID: 1644
		public PublishedFileId_t m_nPublishedFileId;
	}
}
