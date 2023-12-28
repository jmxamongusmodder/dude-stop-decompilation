using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000226 RID: 550
	public struct MatchMakingKeyValuePair_t
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x0000F2D6 File Offset: 0x0000D6D6
		private MatchMakingKeyValuePair_t(string strKey, string strValue)
		{
			this.m_szKey = strKey;
			this.m_szValue = strValue;
		}

		// Token: 0x04000C2D RID: 3117
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey;

		// Token: 0x04000C2E RID: 3118
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue;
	}
}
