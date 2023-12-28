using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C3 RID: 451
	[CallbackIdentity(1112)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x04000759 RID: 1881
		public const int k_iCallback = 1112;

		// Token: 0x0400075A RID: 1882
		public ulong m_nGameID;

		// Token: 0x0400075B RID: 1883
		public EResult m_eResult;
	}
}
