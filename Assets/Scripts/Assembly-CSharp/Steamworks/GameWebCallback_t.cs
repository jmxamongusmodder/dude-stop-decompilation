using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B6 RID: 438
	[CallbackIdentity(164)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameWebCallback_t
	{
		// Token: 0x0400072A RID: 1834
		public const int k_iCallback = 164;

		// Token: 0x0400072B RID: 1835
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szURL;
	}
}
