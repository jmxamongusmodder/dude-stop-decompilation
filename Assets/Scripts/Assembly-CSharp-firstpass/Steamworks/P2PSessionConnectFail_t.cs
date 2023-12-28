using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017E RID: 382
	[CallbackIdentity(1203)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct P2PSessionConnectFail_t
	{
		// Token: 0x04000641 RID: 1601
		public const int k_iCallback = 1203;

		// Token: 0x04000642 RID: 1602
		public CSteamID m_steamIDRemote;

		// Token: 0x04000643 RID: 1603
		public byte m_eP2PSessionError;
	}
}
