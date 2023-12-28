using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200009E RID: 158
	public struct DSP_PARAMETER_DESC
	{
		// Token: 0x040002E4 RID: 740
		public DSP_PARAMETER_TYPE type;

		// Token: 0x040002E5 RID: 741
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] name;

		// Token: 0x040002E6 RID: 742
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] label;

		// Token: 0x040002E7 RID: 743
		public string description;

		// Token: 0x040002E8 RID: 744
		public DSP_PARAMETER_DESC_UNION desc;
	}
}
