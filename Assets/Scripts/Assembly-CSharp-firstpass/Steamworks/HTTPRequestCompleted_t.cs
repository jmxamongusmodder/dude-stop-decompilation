using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015B RID: 347
	[CallbackIdentity(2101)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestCompleted_t
	{
		// Token: 0x040005E0 RID: 1504
		public const int k_iCallback = 2101;

		// Token: 0x040005E1 RID: 1505
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005E2 RID: 1506
		public ulong m_ulContextValue;

		// Token: 0x040005E3 RID: 1507
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bRequestSuccessful;

		// Token: 0x040005E4 RID: 1508
		public EHTTPStatusCode m_eStatusCode;

		// Token: 0x040005E5 RID: 1509
		public uint m_unBodySize;
	}
}
