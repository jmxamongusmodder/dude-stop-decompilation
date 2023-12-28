using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013A RID: 314
	[CallbackIdentity(203)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientKick_t
	{
		// Token: 0x04000550 RID: 1360
		public const int k_iCallback = 203;

		// Token: 0x04000551 RID: 1361
		public CSteamID m_SteamID;

		// Token: 0x04000552 RID: 1362
		public EDenyReason m_eDenyReason;
	}
}
