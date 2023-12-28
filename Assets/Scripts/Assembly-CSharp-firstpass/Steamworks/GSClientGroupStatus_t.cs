using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013E RID: 318
	[CallbackIdentity(208)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GSClientGroupStatus_t
	{
		// Token: 0x0400055E RID: 1374
		public const int k_iCallback = 208;

		// Token: 0x0400055F RID: 1375
		public CSteamID m_SteamIDUser;

		// Token: 0x04000560 RID: 1376
		public CSteamID m_SteamIDGroup;

		// Token: 0x04000561 RID: 1377
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bMember;

		// Token: 0x04000562 RID: 1378
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bOfficer;
	}
}
