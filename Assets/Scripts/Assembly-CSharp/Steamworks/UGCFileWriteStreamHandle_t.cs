using System;

namespace Steamworks
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	public struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x00011EAF File Offset: 0x000102AF
		public UGCFileWriteStreamHandle_t(ulong value)
		{
			this.m_UGCFileWriteStreamHandle = value;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00011EB8 File Offset: 0x000102B8
		public override string ToString()
		{
			return this.m_UGCFileWriteStreamHandle.ToString();
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00011ECB File Offset: 0x000102CB
		public override bool Equals(object other)
		{
			return other is UGCFileWriteStreamHandle_t && this == (UGCFileWriteStreamHandle_t)other;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00011EEC File Offset: 0x000102EC
		public override int GetHashCode()
		{
			return this.m_UGCFileWriteStreamHandle.GetHashCode();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00011EFF File Offset: 0x000102FF
		public static bool operator ==(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00011F11 File Offset: 0x00010311
		public static bool operator !=(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00011F1D File Offset: 0x0001031D
		public static explicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t(value);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00011F25 File Offset: 0x00010325
		public static explicit operator ulong(UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00011F2E File Offset: 0x0001032E
		public bool Equals(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00011F3F File Offset: 0x0001033F
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}

		// Token: 0x04000CBA RID: 3258
		public static readonly UGCFileWriteStreamHandle_t Invalid = new UGCFileWriteStreamHandle_t(ulong.MaxValue);

		// Token: 0x04000CBB RID: 3259
		public ulong m_UGCFileWriteStreamHandle;
	}
}
