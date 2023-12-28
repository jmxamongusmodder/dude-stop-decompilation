using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AB RID: 427
	[CallbackIdentity(2501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUnifiedMessagesSendMethodResult_t
	{
		// Token: 0x04000709 RID: 1801
		public const int k_iCallback = 2501;

		// Token: 0x0400070A RID: 1802
		public ClientUnifiedMessageHandle m_hHandle;

		// Token: 0x0400070B RID: 1803
		public ulong m_unContext;

		// Token: 0x0400070C RID: 1804
		public EResult m_eResult;

		// Token: 0x0400070D RID: 1805
		public uint m_unResponseSize;
	}
}
