using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000135 RID: 309
	[CallbackIdentity(347)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetPersonaNameResponse_t
	{
		// Token: 0x04000542 RID: 1346
		public const int k_iCallback = 347;

		// Token: 0x04000543 RID: 1347
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;

		// Token: 0x04000544 RID: 1348
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocalSuccess;

		// Token: 0x04000545 RID: 1349
		public EResult m_result;
	}
}
