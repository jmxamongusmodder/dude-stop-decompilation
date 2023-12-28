using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A4 RID: 420
	[CallbackIdentity(3407)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x040006EF RID: 1775
		public const int k_iCallback = 3407;

		// Token: 0x040006F0 RID: 1776
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006F1 RID: 1777
		public EResult m_eResult;

		// Token: 0x040006F2 RID: 1778
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bWasAddRequest;
	}
}
