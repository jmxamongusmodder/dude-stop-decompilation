using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017A RID: 378
	[CallbackIdentity(4012)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x04000639 RID: 1593
		public const int k_iCallback = 4012;

		// Token: 0x0400063A RID: 1594
		public int nID;
	}
}
