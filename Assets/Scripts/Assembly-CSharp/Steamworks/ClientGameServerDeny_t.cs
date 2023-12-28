using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AF RID: 431
	[CallbackIdentity(113)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClientGameServerDeny_t
	{
		// Token: 0x04000714 RID: 1812
		public const int k_iCallback = 113;

		// Token: 0x04000715 RID: 1813
		public uint m_uAppID;

		// Token: 0x04000716 RID: 1814
		public uint m_unGameServerIP;

		// Token: 0x04000717 RID: 1815
		public ushort m_usGameServerPort;

		// Token: 0x04000718 RID: 1816
		public ushort m_bSecure;

		// Token: 0x04000719 RID: 1817
		public uint m_uReason;
	}
}
