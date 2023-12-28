using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000125 RID: 293
	[CallbackIdentity(331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameOverlayActivated_t
	{
		// Token: 0x0400050A RID: 1290
		public const int k_iCallback = 331;

		// Token: 0x0400050B RID: 1291
		public byte m_bActive;
	}
}
