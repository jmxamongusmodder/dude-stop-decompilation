using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012F RID: 303
	[CallbackIdentity(341)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadClanActivityCountsResult_t
	{
		// Token: 0x0400052D RID: 1325
		public const int k_iCallback = 341;

		// Token: 0x0400052E RID: 1326
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;
	}
}
