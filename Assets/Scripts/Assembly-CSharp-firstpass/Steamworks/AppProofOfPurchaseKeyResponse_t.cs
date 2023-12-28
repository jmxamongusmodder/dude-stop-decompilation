using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000122 RID: 290
	[CallbackIdentity(1021)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x040004FD RID: 1277
		public const int k_iCallback = 1021;

		// Token: 0x040004FE RID: 1278
		public EResult m_eResult;

		// Token: 0x040004FF RID: 1279
		public uint m_nAppID;

		// Token: 0x04000500 RID: 1280
		public uint m_cchKeyLength;

		// Token: 0x04000501 RID: 1281
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		public string m_rgchKey;
	}
}
