using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018F RID: 399
	[CallbackIdentity(1320)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x0400069F RID: 1695
		public const int k_iCallback = 1320;

		// Token: 0x040006A0 RID: 1696
		public EResult m_eResult;

		// Token: 0x040006A1 RID: 1697
		public PublishedFileId_t m_unPublishedFileId;

		// Token: 0x040006A2 RID: 1698
		public int m_nVotesFor;

		// Token: 0x040006A3 RID: 1699
		public int m_nVotesAgainst;

		// Token: 0x040006A4 RID: 1700
		public int m_nReports;

		// Token: 0x040006A5 RID: 1701
		public float m_fScore;
	}
}
