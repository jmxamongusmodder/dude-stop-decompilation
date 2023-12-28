using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000180 RID: 384
	[CallbackIdentity(1301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncedClient_t
	{
		// Token: 0x04000649 RID: 1609
		public const int k_iCallback = 1301;

		// Token: 0x0400064A RID: 1610
		public AppId_t m_nAppID;

		// Token: 0x0400064B RID: 1611
		public EResult m_eResult;

		// Token: 0x0400064C RID: 1612
		public int m_unNumDownloads;
	}
}
