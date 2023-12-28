﻿using System;

namespace FMOD
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum MEMORY_TYPE : uint
	{
		// Token: 0x0400012C RID: 300
		NORMAL = 0U,
		// Token: 0x0400012D RID: 301
		STREAM_FILE = 1U,
		// Token: 0x0400012E RID: 302
		STREAM_DECODE = 2U,
		// Token: 0x0400012F RID: 303
		SAMPLEDATA = 4U,
		// Token: 0x04000130 RID: 304
		DSP_BUFFER = 8U,
		// Token: 0x04000131 RID: 305
		PLUGIN = 16U,
		// Token: 0x04000132 RID: 306
		XBOX360_PHYSICAL = 1048576U,
		// Token: 0x04000133 RID: 307
		PERSISTENT = 2097152U,
		// Token: 0x04000134 RID: 308
		SECONDARY = 4194304U,
		// Token: 0x04000135 RID: 309
		ALL = 4294967295U
	}
}