using System;

namespace Steamworks
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0001242E File Offset: 0x0001082E
		public UGCQueryHandle_t(ulong value)
		{
			this.m_UGCQueryHandle = value;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00012437 File Offset: 0x00010837
		public override string ToString()
		{
			return this.m_UGCQueryHandle.ToString();
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0001244A File Offset: 0x0001084A
		public override bool Equals(object other)
		{
			return other is UGCQueryHandle_t && this == (UGCQueryHandle_t)other;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0001246B File Offset: 0x0001086B
		public override int GetHashCode()
		{
			return this.m_UGCQueryHandle.GetHashCode();
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0001247E File Offset: 0x0001087E
		public static bool operator ==(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00012490 File Offset: 0x00010890
		public static bool operator !=(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0001249C File Offset: 0x0001089C
		public static explicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t(value);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000124A4 File Offset: 0x000108A4
		public static explicit operator ulong(UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000124AD File Offset: 0x000108AD
		public bool Equals(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000124BE File Offset: 0x000108BE
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}

		// Token: 0x04000CC9 RID: 3273
		public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(ulong.MaxValue);

		// Token: 0x04000CCA RID: 3274
		public ulong m_UGCQueryHandle;
	}
}
