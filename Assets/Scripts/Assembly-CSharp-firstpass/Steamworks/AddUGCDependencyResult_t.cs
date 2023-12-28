using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A9 RID: 425
	[CallbackIdentity(3412)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddUGCDependencyResult_t
	{
		// Token: 0x04000701 RID: 1793
		public const int k_iCallback = 3412;

		// Token: 0x04000702 RID: 1794
		public EResult m_eResult;

		// Token: 0x04000703 RID: 1795
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000704 RID: 1796
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
