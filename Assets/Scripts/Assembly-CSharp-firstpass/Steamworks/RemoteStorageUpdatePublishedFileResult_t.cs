using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018B RID: 395
	[CallbackIdentity(1316)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x04000676 RID: 1654
		public const int k_iCallback = 1316;

		// Token: 0x04000677 RID: 1655
		public EResult m_eResult;

		// Token: 0x04000678 RID: 1656
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000679 RID: 1657
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
