using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001CD RID: 461
	[CallbackIdentity(4624)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetOPFSettingsResult_t
	{
		// Token: 0x04000770 RID: 1904
		public const int k_iCallback = 4624;

		// Token: 0x04000771 RID: 1905
		public EResult m_eResult;

		// Token: 0x04000772 RID: 1906
		public AppId_t m_unVideoAppID;
	}
}
