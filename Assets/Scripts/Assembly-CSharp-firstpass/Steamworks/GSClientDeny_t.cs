using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000139 RID: 313
	[CallbackIdentity(202)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientDeny_t
	{
		// Token: 0x0400054C RID: 1356
		public const int k_iCallback = 202;

		// Token: 0x0400054D RID: 1357
		public CSteamID m_SteamID;

		// Token: 0x0400054E RID: 1358
		public EDenyReason m_eDenyReason;

		// Token: 0x0400054F RID: 1359
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchOptionalText;
	}
}
