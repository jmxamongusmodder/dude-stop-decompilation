using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000236 RID: 566
	public class MMKVPMarshaller
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x0000FDD0 File Offset: 0x0000E1D0
		public MMKVPMarshaller(MatchMakingKeyValuePair_t[] filters)
		{
			if (filters == null)
			{
				return;
			}
			int num = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
			this.m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
			this.m_pArrayEntries = Marshal.AllocHGlobal(num * filters.Length);
			for (int i = 0; i < filters.Length; i++)
			{
				Marshal.StructureToPtr(filters[i], new IntPtr(this.m_pArrayEntries.ToInt64() + (long)(i * num)), false);
			}
			Marshal.WriteIntPtr(this.m_pNativeArray, this.m_pArrayEntries);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0000FE7C File Offset: 0x0000E27C
		~MMKVPMarshaller()
		{
			if (this.m_pArrayEntries != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pArrayEntries);
			}
			if (this.m_pNativeArray != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pNativeArray);
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0000FEE8 File Offset: 0x0000E2E8
		public static implicit operator IntPtr(MMKVPMarshaller that)
		{
			return that.m_pNativeArray;
		}

		// Token: 0x04000C4C RID: 3148
		private IntPtr m_pNativeArray;

		// Token: 0x04000C4D RID: 3149
		private IntPtr m_pArrayEntries;
	}
}
