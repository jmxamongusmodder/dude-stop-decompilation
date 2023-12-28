using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200005C RID: 92
	public struct Factory
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004F36 File Offset: 0x00003336
		public static RESULT System_Create(out System system)
		{
			return Factory.FMOD5_System_Create(out system.handle);
		}

		// Token: 0x06000100 RID: 256
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_System_Create(out IntPtr system);
	}
}
