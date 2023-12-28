using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B7 RID: 439
	[CallbackIdentity(165)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x0400072C RID: 1836
		public const int k_iCallback = 165;

		// Token: 0x0400072D RID: 1837
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string m_szURL;
	}
}
