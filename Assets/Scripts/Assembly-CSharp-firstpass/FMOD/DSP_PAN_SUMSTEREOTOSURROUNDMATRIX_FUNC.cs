using System;

namespace FMOD
{
	// Token: 0x02000092 RID: 146
	// (Invoke) Token: 0x060004E2 RID: 1250
	public delegate RESULT DSP_PAN_SUMSTEREOTOSURROUNDMATRIX_FUNC(ref DSP_STATE dsp_state, int targetSpeakerMode, float direction, float extent, float rotation, float lowFrequencyGain, float overallGain, int matrixHop, IntPtr matrix);
}
