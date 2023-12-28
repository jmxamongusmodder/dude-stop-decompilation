using System;

namespace FMOD
{
	// Token: 0x020000A8 RID: 168
	public struct DSP_STATE_FUNCTIONS
	{
		// Token: 0x0400031E RID: 798
		private DSP_ALLOC_FUNC alloc;

		// Token: 0x0400031F RID: 799
		private DSP_REALLOC_FUNC realloc;

		// Token: 0x04000320 RID: 800
		private DSP_FREE_FUNC free;

		// Token: 0x04000321 RID: 801
		private DSP_GETSAMPLERATE_FUNC getsamplerate;

		// Token: 0x04000322 RID: 802
		private DSP_GETBLOCKSIZE_FUNC getblocksize;

		// Token: 0x04000323 RID: 803
		private IntPtr dft;

		// Token: 0x04000324 RID: 804
		private IntPtr pan;

		// Token: 0x04000325 RID: 805
		private DSP_GETSPEAKERMODE_FUNC getspeakermode;

		// Token: 0x04000326 RID: 806
		private DSP_GETCLOCK_FUNC getclock;

		// Token: 0x04000327 RID: 807
		private DSP_GETLISTENERATTRIBUTES_FUNC getlistenerattributes;

		// Token: 0x04000328 RID: 808
		private DSP_LOG_FUNC log;

		// Token: 0x04000329 RID: 809
		private DSP_GETUSERDATA_FUNC getuserdata;
	}
}
