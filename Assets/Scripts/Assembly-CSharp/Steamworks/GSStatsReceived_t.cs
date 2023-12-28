using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000142 RID: 322
	[CallbackIdentity(1800)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsReceived_t
	{
		// Token: 0x04000573 RID: 1395
		public const int k_iCallback = 1800;

		// Token: 0x04000574 RID: 1396
		public EResult m_eResult;

		// Token: 0x04000575 RID: 1397
		public CSteamID m_steamIDUser;
	}
}
