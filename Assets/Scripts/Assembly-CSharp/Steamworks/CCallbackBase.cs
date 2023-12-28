using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022C RID: 556
	[StructLayout(LayoutKind.Sequential)]
	internal class CCallbackBase
	{
		// Token: 0x04000C3F RID: 3135
		public const byte k_ECallbackFlagsRegistered = 1;

		// Token: 0x04000C40 RID: 3136
		public const byte k_ECallbackFlagsGameServer = 2;

		// Token: 0x04000C41 RID: 3137
		public IntPtr m_vfptr;

		// Token: 0x04000C42 RID: 3138
		public byte m_nCallbackFlags;

		// Token: 0x04000C43 RID: 3139
		public int m_iCallback;
	}
}
