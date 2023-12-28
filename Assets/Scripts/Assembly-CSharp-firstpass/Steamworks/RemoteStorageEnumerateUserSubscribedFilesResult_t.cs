using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000189 RID: 393
	[CallbackIdentity(1314)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		// Token: 0x0400066D RID: 1645
		public const int k_iCallback = 1314;

		// Token: 0x0400066E RID: 1646
		public EResult m_eResult;

		// Token: 0x0400066F RID: 1647
		public int m_nResultsReturned;

		// Token: 0x04000670 RID: 1648
		public int m_nTotalResultCount;

		// Token: 0x04000671 RID: 1649
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000672 RID: 1650
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeSubscribed;
	}
}
