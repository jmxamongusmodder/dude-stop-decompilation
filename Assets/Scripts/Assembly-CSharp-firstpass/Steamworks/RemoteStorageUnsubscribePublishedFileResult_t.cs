using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018A RID: 394
	[CallbackIdentity(1315)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x04000673 RID: 1651
		public const int k_iCallback = 1315;

		// Token: 0x04000674 RID: 1652
		public EResult m_eResult;

		// Token: 0x04000675 RID: 1653
		public PublishedFileId_t m_nPublishedFileId;
	}
}
