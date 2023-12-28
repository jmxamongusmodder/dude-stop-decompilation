using System;

namespace FMOD
{
	// Token: 0x0200006C RID: 108
	public struct DSP_BUFFER_ARRAY
	{
		// Token: 0x04000293 RID: 659
		public int numbuffers;

		// Token: 0x04000294 RID: 660
		public int[] buffernumchannels;

		// Token: 0x04000295 RID: 661
		public CHANNELMASK[] bufferchannelmask;

		// Token: 0x04000296 RID: 662
		public IntPtr[] buffers;

		// Token: 0x04000297 RID: 663
		public SPEAKERMODE speakermode;
	}
}
