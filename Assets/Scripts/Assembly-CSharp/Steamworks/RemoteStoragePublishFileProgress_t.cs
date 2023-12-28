using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000198 RID: 408
	[CallbackIdentity(1329)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileProgress_t
	{
		// Token: 0x040006C6 RID: 1734
		public const int k_iCallback = 1329;

		// Token: 0x040006C7 RID: 1735
		public double m_dPercentFile;

		// Token: 0x040006C8 RID: 1736
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPreview;
	}
}
