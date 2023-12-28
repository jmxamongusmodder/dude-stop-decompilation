using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000224 RID: 548
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CallbackMsg_t
	{
		// Token: 0x04000C24 RID: 3108
		public int m_hSteamUser;

		// Token: 0x04000C25 RID: 3109
		public int m_iCallback;

		// Token: 0x04000C26 RID: 3110
		public IntPtr m_pubParam;

		// Token: 0x04000C27 RID: 3111
		public int m_cubParam;
	}
}
