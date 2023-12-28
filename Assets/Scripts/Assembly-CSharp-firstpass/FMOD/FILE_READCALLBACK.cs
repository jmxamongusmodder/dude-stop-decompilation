using System;

namespace FMOD
{
	// Token: 0x02000048 RID: 72
	// (Invoke) Token: 0x060000C6 RID: 198
	public delegate RESULT FILE_READCALLBACK(IntPtr handle, IntPtr buffer, uint sizebytes, ref uint bytesread, IntPtr userdata);
}
