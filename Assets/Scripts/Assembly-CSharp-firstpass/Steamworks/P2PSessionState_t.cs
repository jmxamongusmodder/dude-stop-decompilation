using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000221 RID: 545
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionState_t
	{
		// Token: 0x04000C00 RID: 3072
		public byte m_bConnectionActive;

		// Token: 0x04000C01 RID: 3073
		public byte m_bConnecting;

		// Token: 0x04000C02 RID: 3074
		public byte m_eP2PSessionError;

		// Token: 0x04000C03 RID: 3075
		public byte m_bUsingRelay;

		// Token: 0x04000C04 RID: 3076
		public int m_nBytesQueuedForSend;

		// Token: 0x04000C05 RID: 3077
		public int m_nPacketsQueuedForSend;

		// Token: 0x04000C06 RID: 3078
		public uint m_nRemoteIP;

		// Token: 0x04000C07 RID: 3079
		public ushort m_nRemotePort;
	}
}
