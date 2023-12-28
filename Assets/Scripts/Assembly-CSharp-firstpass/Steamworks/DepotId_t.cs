using System;

namespace Steamworks
{
	// Token: 0x0200027C RID: 636
	[Serializable]
	public struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06000F55 RID: 3925 RVA: 0x00012219 File Offset: 0x00010619
		public DepotId_t(uint value)
		{
			this.m_DepotId = value;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00012222 File Offset: 0x00010622
		public override string ToString()
		{
			return this.m_DepotId.ToString();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00012235 File Offset: 0x00010635
		public override bool Equals(object other)
		{
			return other is DepotId_t && this == (DepotId_t)other;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00012256 File Offset: 0x00010656
		public override int GetHashCode()
		{
			return this.m_DepotId.GetHashCode();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00012269 File Offset: 0x00010669
		public static bool operator ==(DepotId_t x, DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0001227B File Offset: 0x0001067B
		public static bool operator !=(DepotId_t x, DepotId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00012287 File Offset: 0x00010687
		public static explicit operator DepotId_t(uint value)
		{
			return new DepotId_t(value);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0001228F File Offset: 0x0001068F
		public static explicit operator uint(DepotId_t that)
		{
			return that.m_DepotId;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00012298 File Offset: 0x00010698
		public bool Equals(DepotId_t other)
		{
			return this.m_DepotId == other.m_DepotId;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000122A9 File Offset: 0x000106A9
		public int CompareTo(DepotId_t other)
		{
			return this.m_DepotId.CompareTo(other.m_DepotId);
		}

		// Token: 0x04000CC3 RID: 3267
		public static readonly DepotId_t Invalid = new DepotId_t(0U);

		// Token: 0x04000CC4 RID: 3268
		public uint m_DepotId;
	}
}
