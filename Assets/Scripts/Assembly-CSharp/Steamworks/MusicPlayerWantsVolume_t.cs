using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000179 RID: 377
	[CallbackIdentity(4011)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x04000637 RID: 1591
		public const int k_iCallback = 4011;

		// Token: 0x04000638 RID: 1592
		public float m_flNewVolume;
	}
}
