using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000222 RID: 546
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamParamStringArray_t
	{
		// Token: 0x04000C08 RID: 3080
		public IntPtr m_ppStrings;

		// Token: 0x04000C09 RID: 3081
		public int m_nNumStrings;
	}
}
