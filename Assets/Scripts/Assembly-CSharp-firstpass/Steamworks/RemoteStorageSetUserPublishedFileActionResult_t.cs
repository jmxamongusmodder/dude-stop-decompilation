using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000196 RID: 406
	[CallbackIdentity(1327)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x040006BB RID: 1723
		public const int k_iCallback = 1327;

		// Token: 0x040006BC RID: 1724
		public EResult m_eResult;

		// Token: 0x040006BD RID: 1725
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006BE RID: 1726
		public EWorkshopFileAction m_eAction;
	}
}
