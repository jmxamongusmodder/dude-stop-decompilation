using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000197 RID: 407
	[CallbackIdentity(1328)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x040006BF RID: 1727
		public const int k_iCallback = 1328;

		// Token: 0x040006C0 RID: 1728
		public EResult m_eResult;

		// Token: 0x040006C1 RID: 1729
		public EWorkshopFileAction m_eAction;

		// Token: 0x040006C2 RID: 1730
		public int m_nResultsReturned;

		// Token: 0x040006C3 RID: 1731
		public int m_nTotalResultCount;

		// Token: 0x040006C4 RID: 1732
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x040006C5 RID: 1733
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeUpdated;
	}
}
