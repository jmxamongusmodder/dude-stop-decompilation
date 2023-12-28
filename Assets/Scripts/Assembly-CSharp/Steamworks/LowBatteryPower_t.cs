using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C5 RID: 453
	[CallbackIdentity(702)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LowBatteryPower_t
	{
		// Token: 0x0400075D RID: 1885
		public const int k_iCallback = 702;

		// Token: 0x0400075E RID: 1886
		public byte m_nMinutesBatteryLeft;
	}
}
