using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018E RID: 398
	[CallbackIdentity(1319)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x04000697 RID: 1687
		public const int k_iCallback = 1319;

		// Token: 0x04000698 RID: 1688
		public EResult m_eResult;

		// Token: 0x04000699 RID: 1689
		public int m_nResultsReturned;

		// Token: 0x0400069A RID: 1690
		public int m_nTotalResultCount;

		// Token: 0x0400069B RID: 1691
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x0400069C RID: 1692
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public float[] m_rgScore;

		// Token: 0x0400069D RID: 1693
		public AppId_t m_nAppId;

		// Token: 0x0400069E RID: 1694
		public uint m_unStartIndex;
	}
}
