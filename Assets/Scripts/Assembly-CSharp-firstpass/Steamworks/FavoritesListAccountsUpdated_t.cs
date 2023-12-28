using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016C RID: 364
	[CallbackIdentity(516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x04000626 RID: 1574
		public const int k_iCallback = 516;

		// Token: 0x04000627 RID: 1575
		public EResult m_eResult;
	}
}
