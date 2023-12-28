using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000136 RID: 310
	[CallbackIdentity(1701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GCMessageAvailable_t
	{
		// Token: 0x04000546 RID: 1350
		public const int k_iCallback = 1701;

		// Token: 0x04000547 RID: 1351
		public uint m_nMessageSize;
	}
}
