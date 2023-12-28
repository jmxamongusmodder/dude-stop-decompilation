using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C6 RID: 454
	[CallbackIdentity(703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICallCompleted_t
	{
		// Token: 0x0400075F RID: 1887
		public const int k_iCallback = 703;

		// Token: 0x04000760 RID: 1888
		public SteamAPICall_t m_hAsyncCall;

		// Token: 0x04000761 RID: 1889
		public int m_iCallback;

		// Token: 0x04000762 RID: 1890
		public uint m_cubParam;
	}
}
