using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013F RID: 319
	[CallbackIdentity(209)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSReputation_t
	{
		// Token: 0x04000563 RID: 1379
		public const int k_iCallback = 209;

		// Token: 0x04000564 RID: 1380
		public EResult m_eResult;

		// Token: 0x04000565 RID: 1381
		public uint m_unReputationScore;

		// Token: 0x04000566 RID: 1382
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x04000567 RID: 1383
		public uint m_unBannedIP;

		// Token: 0x04000568 RID: 1384
		public ushort m_usBannedPort;

		// Token: 0x04000569 RID: 1385
		public ulong m_ulBannedGameID;

		// Token: 0x0400056A RID: 1386
		public uint m_unBanExpires;
	}
}
