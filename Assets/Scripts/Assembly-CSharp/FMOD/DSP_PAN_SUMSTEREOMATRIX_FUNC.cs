using System;

namespace FMOD
{
	// Token: 0x0200008F RID: 143
	// (Invoke) Token: 0x060004D6 RID: 1238
	public delegate RESULT DSP_PAN_SUMSTEREOMATRIX_FUNC(ref DSP_STATE dsp_state, int sourceSpeakerMode, float pan, float lowFrequencyGain, float overallGain, int matrixHop, IntPtr matrix);
}
