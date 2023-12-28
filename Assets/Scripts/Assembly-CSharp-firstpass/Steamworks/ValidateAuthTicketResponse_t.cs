using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B2 RID: 434
	[CallbackIdentity(143)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ValidateAuthTicketResponse_t
	{
		// Token: 0x0400071D RID: 1821
		public const int k_iCallback = 143;

		// Token: 0x0400071E RID: 1822
		public CSteamID m_SteamID;

		// Token: 0x0400071F RID: 1823
		public EAuthSessionResponse m_eAuthSessionResponse;

		// Token: 0x04000720 RID: 1824
		public CSteamID m_OwnerSteamID;
	}
}
