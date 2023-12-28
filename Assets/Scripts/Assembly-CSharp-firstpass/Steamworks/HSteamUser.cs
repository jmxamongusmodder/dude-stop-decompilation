using System;

namespace Steamworks
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x00010B07 File Offset: 0x0000EF07
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00010B10 File Offset: 0x0000EF10
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00010B23 File Offset: 0x0000EF23
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00010B44 File Offset: 0x0000EF44
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00010B57 File Offset: 0x0000EF57
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00010B69 File Offset: 0x0000EF69
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00010B75 File Offset: 0x0000EF75
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00010B7D File Offset: 0x0000EF7D
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00010B86 File Offset: 0x0000EF86
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00010B97 File Offset: 0x0000EF97
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000C90 RID: 3216
		public int m_HSteamUser;
	}
}
