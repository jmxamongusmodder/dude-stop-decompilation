using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000178 RID: 376
	[CallbackIdentity(4110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsLooped_t
	{
		// Token: 0x04000635 RID: 1589
		public const int k_iCallback = 4110;

		// Token: 0x04000636 RID: 1590
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLooped;
	}
}
