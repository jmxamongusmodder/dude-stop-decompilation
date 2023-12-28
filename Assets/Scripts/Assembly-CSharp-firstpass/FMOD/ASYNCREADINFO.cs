using System;

namespace FMOD
{
	// Token: 0x02000029 RID: 41
	public struct ASYNCREADINFO
	{
		// Token: 0x040000F9 RID: 249
		public IntPtr handle;

		// Token: 0x040000FA RID: 250
		public uint offset;

		// Token: 0x040000FB RID: 251
		public uint sizebytes;

		// Token: 0x040000FC RID: 252
		public int priority;

		// Token: 0x040000FD RID: 253
		public IntPtr userdata;

		// Token: 0x040000FE RID: 254
		public IntPtr buffer;

		// Token: 0x040000FF RID: 255
		public uint bytesread;

		// Token: 0x04000100 RID: 256
		public ASYNCREADINFO_DONE_CALLBACK done;
	}
}
