using System;

namespace Steamworks
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x000115E0 File Offset: 0x0000F9E0
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000115E9 File Offset: 0x0000F9E9
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000115FC File Offset: 0x0000F9FC
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0001161D File Offset: 0x0000FA1D
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00011630 File Offset: 0x0000FA30
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00011642 File Offset: 0x0000FA42
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0001164E File Offset: 0x0000FA4E
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00011656 File Offset: 0x0000FA56
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0001165F File Offset: 0x0000FA5F
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00011670 File Offset: 0x0000FA70
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x04000CA3 RID: 3235
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000CA4 RID: 3236
		public short m_FriendsGroupID;
	}
}
