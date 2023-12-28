using System;

namespace Steamworks
{
	// Token: 0x02000270 RID: 624
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x000119F9 File Offset: 0x0000FDF9
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00011A02 File Offset: 0x0000FE02
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00011A15 File Offset: 0x0000FE15
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00011A36 File Offset: 0x0000FE36
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00011A49 File Offset: 0x0000FE49
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00011A5B File Offset: 0x0000FE5B
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00011A67 File Offset: 0x0000FE67
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00011A6F File Offset: 0x0000FE6F
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00011A78 File Offset: 0x0000FE78
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00011A89 File Offset: 0x0000FE89
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x04000CAE RID: 3246
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000CAF RID: 3247
		public ulong m_SteamItemInstanceID;
	}
}
