using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x020000A5 RID: 165
	public struct DSP_DESCRIPTION
	{
		// Token: 0x040002FC RID: 764
		public uint pluginsdkversion;

		// Token: 0x040002FD RID: 765
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public char[] name;

		// Token: 0x040002FE RID: 766
		public uint version;

		// Token: 0x040002FF RID: 767
		public int numinputbuffers;

		// Token: 0x04000300 RID: 768
		public int numoutputbuffers;

		// Token: 0x04000301 RID: 769
		public DSP_CREATECALLBACK create;

		// Token: 0x04000302 RID: 770
		public DSP_RELEASECALLBACK release;

		// Token: 0x04000303 RID: 771
		public DSP_RESETCALLBACK reset;

		// Token: 0x04000304 RID: 772
		public DSP_READCALLBACK read;

		// Token: 0x04000305 RID: 773
		public DSP_PROCESS_CALLBACK process;

		// Token: 0x04000306 RID: 774
		public DSP_SETPOSITIONCALLBACK setposition;

		// Token: 0x04000307 RID: 775
		public int numparameters;

		// Token: 0x04000308 RID: 776
		public IntPtr paramdesc;

		// Token: 0x04000309 RID: 777
		public DSP_SETPARAM_FLOAT_CALLBACK setparameterfloat;

		// Token: 0x0400030A RID: 778
		public DSP_SETPARAM_INT_CALLBACK setparameterint;

		// Token: 0x0400030B RID: 779
		public DSP_SETPARAM_BOOL_CALLBACK setparameterbool;

		// Token: 0x0400030C RID: 780
		public DSP_SETPARAM_DATA_CALLBACK setparameterdata;

		// Token: 0x0400030D RID: 781
		public DSP_GETPARAM_FLOAT_CALLBACK getparameterfloat;

		// Token: 0x0400030E RID: 782
		public DSP_GETPARAM_INT_CALLBACK getparameterint;

		// Token: 0x0400030F RID: 783
		public DSP_GETPARAM_BOOL_CALLBACK getparameterbool;

		// Token: 0x04000310 RID: 784
		public DSP_GETPARAM_DATA_CALLBACK getparameterdata;

		// Token: 0x04000311 RID: 785
		public DSP_SHOULDIPROCESS_CALLBACK shouldiprocess;

		// Token: 0x04000312 RID: 786
		public IntPtr userdata;

		// Token: 0x04000313 RID: 787
		public DSP_SYSTEM_REGISTER_CALLBACK sys_register;

		// Token: 0x04000314 RID: 788
		public DSP_SYSTEM_DEREGISTER_CALLBACK sys_deregister;

		// Token: 0x04000315 RID: 789
		public DSP_SYSTEM_MIX_CALLBACK sys_mix;
	}
}
