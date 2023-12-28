using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B5 RID: 437
	[CallbackIdentity(163)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x04000727 RID: 1831
		public const int k_iCallback = 163;

		// Token: 0x04000728 RID: 1832
		public HAuthTicket m_hAuthTicket;

		// Token: 0x04000729 RID: 1833
		public EResult m_eResult;
	}
}
