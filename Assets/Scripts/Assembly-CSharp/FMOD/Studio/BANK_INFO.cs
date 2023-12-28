using System;

namespace FMOD.Studio
{
	// Token: 0x020000DE RID: 222
	public struct BANK_INFO
	{
		// Token: 0x04000476 RID: 1142
		public int size;

		// Token: 0x04000477 RID: 1143
		public IntPtr userdata;

		// Token: 0x04000478 RID: 1144
		public int userdatalength;

		// Token: 0x04000479 RID: 1145
		public FILE_OPENCALLBACK opencallback;

		// Token: 0x0400047A RID: 1146
		public FILE_CLOSECALLBACK closecallback;

		// Token: 0x0400047B RID: 1147
		public FILE_READCALLBACK readcallback;

		// Token: 0x0400047C RID: 1148
		public FILE_SEEKCALLBACK seekcallback;
	}
}
