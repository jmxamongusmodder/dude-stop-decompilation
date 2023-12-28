using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B4 RID: 436
	[CallbackIdentity(154)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EncryptedAppTicketResponse_t
	{
		// Token: 0x04000725 RID: 1829
		public const int k_iCallback = 154;

		// Token: 0x04000726 RID: 1830
		public EResult m_eResult;
	}
}
