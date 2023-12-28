﻿using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000134 RID: 308
	[CallbackIdentity(346)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsEnumerateFollowingList_t
	{
		// Token: 0x0400053D RID: 1341
		public const int k_iCallback = 346;

		// Token: 0x0400053E RID: 1342
		public EResult m_eResult;

		// Token: 0x0400053F RID: 1343
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public CSteamID[] m_rgSteamID;

		// Token: 0x04000540 RID: 1344
		public int m_nResultsReturned;

		// Token: 0x04000541 RID: 1345
		public int m_nTotalResultCount;
	}
}
