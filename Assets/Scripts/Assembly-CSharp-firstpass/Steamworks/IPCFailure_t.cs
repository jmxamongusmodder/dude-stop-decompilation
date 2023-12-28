using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B0 RID: 432
	[CallbackIdentity(117)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCFailure_t
	{
		// Token: 0x0400071A RID: 1818
		public const int k_iCallback = 117;

		// Token: 0x0400071B RID: 1819
		public byte m_eFailureType;
	}
}
