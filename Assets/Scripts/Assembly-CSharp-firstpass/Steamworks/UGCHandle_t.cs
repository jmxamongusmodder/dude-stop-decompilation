using System;

namespace Steamworks
{
	// Token: 0x02000278 RID: 632
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00011F61 File Offset: 0x00010361
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00011F6A File Offset: 0x0001036A
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00011F7D File Offset: 0x0001037D
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00011F9E File Offset: 0x0001039E
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00011FB1 File Offset: 0x000103B1
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00011FC3 File Offset: 0x000103C3
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00011FCF File Offset: 0x000103CF
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00011FD7 File Offset: 0x000103D7
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00011FE0 File Offset: 0x000103E0
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00011FF1 File Offset: 0x000103F1
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x04000CBC RID: 3260
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000CBD RID: 3261
		public ulong m_UGCHandle;
	}
}
