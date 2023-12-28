using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000F7 RID: 247
	public struct Util
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00007648 File Offset: 0x00005A48
		public static RESULT ParseID(string idString, out Guid id)
		{
			RESULT result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = Util.FMOD_Studio_ParseID(freeHelper.byteFromStringUTF8(idString), out id);
			}
			return result;
		}

		// Token: 0x06000507 RID: 1287
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD_Studio_ParseID(byte[] idString, out Guid id);
	}
}
