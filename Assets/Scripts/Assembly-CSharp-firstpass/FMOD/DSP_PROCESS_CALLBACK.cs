using System;

namespace FMOD
{
	// Token: 0x02000076 RID: 118
	// (Invoke) Token: 0x06000472 RID: 1138
	public delegate RESULT DSP_PROCESS_CALLBACK(ref DSP_STATE dsp_state, uint length, ref DSP_BUFFER_ARRAY inbufferarray, ref DSP_BUFFER_ARRAY outbufferarray, bool inputsidle, DSP_PROCESS_OPERATION op);
}
