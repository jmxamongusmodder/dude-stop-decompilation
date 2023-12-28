using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000161 RID: 353
	[CallbackIdentity(4703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x040005F4 RID: 1524
		public const int k_iCallback = 4703;

		// Token: 0x040005F5 RID: 1525
		public EResult m_result;

		// Token: 0x040005F6 RID: 1526
		public CSteamID m_steamID;

		// Token: 0x040005F7 RID: 1527
		public int m_numEligiblePromoItemDefs;

		// Token: 0x040005F8 RID: 1528
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
