using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A7 RID: 423
	[CallbackIdentity(3410)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x040006FD RID: 1789
		public const int k_iCallback = 3410;

		// Token: 0x040006FE RID: 1790
		public EResult m_eResult;
	}
}
