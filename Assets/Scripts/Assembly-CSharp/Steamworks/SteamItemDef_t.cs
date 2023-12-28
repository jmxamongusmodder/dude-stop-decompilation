using System;

namespace Steamworks
{
	// Token: 0x0200026F RID: 623
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x00011955 File Offset: 0x0000FD55
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0001195E File Offset: 0x0000FD5E
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00011971 File Offset: 0x0000FD71
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00011992 File Offset: 0x0000FD92
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000119A5 File Offset: 0x0000FDA5
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000119B7 File Offset: 0x0000FDB7
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000119C3 File Offset: 0x0000FDC3
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000119CB File Offset: 0x0000FDCB
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000119D4 File Offset: 0x0000FDD4
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000119E5 File Offset: 0x0000FDE5
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000CAD RID: 3245
		public int m_SteamItemDef;
	}
}
