using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019A RID: 410
	[CallbackIdentity(1331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileWriteAsyncComplete_t
	{
		// Token: 0x040006CD RID: 1741
		public const int k_iCallback = 1331;

		// Token: 0x040006CE RID: 1742
		public EResult m_eResult;
	}
}
