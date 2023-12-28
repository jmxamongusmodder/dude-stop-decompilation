using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015F RID: 351
	[CallbackIdentity(4701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryFullUpdate_t
	{
		// Token: 0x040005F1 RID: 1521
		public const int k_iCallback = 4701;

		// Token: 0x040005F2 RID: 1522
		public SteamInventoryResult_t m_handle;
	}
}
