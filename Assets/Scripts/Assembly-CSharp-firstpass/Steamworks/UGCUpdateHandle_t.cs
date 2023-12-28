using System;

namespace Steamworks
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x000124E0 File Offset: 0x000108E0
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000124E9 File Offset: 0x000108E9
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000124FC File Offset: 0x000108FC
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0001251D File Offset: 0x0001091D
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00012530 File Offset: 0x00010930
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00012542 File Offset: 0x00010942
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0001254E File Offset: 0x0001094E
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00012556 File Offset: 0x00010956
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0001255F File Offset: 0x0001095F
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00012570 File Offset: 0x00010970
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x04000CCB RID: 3275
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000CCC RID: 3276
		public ulong m_UGCUpdateHandle;
	}
}
