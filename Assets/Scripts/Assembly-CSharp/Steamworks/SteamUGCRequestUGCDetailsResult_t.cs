using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019F RID: 415
	[CallbackIdentity(3402)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x040006DE RID: 1758
		public const int k_iCallback = 3402;

		// Token: 0x040006DF RID: 1759
		public SteamUGCDetails_t m_details;

		// Token: 0x040006E0 RID: 1760
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
