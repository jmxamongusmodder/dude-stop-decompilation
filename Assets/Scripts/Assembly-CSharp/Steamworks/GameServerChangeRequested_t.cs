using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000126 RID: 294
	[CallbackIdentity(332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x0400050C RID: 1292
		public const int k_iCallback = 332;

		// Token: 0x0400050D RID: 1293
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchServer;

		// Token: 0x0400050E RID: 1294
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchPassword;
	}
}
