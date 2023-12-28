using System;

namespace Steamworks
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x00010A63 File Offset: 0x0000EE63
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00010A6C File Offset: 0x0000EE6C
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00010A7F File Offset: 0x0000EE7F
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00010AA0 File Offset: 0x0000EEA0
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00010AB3 File Offset: 0x0000EEB3
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00010AC5 File Offset: 0x0000EEC5
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00010AD1 File Offset: 0x0000EED1
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00010AD9 File Offset: 0x0000EED9
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00010AE2 File Offset: 0x0000EEE2
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00010AF3 File Offset: 0x0000EEF3
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000C8F RID: 3215
		public int m_HSteamPipe;
	}
}
