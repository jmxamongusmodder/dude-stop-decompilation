using System;

namespace Steamworks
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x000120C4 File Offset: 0x000104C4
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000120CD File Offset: 0x000104CD
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000120E0 File Offset: 0x000104E0
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00012101 File Offset: 0x00010501
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00012114 File Offset: 0x00010514
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00012126 File Offset: 0x00010526
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00012132 File Offset: 0x00010532
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0001213A File Offset: 0x0001053A
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00012143 File Offset: 0x00010543
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00012154 File Offset: 0x00010554
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x04000CC0 RID: 3264
		public uint m_AccountID;
	}
}
