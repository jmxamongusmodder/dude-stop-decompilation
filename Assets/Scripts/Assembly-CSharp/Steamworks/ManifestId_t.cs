using System;

namespace Steamworks
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	public struct ManifestId_t : IEquatable<ManifestId_t>, IComparable<ManifestId_t>
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x000122CA File Offset: 0x000106CA
		public ManifestId_t(ulong value)
		{
			this.m_ManifestId = value;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000122D3 File Offset: 0x000106D3
		public override string ToString()
		{
			return this.m_ManifestId.ToString();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000122E6 File Offset: 0x000106E6
		public override bool Equals(object other)
		{
			return other is ManifestId_t && this == (ManifestId_t)other;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00012307 File Offset: 0x00010707
		public override int GetHashCode()
		{
			return this.m_ManifestId.GetHashCode();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0001231A File Offset: 0x0001071A
		public static bool operator ==(ManifestId_t x, ManifestId_t y)
		{
			return x.m_ManifestId == y.m_ManifestId;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0001232C File Offset: 0x0001072C
		public static bool operator !=(ManifestId_t x, ManifestId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00012338 File Offset: 0x00010738
		public static explicit operator ManifestId_t(ulong value)
		{
			return new ManifestId_t(value);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00012340 File Offset: 0x00010740
		public static explicit operator ulong(ManifestId_t that)
		{
			return that.m_ManifestId;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00012349 File Offset: 0x00010749
		public bool Equals(ManifestId_t other)
		{
			return this.m_ManifestId == other.m_ManifestId;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0001235A File Offset: 0x0001075A
		public int CompareTo(ManifestId_t other)
		{
			return this.m_ManifestId.CompareTo(other.m_ManifestId);
		}

		// Token: 0x04000CC5 RID: 3269
		public static readonly ManifestId_t Invalid = new ManifestId_t(0UL);

		// Token: 0x04000CC6 RID: 3270
		public ulong m_ManifestId;
	}
}
