using System;

namespace Steamworks
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	public struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000118A4 File Offset: 0x0000FCA4
		public SteamInventoryResult_t(int value)
		{
			this.m_SteamInventoryResult = value;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000118AD File Offset: 0x0000FCAD
		public override string ToString()
		{
			return this.m_SteamInventoryResult.ToString();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000118C0 File Offset: 0x0000FCC0
		public override bool Equals(object other)
		{
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000118E1 File Offset: 0x0000FCE1
		public override int GetHashCode()
		{
			return this.m_SteamInventoryResult.GetHashCode();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000118F4 File Offset: 0x0000FCF4
		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00011906 File Offset: 0x0000FD06
		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00011912 File Offset: 0x0000FD12
		public static explicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t(value);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0001191A File Offset: 0x0000FD1A
		public static explicit operator int(SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00011923 File Offset: 0x0000FD23
		public bool Equals(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00011934 File Offset: 0x0000FD34
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}

		// Token: 0x04000CAB RID: 3243
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);

		// Token: 0x04000CAC RID: 3244
		public int m_SteamInventoryResult;
	}
}
