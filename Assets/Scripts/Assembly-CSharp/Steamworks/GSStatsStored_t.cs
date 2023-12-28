using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000143 RID: 323
	[CallbackIdentity(1801)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsStored_t
	{
		// Token: 0x04000576 RID: 1398
		public const int k_iCallback = 1801;

		// Token: 0x04000577 RID: 1399
		public EResult m_eResult;

		// Token: 0x04000578 RID: 1400
		public CSteamID m_steamIDUser;
	}
}
