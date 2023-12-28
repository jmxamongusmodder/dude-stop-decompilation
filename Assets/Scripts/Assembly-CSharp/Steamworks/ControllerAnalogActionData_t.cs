using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021B RID: 539
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ControllerAnalogActionData_t
	{
		// Token: 0x04000BE5 RID: 3045
		public EControllerSourceMode eMode;

		// Token: 0x04000BE6 RID: 3046
		public float x;

		// Token: 0x04000BE7 RID: 3047
		public float y;

		// Token: 0x04000BE8 RID: 3048
		public byte bActive;
	}
}
