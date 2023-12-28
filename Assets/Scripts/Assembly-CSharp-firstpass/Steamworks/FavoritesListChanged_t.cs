using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000162 RID: 354
	[CallbackIdentity(502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListChanged_t
	{
		// Token: 0x040005F9 RID: 1529
		public const int k_iCallback = 502;

		// Token: 0x040005FA RID: 1530
		public uint m_nIP;

		// Token: 0x040005FB RID: 1531
		public uint m_nQueryPort;

		// Token: 0x040005FC RID: 1532
		public uint m_nConnPort;

		// Token: 0x040005FD RID: 1533
		public uint m_nAppID;

		// Token: 0x040005FE RID: 1534
		public uint m_nFlags;

		// Token: 0x040005FF RID: 1535
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAdd;

		// Token: 0x04000600 RID: 1536
		public AccountID_t m_unAccountId;
	}
}
