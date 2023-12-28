using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x020000AA RID: 170
	public struct DSP_METERING_INFO
	{
		// Token: 0x04000332 RID: 818
		public int numsamples;

		// Token: 0x04000333 RID: 819
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public float[] peaklevel;

		// Token: 0x04000334 RID: 820
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public float[] rmslevel;

		// Token: 0x04000335 RID: 821
		public short numchannels;
	}
}
