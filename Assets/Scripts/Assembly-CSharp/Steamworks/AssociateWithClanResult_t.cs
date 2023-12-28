using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000140 RID: 320
	[CallbackIdentity(210)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AssociateWithClanResult_t
	{
		// Token: 0x0400056B RID: 1387
		public const int k_iCallback = 210;

		// Token: 0x0400056C RID: 1388
		public EResult m_eResult;
	}
}
