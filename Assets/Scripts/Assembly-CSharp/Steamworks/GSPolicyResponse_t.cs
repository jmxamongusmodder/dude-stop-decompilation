using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013C RID: 316
	[CallbackIdentity(115)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSPolicyResponse_t
	{
		// Token: 0x04000557 RID: 1367
		public const int k_iCallback = 115;

		// Token: 0x04000558 RID: 1368
		public byte m_bSecure;
	}
}
