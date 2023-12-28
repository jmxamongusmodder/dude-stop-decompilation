using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016E RID: 366
	[CallbackIdentity(4002)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct VolumeHasChanged_t
	{
		// Token: 0x04000629 RID: 1577
		public const int k_iCallback = 4002;

		// Token: 0x0400062A RID: 1578
		public float m_flNewVolume;
	}
}
