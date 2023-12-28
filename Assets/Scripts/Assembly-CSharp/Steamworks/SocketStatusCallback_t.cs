using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017F RID: 383
	[CallbackIdentity(1201)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SocketStatusCallback_t
	{
		// Token: 0x04000644 RID: 1604
		public const int k_iCallback = 1201;

		// Token: 0x04000645 RID: 1605
		public SNetSocket_t m_hSocket;

		// Token: 0x04000646 RID: 1606
		public SNetListenSocket_t m_hListenSocket;

		// Token: 0x04000647 RID: 1607
		public CSteamID m_steamIDRemote;

		// Token: 0x04000648 RID: 1608
		public int m_eSNetSocketState;
	}
}
