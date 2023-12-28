using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000138 RID: 312
	[CallbackIdentity(201)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientApprove_t
	{
		// Token: 0x04000549 RID: 1353
		public const int k_iCallback = 201;

		// Token: 0x0400054A RID: 1354
		public CSteamID m_SteamID;

		// Token: 0x0400054B RID: 1355
		public CSteamID m_OwnerSteamID;
	}
}
