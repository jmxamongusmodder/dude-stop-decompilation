using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000256 RID: 598
	public static class Packsize
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0001048C File Offset: 0x0000E88C
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 32 && num2 == 616;
		}

		// Token: 0x04000C70 RID: 3184
		public const int value = 8;

		// Token: 0x02000257 RID: 599
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000C71 RID: 3185
			private uint m_u32;

			// Token: 0x04000C72 RID: 3186
			private ulong m_u64;

			// Token: 0x04000C73 RID: 3187
			private ushort m_u16;

			// Token: 0x04000C74 RID: 3188
			private double m_d;
		}
	}
}
