using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AE RID: 430
	[CallbackIdentity(103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersDisconnected_t
	{
		// Token: 0x04000712 RID: 1810
		public const int k_iCallback = 103;

		// Token: 0x04000713 RID: 1811
		public EResult m_eResult;
	}
}
