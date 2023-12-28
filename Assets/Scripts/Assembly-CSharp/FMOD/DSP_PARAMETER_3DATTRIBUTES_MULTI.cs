using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x020000A2 RID: 162
	public struct DSP_PARAMETER_3DATTRIBUTES_MULTI
	{
		// Token: 0x040002F4 RID: 756
		public int numlisteners;

		// Token: 0x040002F5 RID: 757
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ATTRIBUTES_3D[] relative;

		// Token: 0x040002F6 RID: 758
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public float[] weight;

		// Token: 0x040002F7 RID: 759
		public ATTRIBUTES_3D absolute;
	}
}
