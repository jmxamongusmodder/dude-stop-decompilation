using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000124 RID: 292
	[CallbackIdentity(304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PersonaStateChange_t
	{
		// Token: 0x04000507 RID: 1287
		public const int k_iCallback = 304;

		// Token: 0x04000508 RID: 1288
		public ulong m_ulSteamID;

		// Token: 0x04000509 RID: 1289
		public EPersonaChange m_nChangeFlags;
	}
}
