using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000220 RID: 544
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamItemDetails_t
	{
		// Token: 0x04000BFC RID: 3068
		public SteamItemInstanceID_t m_itemId;

		// Token: 0x04000BFD RID: 3069
		public SteamItemDef_t m_iDefinition;

		// Token: 0x04000BFE RID: 3070
		public ushort m_unQuantity;

		// Token: 0x04000BFF RID: 3071
		public ushort m_unFlags;
	}
}
