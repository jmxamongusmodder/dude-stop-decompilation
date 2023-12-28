using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017C RID: 380
	[CallbackIdentity(4114)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x0400063D RID: 1597
		public const int k_iCallback = 4114;

		// Token: 0x0400063E RID: 1598
		public int m_nPlayingRepeatStatus;
	}
}
