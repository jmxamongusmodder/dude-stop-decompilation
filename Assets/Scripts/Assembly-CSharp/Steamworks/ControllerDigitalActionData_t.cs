using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021C RID: 540
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ControllerDigitalActionData_t
	{
		// Token: 0x04000BE9 RID: 3049
		public byte bState;

		// Token: 0x04000BEA RID: 3050
		public byte bActive;
	}
}
