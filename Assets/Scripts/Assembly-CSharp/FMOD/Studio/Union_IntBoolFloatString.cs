using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Explicit)]
	internal struct Union_IntBoolFloatString
	{
		// Token: 0x040004A1 RID: 1185
		[FieldOffset(0)]
		public int intvalue;

		// Token: 0x040004A2 RID: 1186
		[FieldOffset(0)]
		public bool boolvalue;

		// Token: 0x040004A3 RID: 1187
		[FieldOffset(0)]
		public float floatvalue;

		// Token: 0x040004A4 RID: 1188
		[FieldOffset(0)]
		public StringWrapper stringvalue;
	}
}
