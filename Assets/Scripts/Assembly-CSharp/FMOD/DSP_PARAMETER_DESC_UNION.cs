using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200009D RID: 157
	[StructLayout(LayoutKind.Explicit)]
	public struct DSP_PARAMETER_DESC_UNION
	{
		// Token: 0x040002E0 RID: 736
		[FieldOffset(0)]
		public DSP_PARAMETER_DESC_FLOAT floatdesc;

		// Token: 0x040002E1 RID: 737
		[FieldOffset(0)]
		public DSP_PARAMETER_DESC_INT intdesc;

		// Token: 0x040002E2 RID: 738
		[FieldOffset(0)]
		public DSP_PARAMETER_DESC_BOOL booldesc;

		// Token: 0x040002E3 RID: 739
		[FieldOffset(0)]
		public DSP_PARAMETER_DESC_DATA datadesc;
	}
}
