using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200005E RID: 94
	public struct Debug
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00004F68 File Offset: 0x00003368
		public static RESULT Initialize(DEBUG_FLAGS flags, DEBUG_MODE mode, DEBUG_CALLBACK callback, string filename)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = Debug.FMOD5_Debug_Initialize(flags, mode, callback, freeHelper.byteFromStringUTF8(filename));
			}
			return result;
		}

		// Token: 0x06000107 RID: 263
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Debug_Initialize(DEBUG_FLAGS flags, DEBUG_MODE mode, DEBUG_CALLBACK callback, byte[] filename);
	}
}
