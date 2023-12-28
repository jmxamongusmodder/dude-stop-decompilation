using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000193 RID: 403
	[CallbackIdentity(1324)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x040006AF RID: 1711
		public const int k_iCallback = 1324;

		// Token: 0x040006B0 RID: 1712
		public EResult m_eResult;

		// Token: 0x040006B1 RID: 1713
		public PublishedFileId_t m_nPublishedFileId;
	}
}
