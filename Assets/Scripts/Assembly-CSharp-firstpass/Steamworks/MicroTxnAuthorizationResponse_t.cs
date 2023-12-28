using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B3 RID: 435
	[CallbackIdentity(152)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x04000721 RID: 1825
		public const int k_iCallback = 152;

		// Token: 0x04000722 RID: 1826
		public uint m_unAppID;

		// Token: 0x04000723 RID: 1827
		public ulong m_ulOrderID;

		// Token: 0x04000724 RID: 1828
		public byte m_bAuthorized;
	}
}
