using System;

namespace FMOD
{
	// Token: 0x02000093 RID: 147
	// (Invoke) Token: 0x060004E6 RID: 1254
	public delegate RESULT DSP_PAN_GETROLLOFFGAIN_FUNC(ref DSP_STATE dsp_state, DSP_PAN_3D_ROLLOFF_TYPE rolloff, float distance, float mindistance, float maxdistance, out float gain);
}
