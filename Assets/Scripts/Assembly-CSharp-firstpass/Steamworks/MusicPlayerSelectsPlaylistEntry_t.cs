using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017B RID: 379
	[CallbackIdentity(4013)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x0400063B RID: 1595
		public const int k_iCallback = 4013;

		// Token: 0x0400063C RID: 1596
		public int nID;
	}
}
