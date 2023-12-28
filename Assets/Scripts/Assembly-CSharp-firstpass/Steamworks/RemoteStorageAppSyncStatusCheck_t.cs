using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000183 RID: 387
	[CallbackIdentity(1305)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncStatusCheck_t
	{
		// Token: 0x04000657 RID: 1623
		public const int k_iCallback = 1305;

		// Token: 0x04000658 RID: 1624
		public AppId_t m_nAppID;

		// Token: 0x04000659 RID: 1625
		public EResult m_eResult;
	}
}
