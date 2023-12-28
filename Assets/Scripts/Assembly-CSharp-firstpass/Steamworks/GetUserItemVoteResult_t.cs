using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A6 RID: 422
	[CallbackIdentity(3409)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetUserItemVoteResult_t
	{
		// Token: 0x040006F7 RID: 1783
		public const int k_iCallback = 3409;

		// Token: 0x040006F8 RID: 1784
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006F9 RID: 1785
		public EResult m_eResult;

		// Token: 0x040006FA RID: 1786
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedUp;

		// Token: 0x040006FB RID: 1787
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedDown;

		// Token: 0x040006FC RID: 1788
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteSkipped;
	}
}
