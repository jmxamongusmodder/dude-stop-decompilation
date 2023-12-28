using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000184 RID: 388
	[CallbackIdentity(1307)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x0400065A RID: 1626
		public const int k_iCallback = 1307;

		// Token: 0x0400065B RID: 1627
		public EResult m_eResult;

		// Token: 0x0400065C RID: 1628
		public UGCHandle_t m_hFile;

		// Token: 0x0400065D RID: 1629
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchFilename;
	}
}
