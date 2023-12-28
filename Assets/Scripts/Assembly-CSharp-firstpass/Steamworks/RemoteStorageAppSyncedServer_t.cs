using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000181 RID: 385
	[CallbackIdentity(1302)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncedServer_t
	{
		// Token: 0x0400064D RID: 1613
		public const int k_iCallback = 1302;

		// Token: 0x0400064E RID: 1614
		public AppId_t m_nAppID;

		// Token: 0x0400064F RID: 1615
		public EResult m_eResult;

		// Token: 0x04000650 RID: 1616
		public int m_unNumUploads;
	}
}
