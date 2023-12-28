using System;

namespace Steamworks
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00011CA7 File Offset: 0x000100A7
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00011CB0 File Offset: 0x000100B0
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00011CC3 File Offset: 0x000100C3
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00011CE4 File Offset: 0x000100E4
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00011CF7 File Offset: 0x000100F7
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00011D09 File Offset: 0x00010109
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00011D15 File Offset: 0x00010115
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00011D1D File Offset: 0x0001011D
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00011D26 File Offset: 0x00010126
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00011D37 File Offset: 0x00010137
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000CB5 RID: 3253
		public uint m_SNetSocket;
	}
}
