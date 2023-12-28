using System;

namespace Steamworks
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x000126E8 File Offset: 0x00010AE8
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000126F1 File Offset: 0x00010AF1
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00012704 File Offset: 0x00010B04
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00012725 File Offset: 0x00010B25
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00012738 File Offset: 0x00010B38
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0001274A File Offset: 0x00010B4A
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00012756 File Offset: 0x00010B56
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0001275E File Offset: 0x00010B5E
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00012767 File Offset: 0x00010B67
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00012778 File Offset: 0x00010B78
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000CD0 RID: 3280
		public ulong m_SteamLeaderboardEntries;
	}
}
