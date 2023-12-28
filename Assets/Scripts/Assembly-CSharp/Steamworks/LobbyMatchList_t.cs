using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000169 RID: 361
	[CallbackIdentity(510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyMatchList_t
	{
		// Token: 0x0400061D RID: 1565
		public const int k_iCallback = 510;

		// Token: 0x0400061E RID: 1566
		public uint m_nLobbiesMatching;
	}
}
