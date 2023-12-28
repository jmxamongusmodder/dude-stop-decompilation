using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000177 RID: 375
	[CallbackIdentity(4109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x04000633 RID: 1587
		public const int k_iCallback = 4109;

		// Token: 0x04000634 RID: 1588
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bShuffled;
	}
}
