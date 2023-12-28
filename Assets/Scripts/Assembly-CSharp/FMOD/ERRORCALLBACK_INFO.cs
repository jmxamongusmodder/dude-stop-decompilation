using System;

namespace FMOD
{
	// Token: 0x0200003D RID: 61
	public struct ERRORCALLBACK_INFO
	{
		// Token: 0x040001ED RID: 493
		public RESULT result;

		// Token: 0x040001EE RID: 494
		public ERRORCALLBACK_INSTANCETYPE instancetype;

		// Token: 0x040001EF RID: 495
		public IntPtr instance;

		// Token: 0x040001F0 RID: 496
		public StringWrapper functionname;

		// Token: 0x040001F1 RID: 497
		public StringWrapper functionparams;
	}
}
