using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015E RID: 350
	[CallbackIdentity(4700)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryResultReady_t
	{
		// Token: 0x040005EE RID: 1518
		public const int k_iCallback = 4700;

		// Token: 0x040005EF RID: 1519
		public SteamInventoryResult_t m_handle;

		// Token: 0x040005F0 RID: 1520
		public EResult m_result;
	}
}
