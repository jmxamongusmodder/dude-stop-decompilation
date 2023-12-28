using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000123 RID: 291
	[CallbackIdentity(1023)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FileDetailsResult_t
	{
		// Token: 0x04000502 RID: 1282
		public const int k_iCallback = 1023;

		// Token: 0x04000503 RID: 1283
		public EResult m_eResult;

		// Token: 0x04000504 RID: 1284
		public ulong m_ulFileSize;

		// Token: 0x04000505 RID: 1285
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] m_FileSHA;

		// Token: 0x04000506 RID: 1286
		public uint m_unFlags;
	}
}
