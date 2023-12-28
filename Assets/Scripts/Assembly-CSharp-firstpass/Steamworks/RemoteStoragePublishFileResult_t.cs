using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000185 RID: 389
	[CallbackIdentity(1309)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x0400065E RID: 1630
		public const int k_iCallback = 1309;

		// Token: 0x0400065F RID: 1631
		public EResult m_eResult;

		// Token: 0x04000660 RID: 1632
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000661 RID: 1633
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
