using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x020000A4 RID: 164
	public struct DSP_PARAMETER_FFT
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000071A4 File Offset: 0x000055A4
		public float[][] spectrum
		{
			get
			{
				float[][] array = new float[this.numchannels][];
				for (int i = 0; i < this.numchannels; i++)
				{
					array[i] = new float[this.length];
					Marshal.Copy(this.spectrum_internal[i], array[i], 0, this.length);
				}
				return array;
			}
		}

		// Token: 0x040002F9 RID: 761
		public int length;

		// Token: 0x040002FA RID: 762
		public int numchannels;

		// Token: 0x040002FB RID: 763
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private IntPtr[] spectrum_internal;
	}
}
