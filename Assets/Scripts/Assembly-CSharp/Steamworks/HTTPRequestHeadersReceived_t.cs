using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015C RID: 348
	[CallbackIdentity(2102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x040005E6 RID: 1510
		public const int k_iCallback = 2102;

		// Token: 0x040005E7 RID: 1511
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005E8 RID: 1512
		public ulong m_ulContextValue;
	}
}
