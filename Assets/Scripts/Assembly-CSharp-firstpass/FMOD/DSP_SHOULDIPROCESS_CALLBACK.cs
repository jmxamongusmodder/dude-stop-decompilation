using System;

namespace FMOD
{
	// Token: 0x02000075 RID: 117
	// (Invoke) Token: 0x0600046E RID: 1134
	public delegate RESULT DSP_SHOULDIPROCESS_CALLBACK(ref DSP_STATE dsp_state, bool inputsidle, uint length, CHANNELMASK inmask, int inchannels, SPEAKERMODE speakermode);
}
