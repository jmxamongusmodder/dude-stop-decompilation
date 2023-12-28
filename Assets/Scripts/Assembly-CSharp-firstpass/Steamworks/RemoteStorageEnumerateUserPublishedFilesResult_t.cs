using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000187 RID: 391
	[CallbackIdentity(1312)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x04000665 RID: 1637
		public const int k_iCallback = 1312;

		// Token: 0x04000666 RID: 1638
		public EResult m_eResult;

		// Token: 0x04000667 RID: 1639
		public int m_nResultsReturned;

		// Token: 0x04000668 RID: 1640
		public int m_nTotalResultCount;

		// Token: 0x04000669 RID: 1641
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
