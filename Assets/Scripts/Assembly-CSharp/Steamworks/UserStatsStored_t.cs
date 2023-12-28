using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B9 RID: 441
	[CallbackIdentity(1102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsStored_t
	{
		// Token: 0x04000732 RID: 1842
		public const int k_iCallback = 1102;

		// Token: 0x04000733 RID: 1843
		public ulong m_nGameID;

		// Token: 0x04000734 RID: 1844
		public EResult m_eResult;
	}
}
