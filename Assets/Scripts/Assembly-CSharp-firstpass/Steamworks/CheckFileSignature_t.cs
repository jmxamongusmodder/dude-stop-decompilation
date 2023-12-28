using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C8 RID: 456
	[CallbackIdentity(705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature_t
	{
		// Token: 0x04000764 RID: 1892
		public const int k_iCallback = 705;

		// Token: 0x04000765 RID: 1893
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
