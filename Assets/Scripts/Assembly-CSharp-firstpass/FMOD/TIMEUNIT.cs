using System;

namespace FMOD
{
	// Token: 0x02000055 RID: 85
	[Flags]
	public enum TIMEUNIT : uint
	{
		// Token: 0x0400022C RID: 556
		MS = 1U,
		// Token: 0x0400022D RID: 557
		PCM = 2U,
		// Token: 0x0400022E RID: 558
		PCMBYTES = 4U,
		// Token: 0x0400022F RID: 559
		RAWBYTES = 8U,
		// Token: 0x04000230 RID: 560
		PCMFRACTION = 16U,
		// Token: 0x04000231 RID: 561
		MODORDER = 256U,
		// Token: 0x04000232 RID: 562
		MODROW = 512U,
		// Token: 0x04000233 RID: 563
		MODPATTERN = 1024U
	}
}
