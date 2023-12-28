using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000120 RID: 288
	[CallbackIdentity(1008)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RegisterActivationCodeResponse_t
	{
		// Token: 0x040004F9 RID: 1273
		public const int k_iCallback = 1008;

		// Token: 0x040004FA RID: 1274
		public ERegisterActivationCodeResult m_eResult;

		// Token: 0x040004FB RID: 1275
		public uint m_unPackageRegistered;
	}
}
