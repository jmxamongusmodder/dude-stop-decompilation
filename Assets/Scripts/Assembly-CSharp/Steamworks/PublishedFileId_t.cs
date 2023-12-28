using System;

namespace Steamworks
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x00011D4B File Offset: 0x0001014B
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00011D54 File Offset: 0x00010154
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00011D67 File Offset: 0x00010167
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00011D88 File Offset: 0x00010188
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00011D9B File Offset: 0x0001019B
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00011DAD File Offset: 0x000101AD
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00011DB9 File Offset: 0x000101B9
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00011DC1 File Offset: 0x000101C1
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00011DCA File Offset: 0x000101CA
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00011DDB File Offset: 0x000101DB
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x04000CB6 RID: 3254
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000CB7 RID: 3255
		public ulong m_PublishedFileId;
	}
}
