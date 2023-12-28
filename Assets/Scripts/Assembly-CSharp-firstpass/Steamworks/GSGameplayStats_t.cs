using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013D RID: 317
	[CallbackIdentity(207)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSGameplayStats_t
	{
		// Token: 0x04000559 RID: 1369
		public const int k_iCallback = 207;

		// Token: 0x0400055A RID: 1370
		public EResult m_eResult;

		// Token: 0x0400055B RID: 1371
		public int m_nRank;

		// Token: 0x0400055C RID: 1372
		public uint m_unTotalConnects;

		// Token: 0x0400055D RID: 1373
		public uint m_unTotalMinutesPlayed;
	}
}
