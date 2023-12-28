using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000195 RID: 405
	[CallbackIdentity(1326)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x040006B6 RID: 1718
		public const int k_iCallback = 1326;

		// Token: 0x040006B7 RID: 1719
		public EResult m_eResult;

		// Token: 0x040006B8 RID: 1720
		public int m_nResultsReturned;

		// Token: 0x040006B9 RID: 1721
		public int m_nTotalResultCount;

		// Token: 0x040006BA RID: 1722
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
