using System;

namespace Steamworks
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0001129F File Offset: 0x0000F69F
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000112A8 File Offset: 0x0000F6A8
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000112BB File Offset: 0x0000F6BB
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000112DC File Offset: 0x0000F6DC
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000112EF File Offset: 0x0000F6EF
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00011301 File Offset: 0x0000F701
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0001130D File Offset: 0x0000F70D
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00011315 File Offset: 0x0000F715
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0001131E File Offset: 0x0000F71E
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0001132F File Offset: 0x0000F72F
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x04000C9D RID: 3229
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000C9E RID: 3230
		public uint m_HAuthTicket;
	}
}
