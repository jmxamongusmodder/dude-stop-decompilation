using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A8 RID: 424
	[CallbackIdentity(3411)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x040006FF RID: 1791
		public const int k_iCallback = 3411;

		// Token: 0x04000700 RID: 1792
		public EResult m_eResult;
	}
}
