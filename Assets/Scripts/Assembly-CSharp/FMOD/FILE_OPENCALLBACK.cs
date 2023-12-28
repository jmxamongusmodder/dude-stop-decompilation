using System;

namespace FMOD
{
	// Token: 0x02000046 RID: 70
	// (Invoke) Token: 0x060000BE RID: 190
	public delegate RESULT FILE_OPENCALLBACK(StringWrapper name, ref uint filesize, ref IntPtr handle, IntPtr userdata);
}
