using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017D RID: 381
	[CallbackIdentity(1202)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionRequest_t
	{
		// Token: 0x0400063F RID: 1599
		public const int k_iCallback = 1202;

		// Token: 0x04000640 RID: 1600
		public CSteamID m_steamIDRemote;
	}
}
