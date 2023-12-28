using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200011F RID: 287
	[CallbackIdentity(1005)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DlcInstalled_t
	{
		// Token: 0x040004F7 RID: 1271
		public const int k_iCallback = 1005;

		// Token: 0x040004F8 RID: 1272
		public AppId_t m_nAppID;
	}
}
