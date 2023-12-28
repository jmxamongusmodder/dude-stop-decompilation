using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021D RID: 541
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ControllerMotionData_t
	{
		// Token: 0x04000BEB RID: 3051
		public float rotQuatX;

		// Token: 0x04000BEC RID: 3052
		public float rotQuatY;

		// Token: 0x04000BED RID: 3053
		public float rotQuatZ;

		// Token: 0x04000BEE RID: 3054
		public float rotQuatW;

		// Token: 0x04000BEF RID: 3055
		public float posAccelX;

		// Token: 0x04000BF0 RID: 3056
		public float posAccelY;

		// Token: 0x04000BF1 RID: 3057
		public float posAccelZ;

		// Token: 0x04000BF2 RID: 3058
		public float rotVelX;

		// Token: 0x04000BF3 RID: 3059
		public float rotVelY;

		// Token: 0x04000BF4 RID: 3060
		public float rotVelZ;
	}
}
