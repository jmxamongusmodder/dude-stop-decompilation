using System;

namespace Steamworks
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00012644 File Offset: 0x00010A44
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0001264D File Offset: 0x00010A4D
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00012660 File Offset: 0x00010A60
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00012681 File Offset: 0x00010A81
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00012694 File Offset: 0x00010A94
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000126A6 File Offset: 0x00010AA6
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000126B2 File Offset: 0x00010AB2
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000126BA File Offset: 0x00010ABA
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000126C3 File Offset: 0x00010AC3
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000126D4 File Offset: 0x00010AD4
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000CCF RID: 3279
		public ulong m_SteamLeaderboard;
	}
}
