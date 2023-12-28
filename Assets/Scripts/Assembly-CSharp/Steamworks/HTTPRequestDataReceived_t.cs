using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015D RID: 349
	[CallbackIdentity(2103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestDataReceived_t
	{
		// Token: 0x040005E9 RID: 1513
		public const int k_iCallback = 2103;

		// Token: 0x040005EA RID: 1514
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005EB RID: 1515
		public ulong m_ulContextValue;

		// Token: 0x040005EC RID: 1516
		public uint m_cOffset;

		// Token: 0x040005ED RID: 1517
		public uint m_cBytesReceived;
	}
}
