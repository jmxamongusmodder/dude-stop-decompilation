using System;

namespace FMOD
{
	// Token: 0x02000074 RID: 116
	// (Invoke) Token: 0x0600046A RID: 1130
	public delegate RESULT DSP_READCALLBACK(ref DSP_STATE dsp_state, IntPtr inbuffer, IntPtr outbuffer, uint length, int inchannels, ref int outchannels);
}
