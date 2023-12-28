using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AA RID: 426
	[CallbackIdentity(3413)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveUGCDependencyResult_t
	{
		// Token: 0x04000705 RID: 1797
		public const int k_iCallback = 3413;

		// Token: 0x04000706 RID: 1798
		public EResult m_eResult;

		// Token: 0x04000707 RID: 1799
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000708 RID: 1800
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
