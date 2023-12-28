using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001CB RID: 459
	[CallbackIdentity(4605)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct BroadcastUploadStop_t
	{
		// Token: 0x0400076A RID: 1898
		public const int k_iCallback = 4605;

		// Token: 0x0400076B RID: 1899
		public EBroadcastUploadResult m_eResult;
	}
}
