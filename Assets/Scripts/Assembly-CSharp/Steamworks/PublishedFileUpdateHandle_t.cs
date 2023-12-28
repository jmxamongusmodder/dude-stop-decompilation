using System;

namespace Steamworks
{
	// Token: 0x02000276 RID: 630
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00011DFD File Offset: 0x000101FD
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00011E06 File Offset: 0x00010206
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00011E19 File Offset: 0x00010219
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00011E3A File Offset: 0x0001023A
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00011E4D File Offset: 0x0001024D
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00011E5F File Offset: 0x0001025F
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00011E6B File Offset: 0x0001026B
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00011E73 File Offset: 0x00010273
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00011E7C File Offset: 0x0001027C
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00011E8D File Offset: 0x0001028D
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x04000CB8 RID: 3256
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000CB9 RID: 3257
		public ulong m_PublishedFileUpdateHandle;
	}
}
