using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001CC RID: 460
	[CallbackIdentity(4611)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x0400076C RID: 1900
		public const int k_iCallback = 4611;

		// Token: 0x0400076D RID: 1901
		public EResult m_eResult;

		// Token: 0x0400076E RID: 1902
		public AppId_t m_unVideoAppID;

		// Token: 0x0400076F RID: 1903
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL;
	}
}
