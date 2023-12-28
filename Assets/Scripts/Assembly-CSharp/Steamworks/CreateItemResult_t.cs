using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A0 RID: 416
	[CallbackIdentity(3403)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateItemResult_t
	{
		// Token: 0x040006E1 RID: 1761
		public const int k_iCallback = 3403;

		// Token: 0x040006E2 RID: 1762
		public EResult m_eResult;

		// Token: 0x040006E3 RID: 1763
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006E4 RID: 1764
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
