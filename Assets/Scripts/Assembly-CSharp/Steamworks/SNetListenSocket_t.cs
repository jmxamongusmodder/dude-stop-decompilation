using System;

namespace Steamworks
{
	// Token: 0x02000273 RID: 627
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00011C03 File Offset: 0x00010003
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00011C0C File Offset: 0x0001000C
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00011C1F File Offset: 0x0001001F
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00011C40 File Offset: 0x00010040
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00011C53 File Offset: 0x00010053
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00011C65 File Offset: 0x00010065
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00011C71 File Offset: 0x00010071
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00011C79 File Offset: 0x00010079
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00011C82 File Offset: 0x00010082
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00011C93 File Offset: 0x00010093
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000CB4 RID: 3252
		public uint m_SNetListenSocket;
	}
}
