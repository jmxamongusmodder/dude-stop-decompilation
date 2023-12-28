using System;

namespace Steamworks
{
	// Token: 0x02000272 RID: 626
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x00011B52 File Offset: 0x0000FF52
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00011B5B File Offset: 0x0000FF5B
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00011B6E File Offset: 0x0000FF6E
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00011B8F File Offset: 0x0000FF8F
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00011BA2 File Offset: 0x0000FFA2
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00011BB4 File Offset: 0x0000FFB4
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00011BC0 File Offset: 0x0000FFC0
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00011BC8 File Offset: 0x0000FFC8
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00011BD1 File Offset: 0x0000FFD1
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00011BE2 File Offset: 0x0000FFE2
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x04000CB2 RID: 3250
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000CB3 RID: 3251
		public int m_HServerQuery;
	}
}
