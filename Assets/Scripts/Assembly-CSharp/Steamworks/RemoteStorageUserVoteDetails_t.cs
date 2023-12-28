using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000194 RID: 404
	[CallbackIdentity(1325)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x040006B2 RID: 1714
		public const int k_iCallback = 1325;

		// Token: 0x040006B3 RID: 1715
		public EResult m_eResult;

		// Token: 0x040006B4 RID: 1716
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006B5 RID: 1717
		public EWorkshopVote m_eVote;
	}
}
