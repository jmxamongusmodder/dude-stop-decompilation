using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000128 RID: 296
	[CallbackIdentity(334)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AvatarImageLoaded_t
	{
		// Token: 0x04000512 RID: 1298
		public const int k_iCallback = 334;

		// Token: 0x04000513 RID: 1299
		public CSteamID m_steamID;

		// Token: 0x04000514 RID: 1300
		public int m_iImage;

		// Token: 0x04000515 RID: 1301
		public int m_iWide;

		// Token: 0x04000516 RID: 1302
		public int m_iTall;
	}
}
