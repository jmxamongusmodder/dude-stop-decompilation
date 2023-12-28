using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BE RID: 446
	[CallbackIdentity(1107)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NumberOfCurrentPlayers_t
	{
		// Token: 0x04000749 RID: 1865
		public const int k_iCallback = 1107;

		// Token: 0x0400074A RID: 1866
		public byte m_bSuccess;

		// Token: 0x0400074B RID: 1867
		public int m_cPlayers;
	}
}
