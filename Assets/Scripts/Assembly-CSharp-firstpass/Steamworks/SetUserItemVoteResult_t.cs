using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A5 RID: 421
	[CallbackIdentity(3408)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetUserItemVoteResult_t
	{
		// Token: 0x040006F3 RID: 1779
		public const int k_iCallback = 3408;

		// Token: 0x040006F4 RID: 1780
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006F5 RID: 1781
		public EResult m_eResult;

		// Token: 0x040006F6 RID: 1782
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteUp;
	}
}
